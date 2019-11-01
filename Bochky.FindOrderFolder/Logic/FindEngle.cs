using Bochky.FindOrderFolder.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Logic
{
    public  class FindEngle
    {

        // TODO: Add throw exception

        private readonly IReadOnlyList<Folder> _foldersToFinding;

        private int _iteration;

        public FindEngle(IReadOnlyList<Folder> foldersToFinding)
        {
            
            _foldersToFinding = foldersToFinding;

            _iteration = 0;
        }

        public async Task<SearchResult> FindAsync(FindRequest findRequest, int maxIteration)
        {
                                 
            var result = await Task.Run( ()=> FindInDir(findRequest, _foldersToFinding, maxIteration) ); 
            

            return new SearchResult(result, findRequest);
        }

        
        public IReadOnlyList<Folder> FindInDir(FindRequest findRequest, IReadOnlyList<Folder> searchFolderList, int maxLevel)
        {

            string[] searchFolderResult = new string[0];

            var foldersToFinding = searchFolderList.Select(item => item.DirectoryName).ToArray();            
            
            foldersToFinding.AsParallel().ForAll(item => {

                var tmp = Directory.GetDirectories(item);
                
               
                    searchFolderResult = searchFolderResult.Concat(tmp).ToArray();

            });
            
            var findList = searchFolderResult.Where(item => item.Contains(findRequest.Request)).ToList();

            if (findList.Count == 0 && _iteration <= maxLevel && searchFolderResult != null)
            {
              

                    _iteration += 1;

                    return FindInDir(findRequest, searchFolderResult.Where(item => item != null).Select(item => new Folder(item))?.ToList(), maxLevel);

                   
              
            }

            else            
                return findList?.Select(item => new Folder(item))?.ToList();

        }



    }
}
