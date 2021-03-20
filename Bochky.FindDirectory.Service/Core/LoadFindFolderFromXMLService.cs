using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bochky.FindDirectory.Service.Core
{
    public class LoadFolderListFromXML : ILoadFolderList
    {
        private readonly string _filepath;
        public LoadFolderListFromXML(string filePath)
        {
            _filepath = filePath;
        }

        public async Task<IEnumerable<Folder>> LoadDirectoriesAsync()
        {

            XmlSerializer formatter = new XmlSerializer(typeof(Folder[]));

            using (FileStream fs = new FileStream(_filepath, FileMode.OpenOrCreate))
            {

                return await Task.Run(() => (Folder[])formatter.Deserialize(fs));

            }
        }
    }
}
