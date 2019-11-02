using System;

namespace Bochky.FindOrderFolder.Exceptions
{
    public class MaxLevelIterationException : Exception
    {

        public MaxLevelIterationException(string message) : base(message) { }

        public MaxLevelIterationException(int maxlevel) : base("Максимальная глубина " + maxlevel) { }

    }
}
