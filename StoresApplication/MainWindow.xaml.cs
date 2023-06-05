using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using StoresApp.View;
using StoresApplication.View.FilterView;
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

namespace StoresApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<NavigationItem> NavigationItems =
            new List<NavigationItem>()
            {
            new FirstLevelNavigationItem() { Label = "Владельцы", Icon = PackIconKind.User, IsSelected = true },
            new FirstLevelNavigationItem() { Label = "Поставщики", Icon = PackIconKind.User },
            new FirstLevelNavigationItem() { Label = "Магазины", Icon = PackIconKind.Store },
            new FirstLevelNavigationItem() { Label = "Поставки", Icon = PackIconKind.BoxAdd },
            new FirstLevelNavigationItem() { Label = "Владения", Icon = PackIconKind.StoreCog },
            new FirstLevelNavigationItem() { Label = "Выборки", Icon = PackIconKind.Filter}
            };
        public MainWindow()
        {
            InitializeComponent();
            SideNavigation.Items = NavigationItems;
            SideNavigation.NavigationItemSelected += SideNavigation_NavigationItemSelected;
        }
        private void SideNavigation_NavigationItemSelected(object sender, MaterialDesignExtensions.Controls.NavigationItemSelectedEventArgs args)
        {
            var navItem = (FirstLevelNavigationItem)args.NavigationItem;
            switch (navItem.Label)
            {
                case "Владельцы":
                    mainFrame.NavigationService.Navigate(new StoreOwnersPage());
                    break;
                case "Поставщики":
                    mainFrame.NavigationService.Navigate(new VendorsPage());
                    break;
                case "Магазины":
                    mainFrame.NavigationService.Navigate(new StoresPage());
                    break;
                case "Владения":
                    mainFrame.NavigationService.Navigate(new OwnershipPage());
                    break;
                case "Поставки":
                    mainFrame.NavigationService.Navigate(new DeliveryPage());
                    break;
                case "Выборки":
                    mainFrame.NavigationService.Navigate(new FilterPage());
                    break;
            }


        }
    }
}
