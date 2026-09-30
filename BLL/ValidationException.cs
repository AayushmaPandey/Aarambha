using System;

namespace Aarambha.BLL
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
