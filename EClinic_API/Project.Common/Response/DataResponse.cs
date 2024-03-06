namespace Project.Common.Response
{
    internal class DataResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public DataResponse<T> Success(T data)
        {
            return new DataResponse<T> { IsSuccess = true, Data = data };
        }
    }
}
