namespace CPProgramApplication.Data.Models
{
    public class ApplicationForm
    {
        public string Id { get; set; }
        public string ProgramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<CustomQuestionAnswer> QuestionAnswers { get; set; }
    }
}
