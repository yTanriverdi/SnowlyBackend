namespace Snowly.WebAPI.APIResponse
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string ApiMessage { get; set; } = null!;
        public int StatusCode { get; set; }


        /// <summary>
        /// Başarısız API cevabı dönmek için kullan
        /// </summary>
        /// <param name="apiMessage">API'dan gönderilmek istenen mesaj</param>
        /// <param name="statusCode">API'ın durum kodu</param>
        /// <returns></returns>
        public static ApiResponse FailResponse(string apiMessage, int statusCode)
        {
            ApiResponse failResponse = new ApiResponse()
            {
                ApiMessage = apiMessage,
                StatusCode = statusCode,
                Success = false
            };
            return failResponse;
        }

       
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? ApiData { get; set; }

        /// <summary>
        /// Başarılı API cevabı dönmek için kullan
        /// </summary>
        /// <param name="data">Göndermek istediğin veri</param>
        /// <param name="apiMessage">API'dan gönderilmek istenen mesaj</param>
        /// <param name="statusCode">API'ın durum kodu</param>
        /// <returns></returns>
        public static ApiResponse<T> SuccessResponse(T data, string apiMessage, int statusCode)
        {
            ApiResponse<T> successResponse = new ApiResponse<T>()
            {
                ApiData = data,
                ApiMessage = apiMessage,
                StatusCode = statusCode,
                Success = true
            };
            return successResponse;
        }
    }
}
