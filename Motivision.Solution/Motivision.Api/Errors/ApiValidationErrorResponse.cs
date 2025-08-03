namespace Motivision.API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse() : base(400, "Validation Failed")
        {
            Errors = new List<string>();
        }
    }
}
