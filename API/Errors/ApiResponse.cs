using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors {
    public class ApiResponse {
        public ApiResponse(int statusCode, string message = null) {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode) {
            return statusCode switch {
                400 => "A bad request, You have made",
                401 => "Authorized, you are not ",
                404 => "Reference found, it was not",
                500 => "Errors are path to the dask side, Errors lead to anger. Anger leades to hatehate leades to career change.",
                _ => null
            };
        }
    }

}