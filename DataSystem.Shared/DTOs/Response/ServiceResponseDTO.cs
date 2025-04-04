using DataSystem.Shared.Constants.Messages;

namespace DataSystem.Shared.DTOs.Response
{
    public class ServiceResponseDTO<T>
    {
        public int TotalPages { get; set; }

        public T GenericData { get; set; }

        public bool Success { get; set; } = true;

        public string Message { get; set; } = OkMessages.OperationCompletedWithSuccess;

        public int StatusCode { get; set; }
    }
}
