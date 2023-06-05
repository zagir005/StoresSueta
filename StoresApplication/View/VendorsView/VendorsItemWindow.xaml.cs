
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
using System.Numerics;
using Microsoft.IdentityModel.Tokens;
using StoresApplication.Models;
using StoresApplication;

namespace StoresApp.View.VendorsView
{
    /// <summary>
    /// Логика взаимодействия для VendorsItemWindow.xaml
    /// </summary>
    public partial class VendorsItemWindow : Window
    {
       
        public Vendor vendor;
        public bool itAdd = false;
        public VendorsItemWindow()
        {
            itAdd = true;
            vendor = new Vendor();
            DataContext = vendor;
            InitializeComponent();
        }

        public VendorsItemWindow(Vendor _vendor)
        {
            vendor = new StoresContext().Vendors.ToList().Find(
                item => item.Id == _vendor.Id)!;
            DataContext = vendor;
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
            if (!isValideData(vendor))
            {
                MessageBox.Show("Некорректно введены данные");
            }
            else
            {
                using(StoresContext storesContext = new StoresContext())
                {
                    storesContext.Add(vendor);
                    storesContext.SaveChanges();
                }
                Close();
            }
        }
        private void updateInvoke()
        {
            if (!isValideData(vendor))
            {
                MessageBox.Show("Не все данные заполнены", "Ошибка");
            }
            else
            {
                using (StoresContext storesContext = new StoresContext())
                {
                    storesContext.Update(vendor);
                    storesContext.SaveChanges();
                }
                Close();
            }
        }

        private bool isValideData(Vendor vendor)
        {
            if (
                vendor.Name.IsNullOrEmpty() ||
                vendor.Address.IsNullOrEmpty() ||
                vendor.Telephone.IsNullOrEmpty()
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
