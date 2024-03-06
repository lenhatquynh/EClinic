namespace Project.Common.Response
{
    internal class NoDataResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public NoDataResponse Success(string message)
        {
            return new NoDataResponse { IsSuccess = true, Message = message };
        }

        public NoDataResponse Fail(string message)
        {
            return new NoDataResponse { IsSuccess = false, Message = message };
        }
    }
}
