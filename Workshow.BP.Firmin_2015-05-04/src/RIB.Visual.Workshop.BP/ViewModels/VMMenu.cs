using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

namespace RIB.Visual.Workshop.BP.ViewModels
{
    /// <summary>
    ///     VMMenu
    /// </summary>
    public abstract class VMMenu : ViewModelBase
    {
        #region Properties

        private string _filterText;
        /// <summary>
        ///     FilterText
        /// </summary>
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                if (value != _filterText)
                {
                    _filterText = value;
                    RaisePropertyChanged(() => FilterText);
                }
            }
        }

        private ICommand _addCommand;
        /// <summary>
        /// AddCommand
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    
                    _addCommand = new RelayCommand(OnAddButtonClicked);
                }

                return _addCommand;
            }
        }

        private ICommand _deleteCommand;
        /// <summary>
        /// DeleteCommand
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(OnDeleteButtonClicked);
                }

                return _deleteCommand;
            }
        }

        /// <summary>
        ///     _textChangeCommand
        /// </summary>
        private ICommand _textChangedCommand;

        /// <summary>
        ///     TextChangeCommand
        /// </summary>
        public ICommand TextChangedCommand
        {
            get
            {
                return _textChangedCommand ??
                    (_textChangedCommand = new RelayCommand(TextChanged));
            }
        }
        
        #endregion

        #region Methods

        /// <summary>
        ///     OnAddButtonClicked
        /// </summary>
        protected virtual void OnAddButtonClicked()
        {
            Debug.WriteLine(string.Format("{0} Add Button Clicked", GetType().FullName));
        }

        /// <summary>
        ///     OnDeleteButtonClicked
        /// </summary>
        protected virtual void OnDeleteButtonClicked()
        {
            Debug.WriteLine(string.Format("{0} Delete Button Clicked", GetType().FullName));
        }

        /// <summary>
        ///     TextChanged
        /// </summary>
        protected virtual void TextChanged()
        {
            Debug.WriteLine(string.Format("{0} TextChanged", GetType().FullName));
        }

        #endregion
    }
}
