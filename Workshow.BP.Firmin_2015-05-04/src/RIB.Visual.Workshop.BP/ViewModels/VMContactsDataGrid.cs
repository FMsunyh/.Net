using GalaSoft.MvvmLight;
using RIB.Visual.Workshop.BP.Core.Models;
using RIB.Visual.Workshop.BP.Core.Service;
using RIB.Visual.Workshop.BP.Libraries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIB.Visual.Workshop.BP.ViewModels
{
    /// <summary>
    ///     class VMContactsDataGrid
    /// </summary>
    public class VMContactsDataGrid : ViewModelBase
    {
        /// <summary>
        ///     _contactSelector
        /// </summary>
        private readonly ContactSelector _contactSelector;        

        ///// <summary>
        /////     _selectedItem
        ///// </summary>
        //private Contact _selectedItem;

        /// <summary>
        ///     CurrentSelectedIndex
        /// </summary>
        public int CurrentSelectedIndex
        {
            get
            {
                return _contactSelector.SelectedIndex;
            }
            set
            {
                if (!Equals(value, _contactSelector.SelectedIndex))
                {
                    _contactSelector.SelectedIndex = value;
                    RaisePropertyChanged(() => CurrentSelectedIndex);

                }
            }
        }

        /// <summary>
        ///     _currentSelectedItem
        /// </summary>
        private Contact _currentSelectedItem;

        /// <summary>
        ///     CurrentSelectedItem
        /// </summary>
        public Contact CurrentSelectedItem
        {
            get
            {
                return _currentSelectedItem;
            }
            set
            {
                if (!Equals(CurrentSelectedItem, _contactSelector.SelectedItem))
                {
                    _currentSelectedItem = value;
                    RaisePropertyChanged(() => CurrentSelectedItem);
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
                return _contactSelector.DataView;
            }
        }

        /// <summary>
        ///     VMContactsDataGrid
        /// </summary>
        /// <param name="contactSelector"></param>
        public VMContactsDataGrid(ContactSelector contactSelector)
        {
            _contactSelector = contactSelector;
            _currentSelectedItem = contactSelector.SelectedItem;
        }
    }
}
