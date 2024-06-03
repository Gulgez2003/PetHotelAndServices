namespace FinalProjectApp.Application.Common.Results
{
    public class UserCommandResult
    {
        public bool IsValid { get; set; } = true;
        public Dictionary<string, List<string>> CommandErrors = new Dictionary<string, List<string>>();
    }
}