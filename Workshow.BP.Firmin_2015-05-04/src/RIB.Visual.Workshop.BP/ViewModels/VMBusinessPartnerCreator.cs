using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RIB.Visual.Workshop.BP.Core.Models;
using RIB.Visual.Workshop.BP.Core.Service;
using RIB.Visual.Workshop.BP.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RIB.Visual.Workshop.BP.ViewModels
{
    /// <summary>
    ///     VMBusinessPartnerCreator
    /// </summary>
    public class VMBusinessPartnerCreator : ViewModelBase
    {
        /// <summary>
        ///     _saveCommand
        /// </summary>
        private ICommand _saveCommand;

        /// <summary>
        ///     _cancelCommand
        /// </summary>
        private ICommand _cancelCommand;

        /// <summary>
        ///     _businessPartner
        /// </summary>
        private readonly BusinessPartner _businessPartner = new BusinessPartner();

        /// <summary>
        ///     _bpSelector
        /// </summary>
        private readonly BusinessPartnerSelector _bpSelector;

        /// <summary>
        ///     SaveCommand
        /// </summary>
        public ICommand Save
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new RelayCommand(OnSave));
            }
        }

        /// <summary>
        ///     CancelCommand
        /// </summary>
        public ICommand Cancel
        {
            get
            {
                return _cancelCommand ??
                       (_cancelCommand = new RelayCommand(OnCancel));
            }
        }

        /// <summary>
        ///     Id
        /// </summary>
        public int Id
        {
            get
            {
                return _businessPartner.Id;
            }

            set
            {
                if (value != _businessPartner.Id)
                {
                    _businessPartner.Id = value;
                    RaisePropertyChanged(() => Id);
                }
            }
        }

        /// <summary>
        ///     Name
        /// </summary>
        public string Name
        {
            get
            {
                return _businessPartner.Name;
            }

            set
            {
                if (value != _businessPartner.Name)
                {
                    _businessPartner.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        /// <summary>
        ///     Title
        /// </summary>
        public string Title
        {
            get
            {
                return _businessPartner.Title;
            }

            set
            {
                if (value != _businessPartner.Title)
                {
                    _businessPartner.Title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        /// <summary>
        ///     CompanyName
        /// </summary>
        public string CompanyName
        {
            get
            {
                return _businessPartner.CompanyName;
            }

            set
            {
                if (value != _businessPartner.CompanyName)
                {
                    _businessPartner.CompanyName = value;
                    RaisePropertyChanged(() => CompanyName);
                }
            }
        }

        /// <summary>
        ///     CompanyCode
        /// </summary>
        public string CompanyCode
        {
            get
            {
                return _businessPartner.CompanyCode;
            }

            set
            {
                if (value != _businessPartner.CompanyCode)
                {
                    _businessPartner.CompanyCode = value;
                    RaisePropertyChanged(() => CompanyCode);
                }
            }
        }

        /// <summary>
        ///     Address
        /// </summary>
        public string Address
        {
            get
            {
                return _businessPartner.Address;
            }

            set
            {
                if (value != _businessPartner.Address)
                {
                    _businessPartner.Address = value;
                    RaisePropertyChanged(() => Address);
                }
            }
        }

        /// <summary>
        ///     Street
        /// </summary>
        public string Street
        {
            get
            {
                return _businessPartner.Street;
            }

            set
            {
                if (value != _businessPartner.Street)
                {
                    _businessPartner.Street = value;
                    RaisePropertyChanged(() => Street);
                }
            }
        }

        /// <summary>
        ///     City
        /// </summary>
        public string City
        {
            get
            {
                return _businessPartner.City;
            }

            set
            {
                if (value != _businessPartner.City)
                {
                    _businessPartner.City = value;
                    RaisePropertyChanged(() => City);
                }
            }
        }

        /// <summary>
        ///     ZipCode
        /// </summary>
        public string ZipCode
        {
            get
            {
                return _businessPartner.ZipCode;
            }

            set
            {
                if (value != _businessPartner.ZipCode)
                {
                    _businessPartner.ZipCode = value;
                    RaisePropertyChanged(() => ZipCode);
                }
            }
        }

        /// <summary>
        ///     Telephone
        /// </summary>
        public string Telephone
        {
            get
            {
                return _businessPartner.Telephone;
            }

            set
            {
                if (value != _businessPartner.Telephone)
                {
                    _businessPartner.Telephone = value;
                    RaisePropertyChanged(() => Telephone);
                }
            }
        }

        /// <summary>
        ///     Email
        /// </summary>
        public string Email
        {
            get
            {
                return _businessPartner.Email;
            }

            set
            {
                if (value != _businessPartner.Email)
                {
                    _businessPartner.Email = value;
                    RaisePropertyChanged(() => Email);
                }
            }
        }

        /// <summary>
        ///     VMBusinessPartnerCreator
        /// </summary>
        /// <param name="selector"></param>
        public VMBusinessPartnerCreator(BusinessPartnerSelector selector)
        {
            _bpSelector = selector;
        }

        #region Methods

        /// <summary>
        ///     Save
        /// </summary>
        protected virtual void OnSave()
        {
            //test error 
            //if (null != _businessPartner)
            {
                _bpSelector.Save(_businessPartner);
                OnCancel();
            }
        }

        /// <summary>
        ///     Cancel
        /// </summary>
        protected virtual void OnCancel()
        {
            //var window = App.Current.Properties["creator"] as Window;
            //if (window != null)
            //    window.Close();

            var window = App.Current.Properties["creator"] as Window;
            if (window != null)
                window.Close();
        }

        #endregion
    }
}
