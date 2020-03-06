using System;

namespace Bochky.FindDirectory.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() : base("Произошла ошибка при обработке данных") { }

        public BusinessException(string message) : base(message) { }
    }
}
