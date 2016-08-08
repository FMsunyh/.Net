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
    public class VMSubsidirayCreator : ViewModelBase
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
        ///     _subsidiary
        /// </summary>
        private readonly Subsidiary _subsidiary = new Subsidiary();

        /// <summary>
        ///     _subsidiraySelector
        /// </summary>
        private readonly SubsidiarySelector _subsidiraySelector;

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
                return _subsidiary.Id;
            }

            set
            {
                if (value != _subsidiary.Id)
                {
                    _subsidiary.Id = value;
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
                return _subsidiary.BpId;
            }

            set
            {
                if (value != _subsidiary.BpId)
                {
                    _subsidiary.BpId = value;
                    RaisePropertyChanged(() => BpId);
                }
            }
        }

        /// <summary>
        ///     IsMain
        /// </summary>
        public bool IsMain
        {
            get
            {
                return _subsidiary.IsMain;
            }

            set
            {
                if (value != _subsidiary.IsMain)
                {
                    _subsidiary.IsMain = value;
                    RaisePropertyChanged(() => IsMain);
                }
            }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public string Description
        {
            get
            {
                return _subsidiary.Description;
            }

            set
            {
                if (value != _subsidiary.Description)
                {
                    _subsidiary.Description = value;
                    RaisePropertyChanged(() => Description);
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
                return _subsidiary.Address;
            }

            set
            {
                if (value != _subsidiary.Address)
                {
                    _subsidiary.Address = value;
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
                return _subsidiary.Street;
            }

            set
            {
                if (value != _subsidiary.Street)
                {
                    _subsidiary.Street = value;
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
                return _subsidiary.City;
            }

            set
            {
                if (value != _subsidiary.City)
                {
                    _subsidiary.City = value;
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
                return _subsidiary.ZipCode;
            }

            set
            {
                if (value != _subsidiary.ZipCode)
                {
                    _subsidiary.ZipCode = value;
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
                return _subsidiary.Telephone;
            }

            set
            {
                if (value != _subsidiary.Telephone)
                {
                    _subsidiary.Telephone = value;
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
                return _subsidiary.Email;
            }

            set
            {
                if (value != _subsidiary.Email)
                {
                    _subsidiary.Email = value;
                    RaisePropertyChanged(() => Email);
                }
            }
        }
        
        /// <summary>
        ///     VMContactCreator
        /// </summary>
        /// <param name="selector"></param>
        public VMSubsidirayCreator(SubsidiarySelector selector)
        { 
            _subsidiraySelector = selector;
        }

        #region Methods

        /// <summary>
        ///     Save
        /// </summary>
        protected virtual void OnSave()
        {
            //test error 
            //if (null != _subsidiary)
            {
                _subsidiraySelector.Save(_subsidiary);
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
