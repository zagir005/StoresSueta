using Microsoft.IdentityModel.Tokens;
using StoresApplication;
using StoresApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace StoresApp.View.StoreOwnerView
{
    /// <summary>
    /// Логика взаимодействия для StoreOwnerUpdateWindow.xaml
    /// </summary>
    public partial class StoreOwnerUpdateWindow : Window
    {
        public StoreOwner? storeOwner;
        
        public StoreOwnerUpdateWindow()
        {
            storeOwner = new StoreOwner();
            DataContext = storeOwner;
            InitializeComponent();  
        }

        public StoreOwnerUpdateWindow(StoreOwner _storeOwner)
        {
            storeOwner = new StoresContext().StoreOwners.ToList().Find(
                item => item.Id == _storeOwner.Id);
            DataContext = storeOwner;
            InitializeComponent();
        }
        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            addInvoke();   
        }

        private void addInvoke()
        {
            if (
                storeOwner.Name.IsNullOrEmpty() ||
                storeOwner.BirthDate == null ||
                storeOwner.BirthDate >= DateTime.Now ||
                storeOwner.CityArea.IsNullOrEmpty() ||
                storeOwner.Address.IsNullOrEmpty() ||
                storeOwner.Telephone.IsNullOrEmpty()
                )
            {
                MessageBox.Show("Не все данные заполнены", "Ошибка");
            }
            else
            {
                using (StoresContext storesContext = new StoresContext())
                {
                    storesContext.Update(storeOwner);
                    storesContext.SaveChanges();
                }
                Close();
            }
        }
    }

   
}
