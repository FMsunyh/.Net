using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Trainee.Controls;
using Trainee.Models;

namespace Trainee.ViewModels
{
    /// <summary>
    ///     VMMenu
    /// </summary>
    public class VMMenu : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Fields

        //  staffManager
        protected readonly StaffManager _manager;

        #endregion

        #region Constructors

        /// <summary>
        ///     VMMenu
        /// </summary>
        /// <param name="staffManager"></param>
        public VMMenu(StaffManager staffManager)
        {
            _manager = staffManager;
        }

        #endregion

        #region New Command

        private ICommand _newCommand;
        /// <summary>
        ///     LeaveCommand
        /// </summary>
        public ICommand New
        {
            get
            {
                return _newCommand 
                    ?? (_newCommand = new RelayCommand(OnNew));
            }
        }
        /// <summary>
        ///     OnNew
        /// </summary>
        protected virtual void OnNew()
        {
            var vm = new ViewModels.VMTraineeCreator(_manager);
            var ui = new Views.TraineeCreator
                {
                    DataContext = vm
                };
            var window = new RIBWindow
                {
                    Height = 300,
                    Width = 500,
                    ShowInTaskbar = true,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStyle = WindowStyle.None,
                    Title = "Staff Factory",
                    Content = ui
                };
            //window.MouseLeftButtonDown += (sender, e) => window.DragMove();
            App.Current.Properties["creator"] = window;
            window.ShowDialog();
        }

        #endregion

        #region Leave Command

        private ICommand _leaveCommand;
        /// <summary>
        ///     LeaveCommand
        /// </summary>
        public ICommand Leave
        {
            get
            {
                return _leaveCommand 
                    ?? (_leaveCommand = new RelayCommand(OnLeave));
            }
        }

        /// <summary>
        ///     OnLeave
        /// </summary>
        protected virtual void OnLeave()
        {
            var current = _manager.Selected;
            if (current != null)
            {
                current.State = 1;
                _manager.Save(current);
               
            }
        }

        #endregion

        #region Save Command

        private ICommand _saveCommand;

        /// <summary>
        ///     ExitCommand
        /// </summary>
        public ICommand SaveStore
        {
            get
            {
                return _saveCommand ??
                       (_saveCommand = new RelayCommand(
                                           () =>
                                               {
                                                   var loader = (StaffStore) App.Current.Properties["loader"];
                                                   loader.Save();
                                               }));
            }
        }

        #endregion

        #region Promation Command

        private ICommand _promotionCommand;
        /// <summary>
        ///     LeaveCommand
        /// </summary>
        public ICommand Promation
        {
            get
            {
                return _promotionCommand
                    ?? (_promotionCommand = new RelayCommand(OnPromation));
            }
        }

        /// <summary>
        ///     OnNew
        /// </summary>
        protected virtual void OnPromation()
        {
            if (_manager.Selected != null && 
                _manager.Selected.State == 0)
            {
                var vm = new ViewModels.VMPromotion(_manager, _manager.Selected);
                var ui = new Views.UIPromation
                    {
                        DataContext = vm
                    };
                var window = new RIBWindow
                    {
                        Height = 300,
                        Width = 500,
                        ShowInTaskbar = true,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStyle = WindowStyle.None,
                        Title = "Promotion",
                        Content = ui
                    };
                window.MouseLeftButtonDown += (sender, e) => window.DragMove();
                App.Current.Properties["promotion"] = window;
                window.ShowDialog();
            }
        }

        #endregion
    }
}
