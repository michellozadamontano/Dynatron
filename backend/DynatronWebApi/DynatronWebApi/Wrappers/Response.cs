namespace DynatronWebApi.Wrappers
{
    /// <summary>
    /// Response Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Response Constructor
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// Response Constructor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Response Constructor
        /// </summary>
        /// <param name="message"></param>
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        /// <summary>
        /// Success Flag
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Errors
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public T Data { get; set; }
    }
}