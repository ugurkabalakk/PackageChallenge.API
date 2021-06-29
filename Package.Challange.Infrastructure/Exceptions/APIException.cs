using System;

namespace Package.Challange.Infrastructure.Exceptions
{
    public class APIException : Exception
    {
        public APIException(string message)
            : base(message)
        {
           
        }
        public APIException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}