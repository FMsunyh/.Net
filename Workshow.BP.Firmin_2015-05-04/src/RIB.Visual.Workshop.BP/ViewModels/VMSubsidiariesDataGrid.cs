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
    public class VMSubsidiariesDataGrid : ViewModelBase
    {
        #region Feilds

        ///// <summary>
        ///     _subsidiarySelector
        /// </summary>
        private readonly SubsidiarySelector _subsidiarySelector;

        #endregion

        /// <summary>
        ///     CurrentSelectedIndex
        /// </summary>
        public int CurrentSelectedIndex
        {
            get
            {
                return _subsidiarySelector.SelectedIndex;
            }
            set
            {
                if (!Equals(value, _subsidiarySelector.SelectedIndex))
                {
                    _subsidiarySelector.SelectedIndex = value;
                    RaisePropertyChanged(() => CurrentSelectedIndex);

                }
            }
        }

        /// <summary>
        ///     _currentSelectedItem
        /// </summary>
        private Subsidiary _currentSelectedItem;

        /// <summary>
        ///     CurrentSelectedItem
        /// </summary>
        public Subsidiary CurrentSelectedItem
        {
            get
            {
                return _currentSelectedItem;
            }
            set
            {
                if (!Equals(CurrentSelectedItem, _subsidiarySelector.SelectedItem))
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
                return _subsidiarySelector.DataView;
            }
        }

        /// <summary>
        ///     VMSubsidiariesDataGrid()
        /// </summary>
        public VMSubsidiariesDataGrid(SubsidiarySelector subsidiarySelector)
        {
            _subsidiarySelector = subsidiarySelector;
            _currentSelectedItem = subsidiarySelector.SelectedItem;
        }
    }
}
