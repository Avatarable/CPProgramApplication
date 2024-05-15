using CPProgramApplication.Data.DTOs;
using CPProgramApplication.Data.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace CPProgramApplication.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly CosmosDbSettings _cosmosDbSettings;
        private readonly Container _questionsContainer;
        private readonly Container _applicationFormContainer;
        private readonly Container _programContainer;

        public CosmosDbService(IOptions<CosmosDbSettings> cosmosDbSettings)
        {
            _cosmosDbSettings = cosmosDbSettings.Value;
            var cosmosClient = new CosmosClient(_cosmosDbSettings.ConnectionString);
            var database = cosmosClient.GetDatabase(_cosmosDbSettings.DatabaseName);
            _questionsContainer = database.GetContainer(_cosmosDbSettings.QuestionsContainerName);
            _applicationFormContainer = database.GetContainer(_cosmosDbSettings.ApplicationFormsContainerName);
            _programContainer = database.GetContainer(_cosmosDbSettings.ProgramsContainerName);
        }

        public async Task<CustomQuestion> CreateQuestionAsync(CustomQuestion question)
        {
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

        public async Task<CustomQuestion> UpdateQuestionAsync(string id, CustomQuestion question)
        {
            var response = await _questionsContainer.ReplaceItemAsync(question, id);
            return response.Resource;
        }

        public async Task DeleteQuestionAsync(string id)
        {
            await _questionsContainer.DeleteItemAsync<CustomQuestion>(id, new PartitionKey(id));
        }



    }
}
