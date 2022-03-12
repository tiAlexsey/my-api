namespace my_api
{
    public class CommonResponse
    {
        public object Item { get; set; }
        public int Total { get; set; }
        public string Message { get; set; }

        public CommonResponse(object item, int total = 1, string message = "")
        {
            Item = item;
            Total = total;
            Message = message;
        }
    }
}
