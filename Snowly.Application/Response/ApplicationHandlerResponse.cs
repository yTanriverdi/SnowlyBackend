using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Response
{
    public class ApplicationHandlerResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public T? Data { get; set; } = default(T?);

        /// <summary>
        /// APPLICATION KATMANINDA VERİ GÖNDERME İŞLEMİ İÇİN KULLAN
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApplicationHandlerResponse<T> Ok(T data, string message)
        { 
          ApplicationHandlerResponse<T> successResponse = new ApplicationHandlerResponse<T>()
          {
              Data = data,
              Message = message,
              Success = true
          };
           return successResponse;
        }
        
        
        /// <summary>
        /// APPLICATION KATMANINDA VERİ GÖNDERME İŞLEMİ İÇİN KULLAN
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApplicationHandlerResponse<T> Fail(string message)
        { 
          ApplicationHandlerResponse<T> failResponse = new ApplicationHandlerResponse<T>()
          {
              Message = message,
              Success = false
          };
           return failResponse;
        }
    }
}
