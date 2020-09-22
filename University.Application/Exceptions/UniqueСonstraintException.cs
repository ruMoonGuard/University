using System;

namespace University.Application.Exceptions
{
    public class UniqueСonstraintException : Exception
    {
        public UniqueСonstraintException() { }

        public UniqueСonstraintException(string message) : base(message) { }

        public UniqueСonstraintException(string message, Exception inner) { }
    }
}
