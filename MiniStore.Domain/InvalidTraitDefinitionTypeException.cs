using System;
using System.Runtime.Serialization;

namespace MiniStore.Domain
{
    [Serializable]
    internal class InvalidTraitDefinitionTypeException : Exception
    {
        public InvalidTraitDefinitionTypeException()
        {
        }

        public InvalidTraitDefinitionTypeException(string message) : base(message)
        {
        }

        public InvalidTraitDefinitionTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTraitDefinitionTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}