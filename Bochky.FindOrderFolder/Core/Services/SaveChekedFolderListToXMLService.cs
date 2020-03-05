using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;

namespace Bochky.FindDirectory.Core.Services
{
    public class SaveChekedFolderListToXMLService : ISaveChekedFolderListService
    {
        public async Task SaveChekedFolderList(IEnumerable<ChekedFolder> chekedFolders, string filePath)
        {

            XmlSerializer formatter = new XmlSerializer(typeof(ChekedFolder[]));

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await Task.Run(() => formatter.Serialize(fs, chekedFolders.ToArray()));
            }

        }
    }
}
