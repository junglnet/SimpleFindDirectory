using Bochky.FindOrderFolder.Common.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Logic
{
    /// <summary>
    /// Класс реализует логику загрузки файла с настройками
    /// </summary>
    public class LoadFindFolderService
    {
       
        public static async Task<IReadOnlyList<Folder>> LoadDirectoriesAsync(string path)
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
                        directories.Add(new Folder(line));
                }
            }

            return directories;

        }


    }
}
