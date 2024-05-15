namespace CPProgramApplication.Data.DTOs
{
    public class CosmosDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string QuestionsContainerName { get; set; }
        public string ApplicationFormsContainerName { get; set; }
        public string ProgramsContainerName { get; set; }
    }
}
