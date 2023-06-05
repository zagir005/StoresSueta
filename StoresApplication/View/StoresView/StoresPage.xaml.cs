
using StoresApp.View.VendorsView;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StoresApp.View.StoresView;
using StoresApplication;
using StoresApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace StoresApp.View
{
    /// <summary>
    /// Логика взаимодействия для StoresPage.xaml
    /// </summary>
    public partial class StoresPage : Page
    {
        public string tag = "Stores";

        StoresContext storesContext = new StoresContext();

        Store selectedItem;
        public Store SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;


        List<Store> dataGridItems;
        public List<Store> DataGridVendorItems
        {
            get { return dataGridItems; }
            set
            {
                dataGridItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataGridVendorItems"));
            }
        }

        public StoresPage()
        {
            InitializeComponent();

            this.DataContext = this;
            using(StoresContext context = new StoresContext())
            {
                
                List<Store> stores = context.Stores.Include(u => u.Ownerships).ToList();
                stores.ForEach(store =>
                {
                    int storeId = store.Id;
                    List<Ownership> ownerships = store.Ownerships.ToList().FindAll(u => u.StoreId == storeId);
                    decimal deposit = 0;
                    ownerships.ForEach(ownership =>
                    {
                        deposit += ownership.Deposit;
                    });
                    store.Capital = deposit;
                    context.Stores.Update(store);
                    deposit = 0;
                }
                );
                context.SaveChanges();
                DataGridVendorItems = context.Stores.ToList();
            }


        }

        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить элемент списка? Будут так же удалены связанные элементы из других таблиц", "Сообщение", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    storesContext.Remove(SelectedItem);
                    storesContext.SaveChanges();
                    dataGridVendors.ItemsSource = new StoresContext().Stores.ToList();
                    break;
                default:
                    return;
            }
        }
        private void UpdateItemClick(object sender, RoutedEventArgs e)
        {

            StoreItemWindow itemWindow = new StoreItemWindow(SelectedItem);
            itemWindow.Width = 300;
            itemWindow.Height = 450;
            itemWindow.Closing += UpdateWindow_Closed;
            itemWindow.ShowDialog();

        }

        private void UpdateWindow_Closed(object? sender, CancelEventArgs e)
        {
            dataGridVendors.ItemsSource = new StoresContext().Stores.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            StoreItemWindow itemWindow = new StoreItemWindow();
            itemWindow.Width = 300;
            itemWindow.Height = 450;
            itemWindow.Closing += UpdateWindow_Closed;
            itemWindow.ShowDialog();

        }


    }

}

