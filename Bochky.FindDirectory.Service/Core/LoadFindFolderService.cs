using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Bochky.FindDirectory.Service.Core
{
    /// <summary>
    /// Класс реализует логику загрузки файла с настройками
    /// </summary>
    public class LoadFindFolderService : ILoadFindFOlderService
    {
       
        public async Task<IEnumerable<Folder>> LoadDirectoriesAsync(string path)
        {
            
            if (path == null)
                throw new ArgumentNullException("fullpath");

            if (File.Exists(path) == false)
                throw new FileNotFoundException("Файл не найден: " + path);

            List<Folder> directories = new List<Folder>();

            using ( StreamReader sr = new StreamReader(path, Encoding.UTF8) )
            {
                
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
                {

                    if ( Directory.Exists(line) )                                        
                        directories.Add(new Folder(line.ToLower()));
                }
            }

            return directories;

        }


    }
}
