
namespace Bochky.FindDirectory.Common.Exceptions
{
    public class NullSearchRequestException : BusinessException
    {

        public NullSearchRequestException() : base("Поисковый запрос пуст.") { }

        public NullSearchRequestException(string message) : base(message) { }
    }
}
