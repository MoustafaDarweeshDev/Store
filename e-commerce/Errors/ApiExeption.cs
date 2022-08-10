namespace e_commerce.Errors
{
    public class ApiExeption : ApiResponse
    {
        public ApiExeption(int statuesCode, string message = null , string details=null) : base(statuesCode, message)
        {
            Details = details;
        }
            public string Details { get; set; } 
    }
}
