using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Common.Entities
{
    public static class Extension
    {

        public static string ToPath(this String item) =>
            "\"" + item + "\"";

    }
}
