using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bochky.FindDirectory.Common.Interfaces
{
    public interface IServiceFactoryServer
    {
                
        IFindServiceContract FindServiceContract { get; }        
        ILoadFindFolderService LoadFindFolderService { get; }        
        ISearchEngine SearchEngine { get; }

    }
}
