using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RIB.Visual.Workshop.BP.ViewModels;
using RIB.Visual.Workshop.BP.View;

namespace RIB.Visual.Workshop.BP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            VMMainWindow vm = new VMMainWindow();
            var window = new MainWin
            {
                DataContext = vm
            };
            App.Current.MainWindow = window;

            window.WindowState = WindowState.Maximized;
            window.ResizeMode = ResizeMode.NoResize;
            window.ShowInTaskbar = true;
            window.Show();
        }
    }
}
