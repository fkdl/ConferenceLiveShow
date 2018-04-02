using ConferenceLiveShow.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ConferenceLiveShow.Services;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace ConferenceLiveShow
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigationView.Header = "Settings";
                ContentFrame.Navigate(typeof(SettingPage));
            }
            else
            {
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavigationView_Navigate(item);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationView.Header = "Conference Live Show";
            ContentFrame.Navigate(typeof(HomePage));
        }

        private void NavigationView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "home":
                    NavigationView.Header = "Conference Live Show";
                    ContentFrame.Navigate(typeof(HomePage));
                    break;
                default:
                    break;
            }
        }
    }
}
