using HomeOrganizerAPI.Controllers;

namespace HomeOrganizerAPI.Helpers
{
    public static class ControllerHelper
    {

        public static ResponseData<T> GenerateResponse<T>(T[] data, int total)
        {
            return new ResponseData<T>
            {
                data = data,
                total = total,
                message = "ok",
                error = ""
            };
        }
    }
}
