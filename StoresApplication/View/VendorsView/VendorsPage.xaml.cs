
using StoresApp.View.VendorsView;
using StoresApplication;
using StoresApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StoresApp.View
{
    /// <summary>
    /// Логика взаимодействия для VendorsPage.xaml
    /// </summary>
    public partial class VendorsPage : Page
    {
        public string tag = "Vendors";

        StoresContext storesContext = new StoresContext();

        Vendor selectedItem;
        public Vendor SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;


        List<Vendor> dataGridItems;
        public List<Vendor> DataGridVendorItems
        {
            get { return dataGridItems; }
            set
            {
                dataGridItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataGridVendorItems"));
            }
        }

        public VendorsPage()
        {
            InitializeComponent();

            DataContext = this;
            dataGridVendors.ItemsSource = storesContext.Vendors.ToList();

        }

        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить элемент списка? Будут так же удалены связанные элементы из других таблиц", "Сообщение", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    storesContext.Remove(selectedItem);
                    storesContext.SaveChanges();
                    dataGridVendors.ItemsSource = new StoresContext().Vendors.ToList();
                    break;
                default:
                    return;
            }

        }
        private void UpdateItemClick(object sender, RoutedEventArgs e)
        {

            VendorsItemWindow itemWindow = new VendorsItemWindow(SelectedItem);
            itemWindow.Width = 300;
            itemWindow.Height = 400;
            itemWindow.Closing += UpdateWindow_Closed;
            itemWindow.ShowDialog();
            
        }

        private void UpdateWindow_Closed(object? sender, CancelEventArgs e)
        {
            dataGridVendors.ItemsSource = new StoresContext().Vendors.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            VendorsItemWindow itemWindow = new VendorsItemWindow();
            itemWindow.Width = 300;
            itemWindow.Height = 400;
            itemWindow.Closing += UpdateWindow_Closed;
            itemWindow.ShowDialog();
        
        }


    }

}
