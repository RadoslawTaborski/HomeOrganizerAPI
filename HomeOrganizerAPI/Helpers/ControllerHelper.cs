using HomeOrganizerAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOrganizerAPI.Helpers
{
    public static class ControllerHelper
    {

        public static ResponseData<T> GenerateResponse<T>(T[] data, int total)
        {
            return new ResponseData<T>
            {
                data = data,
                total = data.Length,
                message = "ok",
                error = ""
            };
        }
    }
}
