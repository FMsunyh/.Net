using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Trainee.Controls;
using Trainee.Models;

namespace Trainee.ViewModels
{
    /// <summary>
    ///     VMTraineeCreator
    /// </summary>
    public class VMTraineeCreator : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Fields

        private ICommand _saveCommand;
        private ICommand _cancelCommand;
        private string _error;
        private readonly Staff _trainee = new Staff();
        private readonly StaffManager _staffManager;
        private readonly PositionSelector _positionSelector = new PositionSelector();
        private readonly GenderSelector _genderSelector = new GenderSelector();
        private readonly GradeSelector _gradeSelector = new GradeSelector();

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructors
        /// </summary>
        public VMTraineeCreator(StaffManager staffManager)
        {
            _staffManager = staffManager;
            Gender= new MaleGender();
        }

        #endregion

        #region Properties

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
                return _trainee.StaffNo;
            }
            set
            {
                if (value != _trainee.StaffNo)
                {
                    _trainee.StaffNo = value;
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
                return _trainee.Chinese;
            }
            set
            {
                if (value != _trainee.Chinese)
                {
                    _trainee.Chinese = value;
                    RaisePropertyChanged(() => Chinese);
                }
            }
        }

        /// <summary>
        ///     English
        /// </summary>
        public string English
        {
            get { return _trainee.English; }
            set
            {
                if (value != _trainee.English)
                {
                    _trainee.English = value;
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
            get
            {
                return _trainee.Gender;
            }
            set
            {
                if (!value.Equals(_trainee.Gender))
                {
                    _trainee.Gender = value;
                    RaisePropertyChanged(() => Gender);
                }
            }
        }


        /// <summary>
        ///     Position
        /// </summary>
        public GenderSelector GenderSelector
        {
            get { return _genderSelector; }
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
            var error = Validate();
            if (string.IsNullOrWhiteSpace(error))
            {
                _error = null;
                _staffManager.Save(_trainee);
                OnCancel();
            }
            else
                Error = error;
        }

        /// <summary>
        ///     Save
        /// </summary>
        protected virtual void OnCancel()
        {
            var window = App.Current.Properties["creator"] as RIBWindow;
            if (window != null)
                window.Close();
        }

        #endregion

        #region Validate

        /// <summary>
        ///     Validate
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            if (StaffNo == 0)
                return "Null StaffNo";

            if (string.IsNullOrWhiteSpace(English))
                return "Null English Name";

            if (_staffManager.Contains(StaffNo))
                return "StaffNo error";

            _trainee.Gender = Gender;
            _trainee.State = 0;
            var position = Position;
            position.Grade = Grade;
            _trainee.Position = position;

            return string.Empty;
        }

        #endregion
    }
}
