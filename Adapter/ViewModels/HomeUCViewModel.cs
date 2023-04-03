using Adapter.Commands;
using Adapter.Helpers;
using Adapter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Adapter.ViewModels
{
    public class HomeUCViewModel : BaseViewModel
    {
        public RelayCommand SubmitClickCommand { get; set; }
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
            public void Write(List<User>users)
            {
                _converter.Write(users);
                
            }
            public List<User> Read()
            {
                return _converter.Read();
            }
        }
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }
        private string surname;

        public string Surname
        {
            get { return surname; }
            set { surname = value; OnPropertyChanged(); }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }
        private bool jsonWriter;

        public bool JsonWriterChecked
        {
            get { return jsonWriter; }
            set { jsonWriter = value; OnPropertyChanged(); }
        }
        private bool jsonReader;

        public bool JsonReaderChecked
        {
            get { return jsonReader; }
            set { jsonReader = value; OnPropertyChanged(); }
        }

        private bool xmlWriter;

        public bool XMLWriterChecked
        {
            get { return xmlWriter; }
            set { xmlWriter = value; OnPropertyChanged(); }
        }
        private bool xmlReader;

        public bool XMLReaderChecked
        {
            get { return xmlReader; }
            set { xmlReader = value; OnPropertyChanged(); }
        }

        public HomeUCViewModel()
        {
            
            SubmitClickCommand = new RelayCommand((obj) =>
            {
                if (JsonWriterChecked || JsonReaderChecked || XMLWriterChecked || XMLReaderChecked)
                {

                    var user = new User { UserName = UserName, Surname = Surname, Email = Email, Password = Password };
                    App.UserRepo.Users.Add(user);
                    var allUsers = App.UserRepo.Users;
                    IAdapter adapter;
                    if (JsonWriterChecked)
                    {
                        Json json = new Json();
                        adapter = new JsonAdapter(json);
                        Application app = new Application(adapter);
                        adapter.Write(allUsers);
                    }
                    else if (JsonReaderChecked)
                    {
                        Json json = new Json();
                        adapter = new JsonAdapter(json);
                        Application app = new Application(adapter);
                        var myuser=adapter.Read();
                        UserName = myuser[myuser.Count - 1].UserName;
                        Surname = myuser[myuser.Count - 1].Surname;
                        Email = myuser[myuser.Count - 1].Email;
                        Password = myuser[myuser.Count - 1].Password;
                    }
                    else if (XMLWriterChecked)
                    {
                        XML xml = new XML();
                        adapter = new XmlAdapter(xml);
                        Application app = new Application(adapter);
                        adapter.Write(allUsers);
                    }
                    else
                    {
                        XML xml = new XML();
                        adapter = new XmlAdapter(xml);
                        Application app = new Application(adapter);
                        var myuser=adapter.Read();
                        UserName = myuser[myuser.Count - 1].UserName;
                        Surname = myuser[myuser.Count - 1].Surname;
                        Email = myuser[myuser.Count - 1].Email;
                        Password = myuser[myuser.Count - 1].Password;
                    }
                }
                else
                {
                    MessageBox.Show("You have to choose operation type");
                }
            });
        }

    }
}
