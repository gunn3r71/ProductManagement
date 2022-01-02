namespace ProductManagement.API.DTOs.Output
{
    public class CustomResponseOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}