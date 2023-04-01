using Adapter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace Adapter.Helpers
{
    public class FileHelper
    {
        public static void WriteUsers(List<User> users)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter("users.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    serializer.Serialize(jw, users);
                }
            }
        }
        public static List<User> ReadUsers()
        {
            List<User> users = null;
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader("users.json"))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    users = serializer.Deserialize<List<User>>(jr);
                }
            }
            return users;
        }
        public static void WriteUsersXml(List<User> Users)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (TextWriter writer = new StreamWriter("users.xml"))
            {
                serializer.Serialize(writer, Users);
            }
        }
        public static List<User> ReadUserXml()
        {
            List<User> users = null;
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (TextReader reader = new StreamReader("users.xml"))
            {
                users = (List<User>)serializer.Deserialize(reader);
            }
            return users;
        }
    }
}
