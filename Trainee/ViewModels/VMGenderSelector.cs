using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainee.Models;

namespace Trainee.ViewModels
{
    public class VMGenderSelector : GalaSoft.MvvmLight.ViewModelBase
    {
        private Gender _gender;
        public Gender Gender
        {
            get { return _gender; }
            set
            {
                if (value != _gender)
                {
                    _gender = value;
                    RaisePropertyChanged(() => Gender);
                }
            }
        }
    }
}
