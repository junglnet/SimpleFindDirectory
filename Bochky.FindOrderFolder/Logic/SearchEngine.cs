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
                        () => DeepFindFolderName(findRequest, _foldersToFinding, token)) :
                    await Task.Run(
                        () => FindFolderNameOnKnowLevel(findRequest, _foldersToFinding, token));            

            return new SearchResult(findResult, findRequest);
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

            var foldersToFinding = searchFolderList.Select(item => item.DirectoryName).ToArray();

            // Создание списка вложенных директорий для каждой директории в foldersToFinding.
            foldersToFinding.AsParallel().ForAll(item => {

                if (token.IsCancellationRequested) return;

                var directoriesList = Directory.GetDirectories(item);

                searchFolder = searchFolder.Concat(directoriesList.Select(di => di.ToLower())).ToArray();

            });

            // Поиск во вложенных директориях соответствия поисковому запросу.          
            searchResult = searchFolder.Where(sf => sf.Contains(findRequest.Request)).ToArray();

            return searchResult.Where(
                f => f.Contains(findRequest.Request))
                    ?.Select(item => new Folder(item))
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
                foldersToFinding.AsParallel().ForAll(item =>
                {

                    if (token.IsCancellationRequested) return;

                    var directoriesList = Directory.GetDirectories(item);

                    searchFolder = searchFolder.Concat(directoriesList.Select(di => di.ToLower())).ToArray();

                });

                return FindFolderNameOnKnowLevel(findRequest,
                        searchFolder
                            .Where(item => item != null)
                            .Select(item => new Folder(item))
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
                    .Select(item => new Folder(item))
                    ?.ToList(), token);

            searchResult = searchResult.Concat(lastSearchResult).ToArray();

            if (currentLevel < maxLevel)
            {
                currentLevel += 1;

                // Создание списка вложенных директорий для каждой директории в foldersToFinding.
                foldersToFinding.AsParallel().ForAll(item =>
                {

                    if (token.IsCancellationRequested) return;

                    var directoriesList = Directory.GetDirectories(item);

                    searchFolder = searchFolder.Concat(directoriesList.Select(di => di.ToLower())).ToArray();

                });

                return DeepFindFolderName(
                        findRequest,
                        searchFolder
                            .Where(item => item != null)
                            .Select(item => new Folder(item))
                            ?.ToList(),
                        token,
                        currentLevel,
                        searchResult,
                        maxLevel);
            }

            else
                return searchResult;

        }

        //private IReadOnlyList<Folder> DeepFindFolderName(
        //    FindRequest findRequest,
        //    IReadOnlyList<Folder> searchFolderList,
        //    CancellationToken token = default,
        //    int currentLevel = 0,
        //    int maxLevel = 2)
        //{

        //    if (token.IsCancellationRequested)
        //        return new List<Folder>();

        //    IReadOnlyList<Folder> searchResult;

        //    string[] searchFolder = new string[0];

        //    var foldersToFinding = searchFolderList.Select(item => item.DirectoryName).ToArray();

        //    //        List<string> searchFolderToEscape = new List<string>();

        //    //        foreach (var sf in searchFolder)
        //    //        {
        //    //            foreach (var lsr in lastSearchResult)
        //    //            {
        //    //                if (sf.Contains(lsr)) searchFolderToEscape.Add(sf);
        //    //            }
        //    //        }

        //    //        searchFolder = searchFolder.Except(searchFolderToEscape).ToArray();

        //    //    }


        //    //    // Поиск во вложенных директориях соответствия поисковому запросу.          
        //    //    searchResult = searchFolder.Where(sf => sf.Contains(findRequest.Request)).ToArray();

        //    //    // Объединение с предыдущими результами поиска.
        //    //    // Только если задан углубленый поиск.
        //    //    if (isDeepSearch)
        //    //        searchResult = searchResult.Concat(lastSearchResult).ToArray();

        //    if (currentLevel < maxLevel)
        //    {
        //        currentLevel += 1;

        //        // Создание списка вложенных директорий для каждой директории в foldersToFinding.
        //        foldersToFinding.AsParallel().ForAll(item =>
        //        {

        //            if (token.IsCancellationRequested) return;

        //            var directoriesList = Directory.GetDirectories(item);

        //            searchFolder = searchFolder.Concat(directoriesList.Select(di => di.ToLower())).ToArray();

        //        });

        //        return FindFolderNameOnKnowLevel(findRequest,
        //                searchFolder
        //                    .Where(item => item != null)
        //                    .Select(item => new Folder(item))
        //                    ?.ToList(),
        //                token,
        //                currentLevel,
        //                maxLevel);
        //    }

        //    else
        //        return searchResult;

        //}


        /// <summary>
        /// Поиск заданного имени папки из списка папок на заданном уровне
        /// </summary>       
        //private IReadOnlyList<Folder> FindFolderName1(
        //    FindRequest findRequest,
        //    IReadOnlyList<Folder> searchFolderList,            
        //    CancellationToken token = default,
        //    int currentLevel = 0,
        //    int maxLevel = 1)
        //{

        //    string[] searchFolder = new string[0];

        //    string[] searchResult = new string[0];

        //    if (token.IsCancellationRequested)
        //        return new List<Folder>();

        //    var foldersToFinding = searchFolderList.Select(item => item.DirectoryName).ToArray();

        //    // Создание списка вложенных директорий для каждой директории в foldersToFinding.
        //    foldersToFinding.AsParallel().ForAll(item => {

        //        if (token.IsCancellationRequested) return ;

        //        var directoriesList = Directory.GetDirectories(item);

        //        searchFolder = searchFolder.Concat(directoriesList.Select(di => di.ToLower())).ToArray();                               

        //    });

        //    // Поиск во вложенных директориях соответствия поисковому запросу.          
        //    searchResult = searchFolder.Where(sf => sf.Contains(findRequest.Request)).ToArray();            

        //    if (searchResult.Length == 0 && currentLevel <= maxLevel)
        //    {
        //        currentLevel += 1;

        //        return FindFolderName(
        //            findRequest,
        //            searchFolder
        //                .Where(item => item != null)
        //                .Select(item => new Folder(item))
        //                ?.ToList(),                    
        //            token,
        //            currentLevel);
        //    }

        //    else
        //        return searchResult.Where(
        //            f => f.Contains(findRequest.Request))
        //                ?.Select(item => new Folder(item))
        //                ?.ToList();

        //}

        //public IReadOnlyList<Folder> DeepFindFolderName(
        //    FindRequest findRequest,
        //    IReadOnlyList<Folder> searchFolderList,            
        //    CancellationToken token = default,
        //    int currentLevel = 0,
        //    string[] lastSearchResult = null,
        //    int maxLevel = 2)
        //{

        //    string[] searchFolder = new string[0];

        //    string[] searchResult = new string[0];

        //    lastSearchResult = lastSearchResult ?? new string[0];

        //    if (token.IsCancellationRequested)
        //        return new List<Folder>();

        //    var foldersToFinding = searchFolderList.Select(item => item.DirectoryName).ToArray();

        //    // Создание списка вложенных директорий для каждой директории в foldersToFinding.
        //    foldersToFinding.AsParallel().ForAll(item => {

        //        if (token.IsCancellationRequested) return;

        //        var directoriesList = Directory.GetDirectories(item);

        //        searchFolder = searchFolder.Concat(directoriesList.Select(di => di.ToLower())).ToArray();

        //    });

        //    //TODO: Нужно оптимизировать.
        //    // Исклюяение из поиска найденных на предыдущей категории директорий.
        //    // Только для глубого поиска
        //    if (isDeepSearch)
        //    {
        //        List<string> searchFolderToEscape = new List<string>();

        //        foreach (var sf in searchFolder)
        //        {
        //            foreach (var lsr in lastSearchResult)
        //            {
        //                if (sf.Contains(lsr)) searchFolderToEscape.Add(sf);
        //            }
        //        }

        //        searchFolder = searchFolder.Except(searchFolderToEscape).ToArray();

        //    }


        //    // Поиск во вложенных директориях соответствия поисковому запросу.          
        //    searchResult = searchFolder.Where(sf => sf.Contains(findRequest.Request)).ToArray();

        //    // Объединение с предыдущими результами поиска.
        //    // Только если задан углубленый поиск.
        //    if (isDeepSearch)
        //        searchResult = searchResult.Concat(lastSearchResult).ToArray();

        //    // Если установлен маркер углубленного поиска и не достигнут максимальный уроверь, 
        //    // то увеличить глубину и повторить.             
        //    if (isDeepSearch && currentLevel <= maxLevel)
        //    {
        //        currentLevel += 1;

        //        return DeepFindFolderName(
        //            findRequest,
        //            searchFolder
        //                .Where(item => item != null)
        //                .Select(item => new Folder(item))
        //                ?.ToList(),
        //            isDeepSearch,
        //            token,
        //            currentLevel,
        //            searchResult);
        //    }

        //    // Если не установлен маркер глубого поиска и найдено ли совпадение на текущей глубине
        //    // и не достигнута ли максимальная глубина, то повторить поиск на новой глубине
        //    else if (!isDeepSearch && searchResult.Length == 0 && currentLevel <= maxLevel)
        //    {
        //        currentLevel += 1;

        //        return DeepFindFolderName(
        //            findRequest,
        //            searchFolder
        //                .Where(item => item != null)
        //                .Select(item => new Folder(item))
        //                ?.ToList(),
        //            isDeepSearch,
        //            token,
        //            currentLevel,
        //            searchResult);
        //    }

        //    else
        //    {

        //        return searchResult.Where(
        //            f => f.Contains(findRequest.Request))
        //                ?.Select(item => new Folder(item))
        //                ?.ToList();

        //    }

        //}
    }
}
