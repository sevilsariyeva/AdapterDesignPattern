using Adapter.Helpers;
using Adapter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Adapter.MyAdapter
{
    interface IAdapter
    {
        void Write(List<User> users);
        List<User> Read();
    }
    class JsonAdapter : IAdapter
    {
        private Json _json;

        public JsonAdapter(Json json)
        {
            _json = json;
        }

        public List<User> Read()
        {
            return _json.Read();
        }

        public void Write(List<User> users)
        {
            _json.Write(users);
        }
    }
    class XmlAdapter : IAdapter
    {
        private XML _xml { get; set; }
        public XmlAdapter(XML xml)
        {
            _xml = xml;
        }

        public List<User> Read()
        {
            return _xml.Read();
        }

        public void Write(List<User> users)
        {
            _xml.Write(users);
        }
    }
    class Json
    {
        public List<User> Read()
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
        public void Write(List<User> users)
        {
            FileHelper.WriteUsers(users);
        }
    }
    class XML
    {
        public List<User> Read()
        {
            List<User> users = null;
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (TextReader reader = new StreamReader("users.xml"))
            {
                users = (List<User>)serializer.Deserialize(reader);
            }
            return users;
        }

        public void Write(List<User> users)
        {
            FileHelper.WriteUsersXml(users);
        }
    }
    class Converter
    {
        private readonly IAdapter _adapter;
        public Converter(IAdapter adapter)
        {
            _adapter = adapter;
        }
        public void Write(List<User> users)
        {
            _adapter.Write(users);
        }
        public List<User> Read()
        {
            return _adapter.Read();
        }
    }
    class Application
    {
        private readonly Converter _converter;
        public Application(IAdapter adapter)
        {
            _converter = new Converter(adapter);
        }
        public void Write(List<User> users)
        {
            _converter.Write(users);

        }
        public List<User> Read()
        {
            return _converter.Read();
        }
    }
}
