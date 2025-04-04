using DataSystem.Shared.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataSystem.Shared.Utils
{
    public static class CatchFunctions
    {
        public static ServiceResponseDTO<TEntity> ServiceResponse<TException, TEntity>(TException ex, HttpStatusCode statusCode) where TException : Exception
        {
            ServiceResponseDTO<TEntity> serviceResponseDTO = new ServiceResponseDTO<TEntity>();

            serviceResponseDTO.Success = false;
            serviceResponseDTO.Message = ex.Message;
            serviceResponseDTO.StatusCode = (int)statusCode;

            return serviceResponseDTO;
        }
    }
}
