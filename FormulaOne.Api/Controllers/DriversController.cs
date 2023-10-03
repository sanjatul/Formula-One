using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests;
using FormulaOne.Entities.Dtos.Responses;
using FormulaOne.Services.General.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers
{
    public class DriversController : BaseController
    {
        public DriversController(IUnitOfWork unitOfWork,IMapper mapper):base(unitOfWork,mapper)
        {        }

        [HttpGet]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> GetDriver(Guid driverId)
        {
            var driver=await _unitOfWork.Drivers.GetById(driverId);
            if(driver == null) return NotFound();
            var result = _mapper.Map<GetDriverResponse>(driver);
            return Ok(result);
        }
        [HttpPost("")]
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest driver)
        {
            if(!ModelState.IsValid) return BadRequest();
            var result = _mapper.Map<Driver>(driver);
            await _unitOfWork.Drivers.Add(result);
            await _unitOfWork.CompleteAsync();
            //Fire and Forget Job
            var jobId = BackgroundJob.Enqueue<IEmailService>(x=>x.SendWelcomeEmail("sh@gmail.com",$"{driver.FirstName} {driver.LastName}"));
            //Continue job
            BackgroundJob.ContinueJobWith<IMerchService>(jobId, x=>x.CreateMerch($"{driver.FirstName} {driver.LastName}"));
            Console.WriteLine(jobId);
            return CreatedAtAction(nameof(GetDriver), new { driverId = result.Id }, result);
        }
        [HttpPut("")]
        public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverRequest driver)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = _mapper.Map<Driver>(driver);
            await _unitOfWork.Drivers.Update(result);
            await _unitOfWork.CompleteAsync();
            //Delayed Job
            var jobId = BackgroundJob.Schedule<IMaintenanceService>(x => x.SyncRecord(),TimeSpan.FromSeconds(20));
            Console.WriteLine(jobId);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var driver = await _unitOfWork.Drivers.All();
            //Recurring Job
            RecurringJob.AddOrUpdate<IMaintenanceService>(x => x.SyncRecord(), Cron.Minutely);
            return Ok(_mapper.Map<IEnumerable<GetDriverResponse>>(driver));
        }

        [HttpDelete]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> DeleteDriver(Guid driverId)
        {
            var driver = await _unitOfWork.Drivers.GetById(driverId);
            if (driver == null) return NotFound();
            await _unitOfWork.Drivers.Delete(driverId);
            await _unitOfWork.CompleteAsync();
            var jobId = BackgroundJob.Enqueue<IMerchService>(x => x.RemoveMerch(driverId));
           
            return NoContent();
        }
    }
}
