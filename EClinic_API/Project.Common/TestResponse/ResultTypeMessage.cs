namespace Project.Common.TestResponse
{
    public class ResultTypeData<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
    }
}
