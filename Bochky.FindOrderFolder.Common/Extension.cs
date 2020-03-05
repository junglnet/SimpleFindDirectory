using Bochky.FindDirectory.Common.Entities;
using System;

namespace Bochky.FindDirectory.Common
{
    public static class Extension
    {

        public static string ToPath(this string item) =>
            "\"" + item + "\"";

        

    }

    
}
