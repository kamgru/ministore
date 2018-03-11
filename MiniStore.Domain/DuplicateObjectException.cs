using System;
using System.Runtime.Serialization;

namespace MiniStore.Domain
{
    [Serializable]
    internal class DuplicateObjectException : Exception
    {
        public DuplicateObjectException()
        {
        }

        public DuplicateObjectException(string message) : base(message)
        {
        }

        public DuplicateObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}