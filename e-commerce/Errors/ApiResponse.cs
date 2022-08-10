namespace e_commerce.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statuesCode, string message = null)
        {
            StatuesCode = statuesCode;
            Message = message ?? GetDefaultMessageForStatusCode(statuesCode);
        }

        public int StatuesCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statuesCode)
        {
            return statuesCode switch
            {
                400 => "A Bad Request, You Have Made",
                401 => "Authorized, You Are Not",
                404 => "RResource found, it was not",
                500 => "Error are the path",
                _ => null
            };
        }
    }
}
