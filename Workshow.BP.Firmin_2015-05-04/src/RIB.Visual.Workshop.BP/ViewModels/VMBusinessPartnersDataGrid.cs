using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RIB.Visual.Workshop.BP.Core.Service;
using RIB.Visual.Workshop.BP.Core.Models;
using RIB.Visual.Workshop.BP.Libraries;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.ComponentModel;

namespace RIB.Visual.Workshop.BP.ViewModels
{
    /// <summary>
    ///     class VMBusinessPartnersDataGrid
    /// </summary>
    public class VMBusinessPartnersDataGrid : ViewModelBase
    {
        #region Feilds

        /// <summary>
        /// 
        /// </summary>
        private ICommand _selectionChangeCommand;

        /// <summary>
        ///     _contactService
        /// </summary>
        private EntityFrameWorkContactService _contactService { get; set; }

        /// <summary>
        ///     _subsidiaryService
        /// </summary>
        public ISubsidiaryService _subsidiaryService { get; set; }

        ///// <summary>
        ///     _businessPartnerSelector
        /// </summary>
        private readonly BusinessPartnerSelector _businessPartnerSelector;

        /// <summary>
        ///     _subsidiarySelector
        /// </summary>
        private SubsidiarySelector _subsidiarySelector;

        /// <summary>
        ///     _ContactSelector
        /// </summary>
        private ContactSelector _contactSelector;

        #endregion

        /// <summary>
        ///     SelectionChangeCommand
        /// </summary>
        public ICommand SelectionChangeCommand
        {
            get
            {
                return _selectionChangeCommand ??
                    (_selectionChangeCommand = new RelayCommand(SelectionChanged));
            }
        }

        /// <summary>
        ///     CurrentSelectedIndex
        /// </summary>
        public int CurrentSelectedIndex
        {
            get
            {
                return _businessPartnerSelector.SelectedIndex;
            }
            set
            {
                if (!Equals(value, _businessPartnerSelector.SelectedIndex))
                {
                    _businessPartnerSelector.SelectedIndex = value;
                    RaisePropertyChanged(() => CurrentSelectedIndex);
                }
            }
        }

        /// <summary>
        ///     _currentSelectedItem
        /// </summary>
        private BusinessPartner _currentSelectedItem;

        /// <summary>
        ///     CurrentSelectedItem
        /// </summary>
        public BusinessPartner CurrentSelectedItem
        {
            get
            {
                return _currentSelectedItem;
            }
            set
            {
                if (!Equals(CurrentSelectedItem, _businessPartnerSelector.SelectedItem))
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
                return _businessPartnerSelector.DataView;
            }
        }

        /// <summary>
        ///     VMBusinessPartnersDataGrid()
        /// </summary>
        public VMBusinessPartnersDataGrid(BusinessPartnerSelector businessPartnerSelector, 
            IContactService contactService, ContactSelector contactSelector, 
            ISubsidiaryService subsidiaryService, SubsidiarySelector subsidiarySelector)
        {
            _businessPartnerSelector = businessPartnerSelector;
            _currentSelectedItem = _businessPartnerSelector.SelectedItem;

            _contactService = (EntityFrameWorkContactService)contactService;
            _contactSelector = contactSelector;
            _subsidiaryService = subsidiaryService;
            _subsidiarySelector = subsidiarySelector;
        }

        /// <summary>
        ///     SelectionChanged
        /// </summary>
        private void SelectionChanged()
        {
            _subsidiarySelector.DataSourceChanged(CurrentSelectedItem.Id);
            _contactSelector.DataSourceChanged(CurrentSelectedItem.Id);
        }
    }
}
