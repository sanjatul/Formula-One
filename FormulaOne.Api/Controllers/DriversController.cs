using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;

namespace FormulaOne.Api.Controllers
{
    public class DriversController : BaseController
    {
        public DriversController(IUnitOfWork unitOfWork,IMapper mapper):base(unitOfWork,mapper)
        {        }
    }
}
