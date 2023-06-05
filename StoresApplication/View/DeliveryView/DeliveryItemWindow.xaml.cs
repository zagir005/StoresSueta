using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using StoresApplication;
using StoresApplication.Models;

namespace StoresApp.View.DeliveryView
{
    /// <summary>
    /// Логика взаимодействия для DeliveryItemWindow.xaml
    /// </summary>
    public partial class DeliveryItemWindow : Window
    {
        StoresContext storesContext = new StoresContext();

        public bool itAdd = false;

        public DeliveryRedact RedactDelivery = new DeliveryRedact();
        
        public DeliveryItemWindow()
        {
            itAdd = true;
            
            RedactDelivery.StoresList = storesContext.Stores.ToList();
            RedactDelivery.VendorsList = storesContext.Vendors.ToList();
            

            InitializeComponent();
            DataContext = RedactDelivery;
        }

        public DeliveryItemWindow(Delivery _delivery)
        {
            RedactDelivery.StoresList = storesContext.Stores.ToList();
            RedactDelivery.VendorsList = storesContext.Vendors.ToList();

            

            Delivery delivery = new StoresContext().Deliveries
                .Include(item => item.Vendor)
                .Include(item => item.Store)
                .First(item => item.Id == _delivery.Id);


            InitializeComponent();
            DataContext = RedactDelivery;
            
            RedactDelivery.Id = _delivery.Id;
            RedactDelivery.SelectedStore = delivery.Store;
            RedactDelivery.SelectedVendor = delivery.Vendor;
            RedactDelivery.Price = delivery.Price;

        }


        private void addInvoke()
        {
            var addDelivery = parseRedactDelivery(RedactDelivery);
            if (!isValideData(addDelivery))
            {
                MessageBox.Show("Некорректно введены данные");
            }
            else
            {
                using (StoresContext storesContext = new StoresContext())
                {
                    storesContext.Add(addDelivery);
                    storesContext.SaveChanges();
                }
                Close();
            }
            
        }
        private void updateInvoke()
        {

            var addDeliveryRedact = parseRedactDelivery(RedactDelivery);
            if (!isValideData(addDeliveryRedact))
            {
                MessageBox.Show("Не все данные заполнены", "Ошибка");
            }
            else
            {
                using (StoresContext storesContext = new StoresContext())
                {
                    var returnDelivery = storesContext.Deliveries.First(item => item.Id == RedactDelivery.Id);
                    returnDelivery.VendorId = addDeliveryRedact.VendorId;
                    returnDelivery.StoreId = addDeliveryRedact.StoreId;
                    returnDelivery.Price = addDeliveryRedact.Price;
                    storesContext.Deliveries.Update(returnDelivery);
                    storesContext.SaveChanges();
                }
                Close();
            }
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

        private Delivery parseRedactDelivery(DeliveryRedact deliveryRedact)
        {
            Delivery returnDelivery = new Delivery
            {
                StoreId = deliveryRedact.SelectedStore.Id,
                VendorId = deliveryRedact.SelectedVendor.Id,
                Price = deliveryRedact.Price
            };
            return returnDelivery;
        }
        private bool isValideData(Delivery delivery)
        {
            if (delivery.Price == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class DeliveryRedact: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id;

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }
        private Store selectedStore;

        public Store SelectedStore
        {
            get { return selectedStore; }
            set
            {
                selectedStore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedStore"));
            }
        }

        private List<Store> storesList;
        public List<Store> StoresList
        {
            get { return storesList; }
            set
            {
                storesList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StoresList"));
            }
        }

        private Vendor selectedVendor;
        public Vendor SelectedVendor
        {
            get { return selectedVendor; }
            set
            {
                selectedVendor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedVendor"));
            }
        }

        private List<Vendor> vendorsList;
        public List<Vendor> VendorsList
        {
            get { return vendorsList; }
            set
            {
                vendorsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VendorsList"));
            }
        }
    }
}
