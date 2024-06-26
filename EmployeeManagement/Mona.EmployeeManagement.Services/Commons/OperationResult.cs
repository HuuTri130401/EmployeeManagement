using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.EmployeeManagement.Services.Commons
{
    public class OperationResult<T>
    {
        public StatusCode StatusCode { get; set; } = StatusCode.Ok;
        public string? Message { get; set; }
        public bool IsError { get; set; }

        public T? Payload { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
        public void AddError(StatusCode code, string message)
        {
            HandleError(code, message);
        }

        public void AddResponseStatusCode(StatusCode code, string message, T? payload)
        {
            HandleResponse(code, message, payload);
        }
        private void HandleResponse(StatusCode code, string message, T? payload)
        {
            StatusCode = code;
            IsError = false;
            Message = message;
            Payload = payload;
        }

        private void HandleError(StatusCode code, string message)
        {
            Errors.Add(new Error { Code = code, Message = message });
            IsError = true;
        }
    }
}
