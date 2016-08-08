using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RIB.Visual.Workshop.BP.Core.Models;
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
    ///     VMContactCreator
    /// </summary>
    public class VMContactCreator : ViewModelBase
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
        ///     _contact
        /// </summary>
        private readonly Contact _contact = new Contact();

        /// <summary>
        ///     _contactSelector
        /// </summary>
        private readonly ContactSelector _contactSelector;

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
                return _contact.Id;
            }

            set
            {
                if (value != _contact.Id)
                {
                    _contact.Id = value;
                    RaisePropertyChanged(() => Id);
                }
            }
        }

        /// <summary>
        ///     BpId
        /// </summary>
        public int BpId
        {
            get
            {
                return _contact.BpId;
            }

            set
            {
                if (value != _contact.BpId)
                {
                    _contact.BpId = value;
                    RaisePropertyChanged(() => BpId);
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
                return _contact.Name;
            }

            set
            {
                if (value != _contact.Name)
                {
                    _contact.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        /// <summary>
        ///     SubsidraryId
        /// </summary>
        public int SubsidraryId
        {
            get
            {
                return _contact.SubsidiaryId;
            }

            set
            {
                if (value != _contact.SubsidiaryId)
                {
                    _contact.SubsidiaryId = value;
                    RaisePropertyChanged(() => SubsidraryId);
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
                return _contact.Title;
            }

            set
            {
                if (value != _contact.Title)
                {
                    _contact.Title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        /// <summary>
        ///     Role
        /// </summary>
        public string Role
        {
            get
            {
                return _contact.Role;
            }

            set
            {
                if (value != _contact.Role)
                {
                    _contact.Role = value;
                    RaisePropertyChanged(() => Role);
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
                return _contact.CompanyName;
            }

            set
            {
                if (value != _contact.CompanyName)
                {
                    _contact.CompanyName = value;
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
                return _contact.CompanyCode;
            }

            set
            {
                if (value != _contact.CompanyCode)
                {
                    _contact.CompanyCode = value;
                    RaisePropertyChanged(() => CompanyCode);
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
                return _contact.Telephone;
            }

            set
            {
                if (value != _contact.Telephone)
                {
                    _contact.Telephone = value;
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
                return _contact.Email;
            }

            set
            {
                if (value != _contact.Email)
                {
                    _contact.Email = value;
                    RaisePropertyChanged(() => Email);
                }
            }
        }
    
        /// <summary>
        ///     VMContactCreator
        /// </summary>
        /// <param name="selector"></param>
        public VMContactCreator(ContactSelector selector)
        { 
            _contactSelector = selector;
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
                _contactSelector.Save(_contact);
                OnCancel();
            }
        }

        /// <summary>
        ///     Cancel
        /// </summary>
        protected virtual void OnCancel()
        {
            var window = App.Current.Properties["creator"] as Window;
            if (window != null)
                window.Close();
        }

        #endregion
    }
}
