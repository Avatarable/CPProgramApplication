using CPProgramApplication.Data.Enums;
using CPProgramApplication.Data.Models;
using CPProgramApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CPProgramApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public QuestionsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomQuestion([FromBody] CustomQuestion customQuestion)
        {
            var addedQuestion = await _cosmosDbService.CreateQuestionAsync(customQuestion);
            if (addedQuestion == null)
            {
                return NotFound(); // Program not found
            }

            return Ok(addedQuestion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(string id, CustomQuestion question)
        {
            var updatedQuestion = await _cosmosDbService.UpdateQuestionAsync(id, question);
            return Ok(updatedQuestion);
        }

        [HttpGet("{type}")]
        public async Task<IActionResult> GetQuestionsByType(QuestionType type)
        {
            try
            {
                var questions = await _cosmosDbService.GetQuestionByTypeAsync(type);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
