using CPProgramApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CPProgramApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgramController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ProgramController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }
    }
}
