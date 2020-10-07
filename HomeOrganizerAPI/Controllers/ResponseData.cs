namespace HomeOrganizerAPI.Controllers
{
    public class ResponseData<T>
    {
        public T[] data { get; set; }
        public int total { get; set; }
        public string message { get; set; }
        public string error { get; set; }
    }
}
