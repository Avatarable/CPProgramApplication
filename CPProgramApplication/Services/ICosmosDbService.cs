using CPProgramApplication.Data.Enums;
using CPProgramApplication.Data.Models;

namespace CPProgramApplication.Services
{
    public interface ICosmosDbService
    {
        Task<CustomQuestion> CreateQuestionAsync(CustomQuestion question);
        Task<CustomQuestion> GetQuestionAsync(string id);
        Task<CustomQuestion> UpdateQuestionAsync(string id, CustomQuestion question);
        Task<List<CustomQuestion>> GetQuestionByTypeAsync(QuestionType type);
        Task<ProgramModel> CreateProgramAsync(ProgramModel program);
        Task<ProgramModel> GetProgramByIdAsync(string id);
        Task<List<ProgramModel>> GetAllProgramsAsync();
        Task<CustomQuestion> AddCustomQuestionAsync(string programId, CustomQuestion customQuestion);
        Task<ApplicationForm> SubmitApplicationAsync(ApplicationForm applicationForm);
    }
}
