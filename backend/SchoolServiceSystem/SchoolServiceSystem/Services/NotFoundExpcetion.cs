using System;
using System.Runtime.Serialization;

namespace SchoolServiceSystem.Services
{
    [Serializable]
    internal class NotFoundExpcetion : Exception
    {
        public NotFoundExpcetion()
        {
        }

        public NotFoundExpcetion(string message) : base(message)
        {
        }

        public NotFoundExpcetion(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundExpcetion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}