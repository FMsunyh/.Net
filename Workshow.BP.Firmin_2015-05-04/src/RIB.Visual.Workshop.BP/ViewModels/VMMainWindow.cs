using RIB.Visual.Workshop.BP.Core.Service;
using RIB.Visual.Workshop.BP.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIB.Visual.Workshop.BP.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class VMMainWindow : GalaSoft.MvvmLight.ViewModelBase
    {
        /// <summary>
        ///     _businessPartnerSelector
        /// </summary>
        private readonly BusinessPartnerSelector _businessPartnerSelector;

        /// <summary>
        ///     _subsidiarySelector
        /// </summary>
        private readonly SubsidiarySelector _subsidiarySelector;

        /// <summary>
        ///     _contactSelector
        /// </summary>
        private readonly ContactSelector _contactSelector;

        /// <summary>
        ///     _bpService
        /// </summary>
        private readonly EntityFrameWorkBusinessPartnerService _bpService
            = new EntityFrameWorkBusinessPartnerService();

        /// <summary>
        ///     _subsidiray
        /// </summary>
        private readonly EntityFrameWorkSubsidiaryService _subsidirayService
            = new EntityFrameWorkSubsidiaryService();

        /// <summary>
        ///     _contactSevice
        /// </summary>
        private readonly EntityFrameWorkContactService _contactSevice
            = new EntityFrameWorkContactService();

        /// <summary>
        ///     _businessPartnersDataGrid
        /// </summary>
        private VMBusinessPartnersDataGrid _businessPartnersDataGrid;

        /// <summary>
        ///     BusinessPartnersDataGrid
        /// </summary>
        public VMBusinessPartnersDataGrid BusinessPartnersDataGrid
        {
            get
            {
                return _businessPartnersDataGrid;
            }
            set
            {
                if (_businessPartnersDataGrid != null)
                {
                    _businessPartnersDataGrid = value;
                    RaisePropertyChanged("BusinessPartnersDataGrid");
                }
            }
        }

        /// <summary>
        ///     _subsidiariesDataGrid
        /// </summary>
        private VMSubsidiariesDataGrid _subsidiariesDataGrid;

        /// <summary>
        ///     SubsidiariesDataGrid
        /// </summary>
        public VMSubsidiariesDataGrid SubsidiariesDataGrid
        {
            get { return _subsidiariesDataGrid; }
            set
            {
                if (_subsidiariesDataGrid != value)
                {

                    _subsidiariesDataGrid = value;
                    RaisePropertyChanged("SubsidiariesDataGrid");
                }
            }
        }

        /// <summary>
        ///     _contactsDataGrid
        /// </summary>
        private VMContactsDataGrid _contactsDataGrid;

        /// <summary>
        ///     ContactsDataGrid
        /// </summary>
        public VMContactsDataGrid ContactsDataGrid
        {
            get { return _contactsDataGrid; }
            set
            {
                if (_contactsDataGrid != value)
                {
                    _contactsDataGrid = value;
                    RaisePropertyChanged("ContactsDataGrid");
                }
            }
        }

        /// <summary>
        ///     _businessPartnerMenu
        /// </summary>
        private VMBusinessPartnerMenu _businessPartnerMenu;

        /// <summary>
        ///     BusinessPartnerMenu
        /// </summary>
        public VMBusinessPartnerMenu BusinessPartnerMenu
        {
            get
            {
                return _businessPartnerMenu;
            }
            set
            {
                if (_businessPartnerMenu != value)
                {
                    _businessPartnerMenu = value;
                    RaisePropertyChanged("BusinessPartnerMenu");
                }
            }
        }

        /// <summary>
        ///     _subsidiariesMenu
        /// </summary>
        private VMSubsidiariesMenu _subsidiariesMenu;

        /// <summary>
        ///     SubsidiariesMenu
        /// </summary>
        public VMSubsidiariesMenu SubsidiariesMenu
        {
            get
            {
                return _subsidiariesMenu;
            }
            set
            {
                if (_subsidiariesMenu != value)
                {
                    _subsidiariesMenu = value;
                    RaisePropertyChanged("SubsidiariesMenu");
                }
            }
        }

        /// <summary>
        ///     _contactsMenu
        /// </summary>
        private VMContactsMenu _contactsMenu;

        /// <summary>
        ///     ContactsMenu
        /// </summary>
        public VMContactsMenu ContactsMenu
        {
            get { return _contactsMenu; }
            set
            {
                if (_contactsMenu != value)
                {
                    _contactsMenu = value;
                    RaisePropertyChanged("ContactsMenu");
                }
            }
        }

        /// <summary>
        ///     VMMainWindow
        /// </summary>
        public VMMainWindow()
        {
            _businessPartnerSelector = new BusinessPartnerSelector(_bpService);
            _contactSelector = new ContactSelector(_contactSevice);
            _subsidiarySelector = new SubsidiarySelector(_subsidirayService);

            _businessPartnerMenu = new VMBusinessPartnerMenu(_businessPartnerSelector);
            _businessPartnersDataGrid = new VMBusinessPartnersDataGrid(_businessPartnerSelector, 
                _contactSevice, _contactSelector, 
                _subsidirayService, _subsidiarySelector);

            _subsidiariesMenu = new VMSubsidiariesMenu(_subsidiarySelector);
            _subsidiariesDataGrid = new VMSubsidiariesDataGrid(_subsidiarySelector);

            _contactsMenu = new VMContactsMenu(_contactSelector);
            _contactsDataGrid = new VMContactsDataGrid(_contactSelector);
        }
    }
}
