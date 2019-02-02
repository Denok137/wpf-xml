using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_XML
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Chapter> _listChap;

        public ObservableCollection<Chapter> ListChap
        {
            get { return _listChap; }
            set
            {
                _listChap = value;
                RaisePropertyChanged(); //будем уведомлять что произошло изменение
            }
        }

        public MainWindowViewModel()
        {
            ListChap = new ObservableCollection<Chapter>();
        }

        public void FillList (string path) {
            try
            {
                ListChap = ChapterProvider.ParseXml(path); //заполняем коллекцию данными
            }
            catch {
                System.Windows.MessageBox.Show("Был выбран некорректный файл");
                //если надо обработку ошибок то лучше использовать MVVM Light Toolkit или подобные вещи
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(caller));
        }

    }
}
