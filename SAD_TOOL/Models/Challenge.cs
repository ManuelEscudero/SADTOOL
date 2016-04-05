using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD_TOOL.Models
{
    public class Challenge : INotifyPropertyChanged
    {
        private string _name;
        private string _nameDll;
        private string _version;
        private string _description;
        private List<ParamList> _param;
        private string _type;
        private string _executeXml;

        private string _infoXml;

        public event PropertyChangedEventHandler PropertyChanged;

        public Challenge()
        {
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        public string NameDll
        {
            get { return _nameDll; }
            set
            {
                _nameDll = value;
            }
        }

        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                this.NotifyPropertyChanged("Version");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                this.NotifyPropertyChanged("Description");
            }
        }

        public string InfoXML
        {
            get { return _infoXml; }
            set
            {
                _infoXml = value;
            }
        }

        public List<ParamList> ParamList
        {
            get { return _param; }
            set
            {
                _param = value;
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }

        public string ExecuteXml
        {
            get { return _executeXml; }
            set
            {
                _executeXml = value;
            }
        }

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

}
