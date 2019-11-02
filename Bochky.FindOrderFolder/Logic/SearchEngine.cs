using Bochky.FindOrderFolder.Common;
using Bochky.FindOrderFolder.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Logic
{
    public  class SearchEngine
    {

        private readonly IReadOnlyList<Folder> _foldersToFinding;
                
        public SearchEngine(IReadOnlyList<Folder> foldersToFinding)
        {
            
            _foldersToFinding = foldersToFinding;
                       
        }

        /// <summary>
        /// Выполение поиска
        /// </summary>        
        public async Task<SearchResult> FindAsync(
            FindRequest findRequest, 
            int itetationLevel,
            CancellationToken token = default)
        {

            if (token.IsCancellationRequested)
                return null;

            if (findRequest == null)
                throw new ArgumentNullException(nameof(findRequest));

            if (findRequest.Request.Length <= 3)
                throw new MinLengthRequestException(3);

            if (itetationLevel > 4)
                throw new MaxLevelIterationException(4);

            var result = await Task.Run( ()=> FindFolderName(
                findRequest, 
                _foldersToFinding,
                itetationLevel, 
                token));            

            return new SearchResult(result, findRequest);
        }

        /// <summary>
        /// Поиск заданного имени папки из списка папок с заданной глубиной поиска.
        /// </summary>       
        public IReadOnlyList<Folder> FindFolderName(
            FindRequest findRequest,
            IReadOnlyList<Folder> searchFolderList,
            int maxLevel,
            CancellationToken token = default,
            int currentLevel = 0,
            string[] lastSearchResult = null)
        {

            string[] searchFolder = new string[0];

            string[] searchResult = new string[0];

            lastSearchResult = lastSearchResult ?? new string[0];
            
            if (token.IsCancellationRequested)
                return new List<Folder>();

            var foldersToFinding = searchFolderList.Select(item => item.DirectoryName).ToArray();
            
            foldersToFinding.AsParallel().ForAll(item => {
                               
                if (token.IsCancellationRequested) return ;

                var directoriesList = Directory.GetDirectories(item);

                searchFolder = searchFolder.Concat(directoriesList.Select(di => di.ToLower())).ToArray();                               

            });

            //TODO: Нужно оптимизировать.

            List<string> updatedsearchFolder = new List<string>();

            foreach (var sf in searchFolder) {
                foreach(var lsr in lastSearchResult)
                {
                    if (sf.Contains(lsr)) updatedsearchFolder.Add(sf);
                }
            }

            searchFolder = searchFolder.Except(updatedsearchFolder).ToArray();
            
            searchResult = searchFolder.Where(sf => sf.Contains(findRequest.Request)).ToArray();

            searchResult = searchResult.Concat(lastSearchResult).ToArray();

            if (currentLevel <= maxLevel)

            {
                currentLevel += 1;

                    return FindFolderName(
                        findRequest,
                        searchFolder
                            .Where(item => item != null)
                            .Select(item => new Folder(item))
                            ?.ToList(),
                        maxLevel,
                        token, 
                        currentLevel,
                        searchResult);
            }

            else
            {
               

                return searchResult.Where(
                    f => f.Contains(findRequest.Request))
                        ?.Select(item => new Folder(item))
                        ?.ToList();

            }

        }

    }
}
