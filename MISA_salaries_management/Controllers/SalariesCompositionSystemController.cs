using Microsoft.AspNetCore.Mvc;
using MISA_Core.Helpers;
using MISA_Core.Interface.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA_salaries_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesCompositionSystemController : ControllerBase
    {
        private readonly ISalariesCompositionSystemService _salariesCompositionSystemService;
        public SalariesCompositionSystemController(ISalariesCompositionSystemService salariesCompositionSystemService)
        {
            _salariesCompositionSystemService = salariesCompositionSystemService;
        }
        // GET: api/salaries-composition-system
        [HttpGet]
        public async Task<object> GetSalaryCompositionSystemByFilter([FromQuery] string search = "", [FromQuery] int limit = 10, [FromQuery] int page = 1)
        {

            var getSalarySystem = await _salariesCompositionSystemService.GetAllWithFilter(search, limit, page);
            return getSalarySystem;
        }
    }
}
