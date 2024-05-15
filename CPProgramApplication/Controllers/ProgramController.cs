using CPProgramApplication.Data.Models;
using CPProgramApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CPProgramApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ProgramController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProgram([FromBody] ProgramModel program)
        {
            var createdProgram = await _cosmosDbService.CreateProgramAsync(program);
            return Ok(createdProgram);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramById(string id)
        {
            var program = await _cosmosDbService.GetProgramByIdAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            return Ok(program);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrograms()
        {
            var programs = await _cosmosDbService.GetAllProgramsAsync();
            return Ok(programs);
        }

        
    }
}
