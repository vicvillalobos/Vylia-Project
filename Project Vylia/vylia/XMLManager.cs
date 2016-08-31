using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project_Vylia.vylia
{
    public class XMLManager<T>
    {

        public Type Type;

        public T Load(string path)
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
                Console.WriteLine("### INSTANCIA DE OBJETO EN [{0}] CARGADA  ###",path);
            }
            return instance;
        }

        public void Save(string path,Object obj)
        {
            using(StreamWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}
