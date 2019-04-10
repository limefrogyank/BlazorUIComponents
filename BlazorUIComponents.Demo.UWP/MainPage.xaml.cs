using BlazorUIComponents.Core;
using BlazorUIComponents.Core.Service;
using BlazorUIComponents.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlazorUIComponents.Demo.UWP
{

    public sealed partial class MainPage : Page
    {
        private readonly INavigationService navService;
        public MainViewModel ViewModel { get; set; }
        private object lastItem;

        public MainPage()
        {
            this.InitializeComponent();
            navService = Splat.Locator.Current.GetServiceExt<INavigationService>();

            //authService.IsLoggedIn.ObserveOn(RxApp.MainThreadScheduler).Subscribe((isLoggedIn) =>
            //{
            //    if (isLoggedIn)
            //        TurnOnMenuItems();
            //});

            navService.Initialize(navMenuFrame);
            navService.Navigated.Subscribe((vm) =>
            {
                //if (vm != ViewModel.SettingsViewModel)
                {
                    var menuItem = navMenu.MenuItems.Cast<NavigationViewItemBase>().FirstOrDefault(x => x.DataContext == vm);
                    if (menuItem != null)
                        navMenu.SelectedItem = menuItem;
                }
                //else
                //{
                //    navMenu.SelectedItem = navMenu.SettingsItem;
                //}
            });
            ViewModel = Splat.Locator.Current.GetServiceExt<MainViewModel>();

            TurnOnMenuItems();
        }

        public void TurnOnMenuItems()
        {
            lastItem = navMenu.MenuItems[0];
            navMenu.SelectedItem = navMenu.MenuItems[0];
            navService.NavigateToAsync(ViewModel.ListViewDemoViewModel);
        }

        private async void NavMenu_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            //if (args.IsSettingsInvoked)
            //{
            //    if (lastItem != args.InvokedItemContainer)
            //    {
            //        lastItem = args.InvokedItemContainer;
            //        await navService.NavigateToAsync(ViewModel.SettingsViewModel);
            //    }
            //}
            //else
            {
                if (lastItem != args.InvokedItemContainer)
                {
                    lastItem = args.InvokedItemContainer;
                    await navService.NavigateToAsync(args.InvokedItemContainer.DataContext);
                }
            }
        }



    }
}
