
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Microsoft.IdentityModel.Tokens;
using StoresApplication.Models;
using StoresApplication;

namespace StoresApp.View.StoresView
{
    /// <summary>
    /// Логика взаимодействия для StoreItemWindow.xaml
    /// </summary>
    public partial class StoreItemWindow : Window
    {
        
        public Store store;
        public bool itAdd = false;
        public StoreItemWindow()
        {
            itAdd = true;
            store = new Store();
            DataContext = store;
            InitializeComponent();
        }

        public StoreItemWindow(Store _store)
        {
            store = new StoresContext().Stores.ToList().Find(
                item => item.Id == _store.Id)!;
            DataContext = store;
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (itAdd)
            {
                addInvoke();
            }
            else
            {
                updateInvoke();
            }
        }

        private void addInvoke()
        {
            if (!isValideData(store))
            {
                MessageBox.Show("Некорректно введены данные");
            }
            else
            {
                using (StoresContext storesContext = new StoresContext())
                {
                    storesContext.Add(store);
                    storesContext.SaveChanges();
                }
                Close();
            }
        }
        private void updateInvoke()
        {
            if (!isValideData(store))
            {
                MessageBox.Show("Не все данные заполнены", "Ошибка");
            }
            else
            {
                using (StoresContext storesContext = new StoresContext())
                {
                    storesContext.Update(store);
                    storesContext.SaveChanges();
                }
                Close();
            }
        }

        private bool isValideData(Store store)
        {
            if (
                store.Name.IsNullOrEmpty() ||
                store.Address.IsNullOrEmpty() ||
                store.Telephone.IsNullOrEmpty() ||
                store.CityArea.IsNullOrEmpty() ||
                store.Profile.IsNullOrEmpty()
                )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
