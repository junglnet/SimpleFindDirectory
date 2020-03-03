using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Exceptions;
using Bochky.FindDirectory.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bochky.FindDirectory.Service.Core
{
    /// <summary>
    /// Класс реализует логику поиска 
    /// </summary>
    public class SearchEngine
    {
             
        /// <summary>
        /// Выполение поиска
        /// </summary>        
        public async Task<SearchResult> FindAsync(
            FindRequest findRequest,
            IReadOnlyList<Folder> foldersToFinding,
            bool isDeepSearch,
            CancellationToken token = default)
        {

            if (token.IsCancellationRequested)
                return null;

            if (findRequest == null)
                throw new ArgumentNullException(nameof(findRequest));

            if (findRequest.Request.Length <= 3)
                throw new MinLengthRequestException(3);

            var findResult = isDeepSearch ? 
                    await Task.Run(
                        () => DeepFindFolderName(findRequest, foldersToFinding, token)) :
                    await Task.Run(
                        () => FindFolderNameOnKnowLevel(findRequest, foldersToFinding, token));

            return new SearchResult(findRequest, findResult, findResult.Count > 0 ? true : false);            
           
        }

        private IReadOnlyList<Folder> FindFolderName(
            FindRequest findRequest,
            IReadOnlyList<Folder> searchFolderList,
            CancellationToken token = default)
        {

            string[] searchFolder = new string[0];

            string[] searchResult = new string[0];

            if (token.IsCancellationRequested)
                return new List<Folder>();

           
            // Спросить у Антона.
            //foldersToFinding.AsParallel().ForAll(item =>
            //{

            //    if (token.IsCancellationRequested) return;

            //    var directoriesList = Directory.GetDirectories(item).AsParallel();

            //    directoriesList = directoriesList.Select(di => di.ToLower());

            //    searchFolder = searchFolder.Concat(directoriesList).ToArray();

            //});

            foreach(var item in searchFolderList.Select(item => item.DirectoryName))
            {
                if (token.IsCancellationRequested) break;

                var directoriesList = Directory.GetDirectories(item).AsParallel();

                directoriesList = directoriesList.Select(di => di.ToLower());

                searchFolder = searchFolder.Concat(directoriesList).ToArray();
            }

            

            // Поиск во вложенных директориях соответствия поисковому запросу.          
            searchResult = searchFolder.Where(sf => sf.Contains(findRequest.Request)).ToArray();

            return searchResult.Where(
                f => f.Contains(findRequest.Request))
                    ?.Select(item => new Folder(item.ToLower()))
                    ?.ToList();
        }

        private IReadOnlyList<Folder> FindFolderNameOnKnowLevel(
            FindRequest findRequest,
            IReadOnlyList<Folder> searchFolderList,
            CancellationToken token = default,
            int currentLevel = 0,
            int maxLevel = 1)
        {

            if (token.IsCancellationRequested)
                return new List<Folder>();

            IReadOnlyList<Folder> searchResult;
                        
            string[] searchFolder = new string[0];

            var foldersToFinding = searchFolderList.Select(item => item.DirectoryName).ToArray();

            searchResult = FindFolderName(findRequest, searchFolderList, token);

            if (searchResult.Count == 0 && currentLevel < maxLevel)
            {
                currentLevel += 1;

                // Создание списка вложенных директорий для каждой директории в foldersToFinding.
                foreach (var item in foldersToFinding)
                {
                    if (token.IsCancellationRequested) break;

                    var directoriesList = Directory.GetDirectories(item);

                    searchFolder = searchFolder.Concat(directoriesList.Select(di => di.ToLower())).ToArray();
                }

                return FindFolderNameOnKnowLevel(findRequest,
                        searchFolder
                            .Where(item => item != null)
                            .Select(item => new Folder(item.ToLower()))
                            ?.ToList(),
                        token,
                        currentLevel,
                        maxLevel);
            }

            else
                return searchResult;

        }

        private IReadOnlyList<Folder> DeepFindFolderName(
            FindRequest findRequest,
            IReadOnlyList<Folder> searchFolderList,
            CancellationToken token = default,            
            int currentLevel = 0,
            IReadOnlyList<Folder> lastSearchResult = null,
            int maxLevel = 2)
        {

            if (token.IsCancellationRequested)
                return new List<Folder>();

            IReadOnlyList<Folder> searchResult;

            string[] searchFolder = new string[0];

            lastSearchResult = lastSearchResult ?? new List<Folder>();

            var foldersToFinding = searchFolderList.Select(item => item.DirectoryName).ToArray();

            // исключаем из поиска ранее найденые результаты
            List<string> searchFolderToEscape = new List<string>();

            foreach (var sf in foldersToFinding)
            {
                foreach (var lsr in lastSearchResult.Select(item => item.DirectoryName).ToArray())
                {
                    if (sf.Contains(lsr)) searchFolderToEscape.Add(sf);
                }
            }

            foldersToFinding = foldersToFinding.Except(searchFolderToEscape).ToArray();

            searchResult = FindFolderName(
                findRequest, 
                foldersToFinding
                    .Where(item => item != null)
                    .Select(item => new Folder(item.ToLower()) )
                    ?.ToList(), token);

            searchResult = searchResult.Concat(lastSearchResult).ToArray();

            if (currentLevel < maxLevel)
            {
                currentLevel += 1;

                // Создание списка вложенных директорий для каждой директории в foldersToFinding.

                foreach(var item in foldersToFinding)
                {
                    if (token.IsCancellationRequested) break;

                    var directoriesList = Directory.GetDirectories(item);

                    searchFolder = searchFolder.Concat(directoriesList.Select(di => di.ToLower())).ToArray();
                }

               

                return DeepFindFolderName(
                        findRequest,
                        searchFolder
                            .Where(item => item != null)
                            .Select(item => new Folder(item.ToLower()))
                            ?.ToList(),
                        token,
                        currentLevel,
                        searchResult,
                        maxLevel);
            }

            else
                return searchResult;

        }

    }
}
