namespace QuranGuide.Maui.Shared
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }
    }
}



