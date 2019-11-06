using Bochky.FindOrderFolder.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bochky.FindOrderFolder.Logic
{
    public class DirectoryOpener
    {

        public static async Task OpenDirectory(Folder folder, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            if (folder == null)
                throw new ArgumentNullException(nameof(folder));

            await Task.Run(() => Process.Start(
                            "explorer", folder.DirectoryName));
        }

    }
}
