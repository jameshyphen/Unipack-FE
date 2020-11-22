using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Unipack_FE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            string WelcomeString = "Welcome, Luna";
            this.InitializeComponent();
            WelcomeDropDown.DataContext = WelcomeString;
        }

        private void OnPointEnter(object sender, PointerRoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromArgb(10, 0, 0, 0));
            this.Foreground = new SolidColorBrush(Colors.White);
            VisualStateManager.GoToState(this, "VisualStateNormal", true);
        }

        private void OnPointExit(object sender, PointerRoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromArgb(25, 0, 0, 0));
            this.Foreground = new SolidColorBrush(Colors.White);
            VisualStateManager.GoToState(this, "VisualStateNormal", true);
        }
    }
}
