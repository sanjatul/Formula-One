
using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers
{
    public class AchievementsController : BaseController
    {
        public AchievementsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        { }
        [HttpGet]
        [Route("{driverId:guid}")]
        public async Task<IActionResult> GetDriverAchievements(Guid driverId)
        {
            var driverAchievements=await _unitOfWork.Achievements.GetAchievementAsync(driverId);
            if(driverAchievements == null) return NotFound("achievements not found");
            var result = _mapper.Map<DriverAchievementResponse>(driverAchievements);
          return Ok(result);
        }
    }
}
