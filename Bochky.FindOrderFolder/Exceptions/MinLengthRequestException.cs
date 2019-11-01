using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Exceptions
{
    public class MinLengthRequestException : Exception
    {
        public MinLengthRequestException(string message) : base(message) { }
    }
}
