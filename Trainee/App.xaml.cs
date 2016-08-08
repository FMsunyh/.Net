using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Trainee.Controls;
using Trainee.Models;
using Trainee.ViewModels;
using Trainee.Views;

namespace Trainee
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
           
            VMMainWindow vm = new VMMainWindow();
            var window = new MainWindow
                {
                    Width = 1280,
                    Height = 800,
                    DataContext = vm
                };
            App.Current.MainWindow = window;
            window.Show();
        }
    }
}
