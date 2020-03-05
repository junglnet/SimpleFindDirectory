using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;

namespace Bochky.FindDirectory.Core.Services
{
    public class LoadChekedFolderListFromXMLService : ILoadChekedFolderListService
    {
        public async Task<IEnumerable<ChekedFolder>> LoadChekedFolderList(string filePath)
        {

            XmlSerializer formatter = new XmlSerializer(typeof(ChekedFolder[]));

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
              
                return await Task.Run(() => (ChekedFolder[])formatter.Deserialize(fs));

            }

        }
    }
}
