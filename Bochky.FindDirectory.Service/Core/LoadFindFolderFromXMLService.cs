using Bochky.FindDirectory.Common.Entities;
using Bochky.FindDirectory.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bochky.FindDirectory.Service.Core
{
    public class LoadFindFolderFromXMLService : ILoadFindFolderService
    {
        public Task<IEnumerable<Folder>> LoadDirectoriesAsync(string path)
        {

            XmlSerializer formatter = new XmlSerializer(typeof(Folder[]));

            Folder[] searchPoints;

            using (FileStream fs = new FileStream("SearchPoints.xml", FileMode.OpenOrCreate))
            {
                searchPoints = (Folder[])formatter.Deserialize(fs);

            }

            return null;
        }
    }
}
