using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Trainee.Models;

namespace Trainee.ViewModels
{
    /// <summary>
    ///     VMTraineeBrowser
    /// </summary>
    public class VMTraineeBrowser : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Feilds

        private readonly StaffManager _staffManager;

        #endregion

        #region Constructors

        /// <summary>
        ///    
        /// </summary>
        public VMTraineeBrowser(StaffManager staffManager)
        {
            _staffManager = staffManager;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Current
        /// </summary>
        public Staff Current
        {
            get
            {
                return _staffManager.Selected;
            }
            set
            {
                if (!Equals(value, _staffManager.Selected))
                {
                    _staffManager.Selected = value;
                    RaisePropertyChanged(() => Current);
                }
            }
        }

        /// <summary>
        ///     DataSource
        /// </summary>
        public ICollectionView DataSource
        {
            get
            {
                return _staffManager.DataView;
            }
        }

        #endregion
    }
}
