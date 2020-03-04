using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Bochky.FindDirectory.Services
{
       
    /// <summary>
    /// Класс реализует логику открытия директории
    /// </summary>
    public class DirectoryOpenerService
    {

        public static async Task OpenDirectory(Folder folder, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            if (folder == null)
                throw new ArgumentNullException(nameof(folder));
           
            await Task.Run(() => Process.Start(
                            "explorer", folder.DirectoryPath.ToPath()));
                        

        }

    }
}
