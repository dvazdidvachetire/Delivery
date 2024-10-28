using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Delivery.Views
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : UserControl
    {
        public AboutWindow()
        {
            InitializeComponent();

            LabelVersion.Content = $"Версия ПО: {Assembly.GetAssembly(typeof(AboutWindow)).GetName().Version}";
        }
    }
}
