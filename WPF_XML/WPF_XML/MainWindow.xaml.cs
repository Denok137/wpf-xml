using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace WPF_XML
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //открываем окно выбора
            openFileDialog.Filter = "xml files (*.xml)|*.xml"; //фильтр форматов

            if (openFileDialog.ShowDialog() == true) {
                MainWindowViewModel view = (MainWindowViewModel)DataContext; //добираемся до вьюмодель
                view.FillList(openFileDialog.FileName); //вызываем заполнение коллекции
            }
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel view = (MainWindowViewModel)DataContext;

            if (view.ListChap.Count < 1)
            {
                MessageBox.Show("XML не был загружен в программу!");
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "Excel.xlsx";
                saveFileDialog.Filter = "excel files (*.xlsx)|*.xlsx";

                if (saveFileDialog.ShowDialog() == true)
                {
                    Excel.GenerateExcelFile(view.ListChap, saveFileDialog.FileName); //вызываем генерацию экселя
                }
            }

        }
    }
}
