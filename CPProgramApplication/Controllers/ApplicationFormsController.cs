using CPProgramApplication.Data.Models;
using CPProgramApplication.Data.ViewModels;
using CPProgramApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CPProgramApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationFormsController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ApplicationFormsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitApplication([FromBody] ApplicationFormVM applicationForm)
        {
            var submittedForm = await _cosmosDbService.SubmitApplicationAsync(applicationForm);
            return Ok(submittedForm);
        }
    }
}
