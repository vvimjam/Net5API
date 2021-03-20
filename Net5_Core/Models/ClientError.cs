using System;
using System.Net;

namespace Net5_Core.Models
{
    [Serializable]
    public class ClientError : Exception
    {
        public int StatusCode { get; set; }

        public ClientError()
        {

        }

        public ClientError(string userMessage, int statusCode = 500) : base(userMessage)
        {
            this.StatusCode = statusCode;
        }

    }
}
