using Adapter.Commands;
using Adapter.Helpers;
using Adapter.Models;
using Adapter.MyAdapter;
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
                        MyAdapter.Application app = new MyAdapter.Application(adapter);
                        adapter.Write(allUsers);
                    }
                    else if (JsonReaderChecked)
                    {
                        Json json = new Json();
                        adapter = new JsonAdapter(json);
                        MyAdapter.Application app = new MyAdapter.Application(adapter);
                        var myuser =adapter.Read();
                        UserName = myuser[myuser.Count - 1].UserName;
                        Surname = myuser[myuser.Count - 1].Surname;
                        Email = myuser[myuser.Count - 1].Email;
                        Password = myuser[myuser.Count - 1].Password;
                    }
                    else if (XMLWriterChecked)
                    {
                        XML xml = new XML();
                        adapter = new XmlAdapter(xml);
                        MyAdapter.Application app = new MyAdapter.Application(adapter);
                        adapter.Write(allUsers);
                    }
                    else
                    {
                        XML xml = new XML();
                        adapter = new XmlAdapter(xml);
                        MyAdapter.Application app = new MyAdapter.Application(adapter);
                        var myuser =adapter.Read();
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
