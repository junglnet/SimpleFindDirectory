using System;

namespace Bochky.FindOrderFolder.Exceptions
{
    public class MinLengthRequestException : Exception
    {
        public MinLengthRequestException(string message) : base(message) { }

        public MinLengthRequestException(int minSymbolCount) 
            : base("Должно быть указано не менее " + minSymbolCount  + " символов.") { }
    }
}
