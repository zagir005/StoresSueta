using Microsoft.EntityFrameworkCore;
using StoresApp.View.DeliveryView;
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

namespace StoresApplication.View.OwnershipView
{
    /// <summary>
    /// Логика взаимодействия для OwnershipItemWindow.xaml
    /// </summary>
    public partial class OwnershipItemWindow : Window
    {
        
        StoresContext storesContext = new StoresContext();

        public bool itAdd = false;

        public OwnershipRedact RedactOwnership = new OwnershipRedact();
        public Ownership redactingOwnership;
        public OwnershipItemWindow()
        {
            itAdd = true;

            RedactOwnership.StoresList = storesContext.Stores.ToList();
            RedactOwnership.StoreOwnersList = storesContext.StoreOwners.ToList();


            InitializeComponent();
            DataContext = RedactOwnership;
        }

        public OwnershipItemWindow(Ownership _ownership)
        {
            RedactOwnership.StoresList = storesContext.Stores.ToList();
            RedactOwnership.StoreOwnersList = storesContext.StoreOwners.ToList();

            Ownership ownership = new StoresContext().Ownerships
                .Include(item => item.StoreOwner)
                .Include(item => item.Store)
                .First(item => item.Id == _ownership.Id);

            redactingOwnership = ownership;

            InitializeComponent();
            DataContext = RedactOwnership;

            RedactOwnership.Id = _ownership.Id;
            RedactOwnership.SelectedStore = ownership.Store;
            RedactOwnership.SelectedStoreOwner = ownership.StoreOwner;
            RedactOwnership.RegisterDate = ownership.RegisterDate;
            RedactOwnership.Deposit = ownership.Deposit;

        }


        private void addInvoke()
        {
            var addDelivery = parseRedactDelivery(RedactOwnership);
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

            var addDeliveryRedact = parseRedactDelivery(RedactOwnership);
            if (!isValideData(addDeliveryRedact))
            {
                MessageBox.Show("Не все данные заполнены", "Ошибка");
            }
            else
            {
                using (StoresContext storesContext = new StoresContext())
                {
                    var returnOwnership = storesContext.Ownerships.First(item => item.Id == RedactOwnership.Id);
                    returnOwnership.StoreOwnerId = addDeliveryRedact.StoreOwnerId;
                    returnOwnership.StoreId = addDeliveryRedact.StoreId;
                    returnOwnership.Deposit = addDeliveryRedact.Deposit;
                    returnOwnership.RegisterDate = addDeliveryRedact.RegisterDate;
                    storesContext.Ownerships.Update(returnOwnership);
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

        private Ownership parseRedactDelivery(OwnershipRedact ownershipRedact)
        {
            Ownership returnOwnership = new Ownership
            {
                StoreId = ownershipRedact.SelectedStore.Id,
                StoreOwnerId = ownershipRedact.SelectedStoreOwner.Id,
                Deposit = ownershipRedact.Deposit,
                RegisterDate = ownershipRedact.RegisterDate,
            };
            return returnOwnership;
        }
        private bool isValideData(Ownership ownership)
        {
            if (
                    ownership.Deposit == 0 || 
                    ownership.StoreId == null || 
                    ownership.StoreOwnerId == null || 
                    ownership.RegisterDate == null 
               )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void storeOwners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }


    public class OwnershipRedact : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id;

        private decimal deposit;
        public decimal Deposit
        {
            get { return deposit; }
            set
            {
                deposit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }

        private DateTime registerDate;
        public DateTime RegisterDate
        {
            get { return registerDate; }
            set
            {
                registerDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RegisterDate"));

            }
        }
        
        private StoreOwner selectedStoreOwner;
        public StoreOwner SelectedStoreOwner
        {
            get { return selectedStoreOwner; }
            set
            {
                selectedStoreOwner = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedStoreOwner"));
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

        private List<StoreOwner> storeOwnersList;
        public List<StoreOwner> StoreOwnersList
        {
            get { return storeOwnersList; }
            set
            {
                storeOwnersList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StoreOwnersList"));
            }
        }

        
    }
}
