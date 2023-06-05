using Microsoft.Identity.Client;
using StoresApp.View.DeliveryView;
using StoresApplication.Models;
using StoresApplication;
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
using Microsoft.EntityFrameworkCore;
using StoresApplication.View.OwnershipView;

namespace StoresApp.View
{
    /// <summary>
    /// Логика взаимодействия для OwnershipPage.xaml
    /// </summary>
    public partial class OwnershipPage : Page
    {
        public string tag = "Ownership";

        Ownership selectedItem;

        StoresContext justStoresContext = new StoresContext();

        public Ownership SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        List<Ownership> dataGridItems;
        public List<Ownership> OwnershipsDataGridItems
        {
            get { return dataGridItems; }
            set
            {
                dataGridItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OwnershipsDataGridItems"));
            }
        }

        public OwnershipPage()
        {
            InitializeComponent();
            DataContext = this;
            loadOwnerships();
        }

        private void loadOwnerships()
        {
            
                List<Ownership> items = justStoresContext.Ownerships.Include(u => u.StoreOwner).Include(u => u.Store).ToList();
                ownershipDataGrid.ItemsSource = items;
                OwnershipsDataGridItems = items;
            
        }

        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить элемент списка?", "Сообщение", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:   
                    
                        justStoresContext.Remove(SelectedItem);
                        justStoresContext.SaveChanges();
                    
                    loadOwnerships();
                    ownershipDataGrid.ItemsSource = OwnershipsDataGridItems;
                    break;
                default:
                    return;
            }

        }
        private void UpdateItemClick(object sender, RoutedEventArgs e)
        {
            
            OwnershipItemWindow itemWindow = new OwnershipItemWindow(selectedItem);
            itemWindow.Width = 300;
            itemWindow.Height = 450;
            itemWindow.Closing += UpdateWindow_Closed;
            itemWindow.ShowDialog();
            
        }

        private void UpdateWindow_Closed(object? sender, CancelEventArgs e)
        {
            loadOwnerships();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            OwnershipItemWindow itemWindow = new OwnershipItemWindow();
            itemWindow.Width = 300;
            itemWindow.Height = 450;
            itemWindow.Closing += UpdateWindow_Closed;
            itemWindow.ShowDialog();

        }
    }
}
