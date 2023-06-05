using StoresApp.View.StoreOwnerView;
using StoresApplication;
using StoresApplication.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StoresApp.View
{
    /// <summary>
    /// Логика взаимодействия для StoreOwnersPage.xaml
    /// </summary>
    public partial class StoreOwnersPage : Page, INotifyPropertyChanged
    {
        public string tag = "StoreOwners";
        
        
        StoresContext storesContext = new StoresContext();

            
        StoreOwner selectedItem;
        public StoreOwner SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
            }
        }

        List<StoreOwner> dataGridItems;

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<StoreOwner> DataGridItems
        {
            get { return dataGridItems; }
            set
            {
                dataGridItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataGridItems"));
            }
        }

        public StoreOwnersPage()
        {
            InitializeComponent();

            this.DataContext = this;

            DataGridItems = storesContext.StoreOwners.ToList();

        }

        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить элемент списка? Будут так же удалены связанные элементы из других таблиц", "Сообщение", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    storesContext.Remove(selectedItem);
                    storesContext.SaveChanges();
                    DataGridItems = storesContext.StoreOwners.ToList();
                    break;
                default:
                    return;
            }
            
        }

        private void UpdateWindow_Closed(object? sender, CancelEventArgs e)
        {
            DataGridItems = new StoresContext().StoreOwners.ToList();
        }

        private void UpdateItemClick(object sender, RoutedEventArgs e)
        {
            StoreOwnerUpdateWindow updateWindow = new StoreOwnerUpdateWindow(selectedItem);
            updateWindow.Width = 300;
            updateWindow.Height = 400;
            updateWindow.Closing += UpdateWindow_Closed;
            updateWindow.ShowDialog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            showStoreItemWindow();
        }

        private void showStoreItemWindow()
        {
            StoreOwnerUpdateWindow updateWindow = new StoreOwnerUpdateWindow();
            updateWindow.Width = 300;
            updateWindow.Height = 400;
            updateWindow.Closing += UpdateWindow_Closed;
            updateWindow.ShowDialog();
        }

        
    }

    


}
