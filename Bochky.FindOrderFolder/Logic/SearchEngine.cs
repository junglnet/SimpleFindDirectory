using Bochky.FindOrderFolder.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Logic
{
    public  class SearchEngine
    {

        // TODO: Add throw exception

        private readonly IReadOnlyList<Folder> _foldersToFinding;

        private int _iteration;

        public SearchEngine(IReadOnlyList<Folder> foldersToFinding)
        {
            
            _foldersToFinding = foldersToFinding;

            _iteration = 0;
        }

        public async Task<SearchResult> FindAsync(
            FindRequest findRequest, 
            int maxIteration,
            CancellationToken token = default)
        {

            if (token.IsCancellationRequested)
                return null;

            var result = await Task.Run( ()=> FindDirName(
                findRequest, 
                _foldersToFinding, 
                maxIteration, 
                token)); 
            

            return new SearchResult(result, findRequest);
        }

        
        public IReadOnlyList<Folder> FindDirName(
            FindRequest findRequest, 
            IReadOnlyList<Folder> searchFolderList, 
            int maxLevel,
            CancellationToken token = default)
        {

            string[] searchFolderResult = new string[0];                      

            if (token.IsCancellationRequested)
                return new List<Folder>();

            var foldersToFinding = searchFolderList.Select(item => item.DirectoryName).ToArray();            
            
            foldersToFinding.AsParallel().ForAll(item => {

                // TODO: Need to testing.
                
                if (token.IsCancellationRequested)
                    return ;

                var tmp = Directory.GetDirectories(item);
                searchFolderResult = searchFolderResult.Concat(tmp).ToArray();

            });
            
            var findList = searchFolderResult.Where(item => item.Contains(findRequest.Request)).ToList();

            if (_iteration <= maxLevel && searchFolderResult != null)
            {
                    _iteration += 1;

                    return FindDirName(
                        findRequest, 
                        searchFolderResult
                        .Where(item => item != null)
                        .Select(item => new Folder(item))
                        ?.ToList(),
                        maxLevel,
                        token);                   
              
            }

            else            
                return findList?.Select(item => new Folder(item))?.ToList();

        }



    }
}
