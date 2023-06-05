using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using StoresApp.View.DeliveryView;
using StoresApplication;
using StoresApplication.Models;

namespace StoresApp.View
{
    /// <summary>
    /// Логика взаимодействия для DeliveryPage.xaml
    /// </summary>
    public partial class DeliveryPage : Page
    {
        public string tag = "Delivery";
        
        

        Delivery selectedItem;

        StoresContext justStoresContext = new StoresContext();


        public Delivery SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        List<Delivery> dataGridItems;
        public List<Delivery> DataGridDeliveriesItems
        {
            get { return dataGridItems; }
            set
            {
                dataGridItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataGridDeliveriesItems"));
            }
        }

        public DeliveryPage()
        {

           
            InitializeComponent();
            DataContext = this;
            loadDeliviries();
        }

        private void loadDeliviries()
        {
            using (StoresContext storesContext = new StoresContext())
            {
                List<Delivery> items = storesContext.Deliveries.Include(u => u.Vendor).Include(u => u.Store).ToList();
                dataGridDeliveries.ItemsSource = items;
                DataGridDeliveriesItems = items;
            }
        }

        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить элемент списка?", "Сообщение", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    
                    using (StoresContext storesContext = new StoresContext())
                    {
                        justStoresContext.Remove(SelectedItem);
                        justStoresContext.SaveChanges();
                    }
                    loadDeliviries();
                    dataGridDeliveries.ItemsSource = DataGridDeliveriesItems;
                    break;
                default:
                    return;
            }

        }
        private void UpdateItemClick(object sender, RoutedEventArgs e)
        {

            DeliveryItemWindow itemWindow = new DeliveryItemWindow(selectedItem);
            itemWindow.Width = 300;
            itemWindow.Height = 450;
            itemWindow.Closing += UpdateWindow_Closed;
            itemWindow.ShowDialog();

        }

        private void UpdateWindow_Closed(object? sender, CancelEventArgs e)
        {
            loadDeliviries();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            DeliveryItemWindow itemWindow = new DeliveryItemWindow();
            itemWindow.Width = 300;
            itemWindow.Height = 450;
            itemWindow.Closing += UpdateWindow_Closed;
            itemWindow.ShowDialog();

        }
    }
}
