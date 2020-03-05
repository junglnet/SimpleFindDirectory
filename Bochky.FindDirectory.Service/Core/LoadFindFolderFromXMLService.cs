using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bochky.FindDirectory.Service.Core
{
    public class LoadFindFolderFromXMLService : ILoadFindFolderService
    {
        public async Task<IEnumerable<Folder>> LoadDirectoriesAsync(string filePath)
        {

            XmlSerializer formatter = new XmlSerializer(typeof(Folder[]));

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {

                return await Task.Run(() => (Folder[])formatter.Deserialize(fs));

            }
        }
    }
}
