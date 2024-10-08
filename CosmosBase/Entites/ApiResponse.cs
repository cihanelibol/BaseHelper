namespace CosmosBase.Entites
{
    public class ApiResponse
    {
        public object? Data { get; set; }
        public bool IsSuccessful { get; set; }
        public object? Error { get; set; }
        public int StatusCode { get; set; }
    }
}
