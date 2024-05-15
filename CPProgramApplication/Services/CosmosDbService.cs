using CPProgramApplication.Data.DTOs;
using CPProgramApplication.Data.Enums;
using CPProgramApplication.Data.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.Net;

namespace CPProgramApplication.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly CosmosDbSettings _cosmosDbSettings;
        private readonly Container _questionsContainer;
        private readonly Container _applicationFormsContainer;
        private readonly Container _programsContainer;

        public CosmosDbService(IOptions<CosmosDbSettings> cosmosDbSettings)
        {
            _cosmosDbSettings = cosmosDbSettings.Value;
            var cosmosClient = new CosmosClient(_cosmosDbSettings.ConnectionString);
            var database = cosmosClient.GetDatabase(_cosmosDbSettings.DatabaseName);
            _questionsContainer = database.GetContainer(_cosmosDbSettings.QuestionsContainerName);
            _applicationFormsContainer = database.GetContainer(_cosmosDbSettings.ApplicationFormsContainerName);
            _programsContainer = database.GetContainer(_cosmosDbSettings.ProgramsContainerName);
        }

        public async Task<CustomQuestion> CreateQuestionAsync(CustomQuestion question)
        {
            question.Id = Guid.NewGuid().ToString();
            var response = await _questionsContainer.CreateItemAsync(question);
            return response.Resource;
        }

        public async Task<CustomQuestion> GetQuestionAsync(string id)
        {
            try
            {
                var response = await _questionsContainer.ReadItemAsync<CustomQuestion>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<CustomQuestion>> GetQuestionByTypeAsync(QuestionType type)
        {
            var queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.Type = @type")
        .WithParameter("@type", type.ToString());

            var query = _questionsContainer.GetItemQueryIterator<CustomQuestion>(queryDefinition);
            List<CustomQuestion> questions = new List<CustomQuestion>();

            while (query.HasMoreResults)
            {
                FeedResponse<CustomQuestion> response = await query.ReadNextAsync();
                questions.AddRange(response.Resource);
            }

            return questions;
        }

        public async Task<CustomQuestion> UpdateQuestionAsync(string id, CustomQuestion question)
        {
            var response = await _questionsContainer.ReplaceItemAsync(question, id);
            return response.Resource;
        }

        public async Task DeleteQuestionAsync(string id)
        {
            await _questionsContainer.DeleteItemAsync<CustomQuestion>(id, new PartitionKey(id));
        }

        public async Task<ProgramModel> CreateProgramAsync(ProgramModel program)
        {
            program.Id = Guid.NewGuid().ToString();

            // Create a new document (program) in the programs container
            ItemResponse<ProgramModel> response = await _programsContainer.CreateItemAsync(program);

            return response.Resource;
        }

        public async Task<ProgramModel> GetProgramByIdAsync(string id)
        {
            try
            {
                ItemResponse<ProgramModel> response = await _programsContainer.ReadItemAsync<ProgramModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null; // Program with the specified ID not found
            }
        }

        public async Task<List<ProgramModel>> GetAllProgramsAsync()
        {
            var query = _programsContainer.GetItemQueryIterator<ProgramModel>();
            List<ProgramModel> programs = new List<ProgramModel>();

            while (query.HasMoreResults)
            {
                FeedResponse<ProgramModel> response = await query.ReadNextAsync();
                programs.AddRange(response.Resource);
            }

            return programs;
        }


        public async Task<CustomQuestion> AddCustomQuestionAsync(string programId, CustomQuestion customQuestion)
        {
            customQuestion.Id = Guid.NewGuid().ToString();

            // Add the custom question to the specified program's custom questions
            ProgramModel program = await GetProgramByIdAsync(programId);
            if (program != null)
            {
                program.Questions.Add(customQuestion);
                await _programsContainer.ReplaceItemAsync(program, program.Id);
                return customQuestion;
            }

            return null;
        }

        public async Task<ApplicationForm> SubmitApplicationAsync(ApplicationForm applicationForm)
        {
            applicationForm.Id = Guid.NewGuid().ToString();

            // Create a new document (application form) in the application forms container
            ItemResponse<ApplicationForm> response = await _applicationFormsContainer.CreateItemAsync(applicationForm);

            return response.Resource;
        }
    }
}
