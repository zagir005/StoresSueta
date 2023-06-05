using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using StoresApplication.Models;
using StoresApplication.MyExt;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoresApplication.View.FilterView
{
    /// <summary>
    /// Логика взаимодействия для FilterPage.xaml
    /// </summary>
    /// 


    public partial class FilterPage : Page
    {
        StoresContext context = new StoresContext();


        public FilterPage()
        {
            InitializeComponent();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selectedItem = comboBox?.SelectedItem as ComboBoxItem;
            var selectedItemTag = selectedItem?.Tag;

            
            dataGrid.Columns.Clear();
            switch (selectedItemTag)
            {
                case "kuznec":
                    KuznecovMagazines();
                    break;
                case "Small18":
                    Ownerships18();
                    break;
                case "DifferentOwner":
                    percentArea();
                    break;
                case "yongKiev":
                    yongKiev();
                    break;
            }
        }

        private void Ownerships18()
        {
            var rowsList = context.Ownerships.Select(o => new YongersModel
            {
                StoreName = o.Store.Name,
                StoreOwnerName = o.StoreOwner.Name,
                StoreOwnerBirthday = o.StoreOwner.BirthDate,
                RegisterDate = o.RegisterDate
            });

            var yongersList = (from own in rowsList
                               where DateTime.Now.Year - own.StoreOwnerBirthday.Year < 18
                               select own);

            var query = yongersList.ToQueryString();

            dataGrid.addMaterialColumn("ФИО", "StoreOwnerName");
            dataGrid.addMaterialColumn("Магазин", "StoreName");
            dataGrid.addMaterialColumn("Дата рождения владельца", "StoreOwnerBirthday");
            dataGrid.addMaterialColumn("Дата регистрации", "RegisterDate");

            dataGrid.ItemsSource = yongersList.ToList();

        }
        class YongersModel
        {
            public string StoreName { get; set; }
            public string StoreOwnerName { get; set; }
            public DateTime StoreOwnerBirthday { get; set; }
            public DateTime RegisterDate { get; set; }
        }

        private void KuznecovMagazines()
        {
            var stores = context.Ownerships.Select(o => new KuznecovStore
            {
                StoreOwnerName = o.StoreOwner.Name,
                StoreName = o.Store.Name,
                Deposit = o.Deposit
            });


            var kuznecovStores = (from store in stores
                                  where
                                    store.StoreOwnerName.Contains("Кузнецов")
                                  orderby store.Deposit
                                  select store);
            

            var query = kuznecovStores.ToQueryString();

            dataGrid.addMaterialColumn("Магазин", "StoreName");
            dataGrid.addMaterialColumn("Владелец", "StoreOwnerName");
            dataGrid.addMaterialColumn("Депозит", "Deposit");

            dataGrid.ItemsSource = kuznecovStores.ToList();
        }
        class KuznecovStore
        {
            public string StoreName { get; set; }
            public string StoreOwnerName { get; set; }
            public decimal Deposit { get; set; }
        }

        private void yongKiev()
        {

            var list =
                (from own in context.Ownerships.Include(store => store.Store).Include(storeOwner => storeOwner.StoreOwner)
                 where
                     own.Store.CityArea == "Киевский"

                 select own).OrderByDescending(own => own.StoreOwner!.BirthDate.Year);


            var query = list.ToQueryString();

            var yongKievList = list.Take(1).Select(u => new YongKiev
            {
                StoreName = u.Store.Name,
                StoreOwnerName = u.StoreOwner.Name,
                BirthDate = u.StoreOwner.BirthDate,
                RegisterDate = u.RegisterDate,
                CityArea = u.Store.CityArea
            });


            dataGrid.addMaterialColumn("Владелец", "StoreOwnerName");
            dataGrid.addMaterialColumn("Магазин", "StoreName");
            dataGrid.addMaterialColumn("Район", "CityArea");
            dataGrid.addMaterialColumn("Дата рождения", "BirthDate");

            dataGrid.ItemsSource = yongKievList.ToList();


        }
        class YongKiev
        {
            public string StoreName { get; set; }
            public string StoreOwnerName { get; set; }
            public DateTime RegisterDate { get; set; }
            public DateTime BirthDate { get; set; }
            public string CityArea { get; set; }

        }

        private void percentArea()
        {
            var list = (from ownership in context.Ownerships.Include(store => store.Store).Include(storeOwner => storeOwner.StoreOwner)
                        where
                        ownership.StoreOwner.CityArea != ownership.Store.CityArea &&
                        ownership.Store.Capital - ownership.Deposit < ownership.Store.Capital / 2
                        select ownership
                        );

            var query = list.ToQueryString();

            var ownrs = list.Select(own => new PercentArea
            {
                StoreOwnerArea = own.StoreOwner.CityArea,
                StoreArea = own.Store.CityArea,
                StoreName = own.Store.Name,
                StoreOwnerName = own.StoreOwner.Name,
                Percent = own.Deposit / own.Store.Capital * 100,
                Deposit = own.Deposit,
                Capital = own.Store.Capital,
                RegisterDate = own.RegisterDate
            }).ToList();

            dataGrid.addMaterialColumn("Владелец", "StoreOwnerName");
            dataGrid.addMaterialColumn("Магазин", "StoreName");
            dataGrid.addMaterialColumn("Район владелеца", "StoreOwnerArea");
            dataGrid.addMaterialColumn("Район магазина", "StoreArea");
            dataGrid.addMaterialColumn("Процент от капитала", "Percent");
            dataGrid.addMaterialColumn("Депозит", "Deposit");
            dataGrid.addMaterialColumn("Капитал", "Capital");
            dataGrid.addMaterialColumn("Дата регистрации", "RegisterDate");

            dataGrid.ItemsSource = ownrs;
        }

        class PercentArea
        {
            public string StoreOwnerName{ get; set;}
            public string StoreName { get; set;}
            public decimal? Capital { get; set; }
            public decimal Deposit { get; set; }
            public decimal? Percent { get; set; }
            public DateTime RegisterDate { get; set; }
            public string StoreOwnerArea { get; set; }
            public string StoreArea { get; set; }

        }

    }
    
    
    
}

