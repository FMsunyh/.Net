using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Trainee.Controls;
using Trainee.Models;

namespace Trainee.ViewModels
{
    /// <summary>
    ///     VMPromotion
    /// </summary>
    public class VMPromotion : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Fields

        private ICommand _saveCommand;
        private ICommand _cancelCommand;
        private string _error;
        private readonly Staff _staff;
        private readonly StaffManager _staffManager;
        private readonly PositionSelector _positionSelector = new PositionSelector();
        private readonly GradeSelector _gradeSelector = new GradeSelector();

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructors
        /// </summary>
        public VMPromotion(StaffManager staffManager, Staff staff)
        {
            _staffManager = staffManager;
            _staff = staff;
            _positionSelector.Seek(staff.Position);
            _gradeSelector.Seek(staff.Position.Grade);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     SaveCommand
        /// </summary>
        public ICommand Promotion
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new RelayCommand(OnSave));
            }
        }

        /// <summary>
        ///     SaveCommand
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
        ///     StaffNo
        /// </summary>
        public int StaffNo
        {
            get
            {
                return _staff.StaffNo;
            }
            set
            {
                if (value != _staff.StaffNo)
                {
                    _staff.StaffNo = value;
                    RaisePropertyChanged(() => StaffNo);
                }
            }
        }

        /// <summary>
        ///     Chinese
        /// </summary>
        public string Chinese
        {
            get
            {
                return _staff.Chinese;
            }
            set
            {
                if (value != _staff.Chinese)
                {
                    _staff.Chinese = value;
                    RaisePropertyChanged(() => Chinese);
                }
            }
        }

        /// <summary>
        ///     English
        /// </summary>
        public string English
        {
            get { return _staff.English; }
            set
            {
                if (value != _staff.English)
                {
                    _staff.English = value;
                    RaisePropertyChanged(() => English);
                }
            }
        }

        /// <summary>
        ///     Position
        /// </summary>
        protected Position Position
        {
            get
            {
                return _positionSelector.Selected;
            }
        }

        /// <summary>
        ///     Position
        /// </summary>
        public PositionSelector PositionSelector
        {
            get
            {
                return _positionSelector;
            }
        }

        /// <summary>
        ///     Gender
        /// </summary>
        public Gender Gender
        {
            get { return _staff.Gender; }
            set
            {
                if (!value.Equals( _staff.Gender))
                {
                    _staff.Gender = value;
                    RaisePropertyChanged(() => Gender);
                }
            }
        }

        /// <summary>
        ///     Grade
        /// </summary>
        protected Grade Grade
        {
            get { return _gradeSelector.Selected; }
        }

        /// <summary>
        ///     Position
        /// </summary>
        public GradeSelector GradeSelector
        {
            get
            {
                return _gradeSelector;
            }
        }

        /// <summary>
        ///     Error
        /// </summary>
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _error = null;
                }
                else
                {
                    _error = value;
                    RaisePropertyChanged(() => Error);
                }
            }
        }

        #endregion

        #region Methods 

        /// <summary>
        ///     Save
        /// </summary>
        protected virtual void OnSave()
        {
            _error = null;
            var position = Position;
            position.Grade = Grade;
            _staff.Promotion(position);
            _staffManager.Save(_staff);
            OnCancel();
        }

        /// <summary>
        ///     Save
        /// </summary>
        protected virtual void OnCancel()
        {
            var window = App.Current.Properties["promotion"] as RIBWindow;
            if (window != null)
                window.Close();
        }

        #endregion
    }
}
