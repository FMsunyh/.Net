using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using Trainee.Controls;
using Trainee.Models;
using Trainee.Views;

namespace Trainee.ViewModels
{
    /// <summary>
    ///     MainWindowViewModel
    /// </summary>
    public class VMMainWindow : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Feilds

        private readonly StaffStore _staffLoader;

        #endregion

        #region Constructors

        /// <summary>
        ///    
        /// </summary>
        public VMMainWindow()
        {
            _staffLoader = new StaffStore();
            App.Current.Properties["loader"] = _staffLoader;
            var staffManager = _staffLoader.GetManager();
            Browser = new VMTraineeBrowser(staffManager);
            Menu = new VMMenu(staffManager);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Menu
        /// </summary>
        public VMMenu Menu { get; protected set; }

        /// <summary>
        ///     Browser
        /// </summary>
        public VMTraineeBrowser Browser { get; protected set; }

        #endregion

        #region Save

        /// <summary>
        ///     Save
        /// </summary>
        public void Save()
        {
            _staffLoader.Save();
        }

        #endregion
    }
}
