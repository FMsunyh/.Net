using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Trainee.Annotations;

namespace Trainee.Models
{
    /// <summary>
    ///     ModelReadOnlySelector
    /// </summary>
    public class ModelObserver<TModel> : ObservableCollection<TModel>, IModelSelector<TModel>
        where TModel : ModelBase
    {
        #region Fields

        private int _selectedIndex;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public ModelObserver(IEnumerable<TModel> dataSource)
            : base(dataSource)
        {

        }

        /// <summary>
        ///     ModelReadOnlySelector
        /// </summary>
        public ModelObserver()
        {

        }

        #endregion

        #region Implementation of IModelSelector

        /// <summary>
        ///     SelectedIndex
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (IsChanged(value))
                {
                    _selectedIndex = value;
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }

        /// <summary>
        ///     Selected
        /// </summary>
        public TModel Selected
        {
            get
            {
                return Has(_selectedIndex)
                           ? Items[_selectedIndex]
                           : null;
            }
        }

        /// <summary>
        ///     First
        /// </summary>
        /// <returns></returns>
        public Tuple<int, TModel> First()
        {
            if (Count > 0)
            {
                SelectedIndex = 0;
                return GetTuple();
            }
            else
                return GetEmpty();
        }

        /// <summary>
        ///     Last
        /// </summary>
        /// <returns></returns>
        public Tuple<int, TModel> Last()
        {
            if (Count > 0)
            {
                SelectedIndex = Count - 1;
                return GetTuple();
            }
            else
                return GetEmpty();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Tuple<int, TModel> Index(int index)
        {
            if (Has(index))
            {
                SelectedIndex = index;
                return GetTuple();
            }
            else
                return GetEmpty();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Tuple<int, TModel> Seek(TModel model)
        {
            return Index(IndexOf(model));
        }

        /// <summary>
        ///     Next
        /// </summary>
        /// <returns></returns>
        public Tuple<int, TModel> Next()
        {
            return Index(SelectedIndex + 1);
        }

        /// <summary>
        ///     Previous
        /// </summary>
        /// <returns></returns>
        public Tuple<int, TModel> Previous()
        {
            return Index(SelectedIndex - 1);
        }

        #endregion

        #region OnPropertyChanged

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(
                new PropertyChangedEventArgs(propertyName)
                );
        }

        #endregion

        #region Value Validate

        /// <summary>
        ///     GetTuple
        /// </summary>
        /// <returns></returns>
        protected virtual Tuple<int, TModel> GetTuple()
        {
            return new Tuple<int, TModel>(SelectedIndex, Selected);
        }

        /// <summary>
        ///     GetTuple
        /// </summary>
        /// <returns></returns>
        protected virtual Tuple<int, TModel> GetEmpty()
        {
            return new Tuple<int, TModel>(-1, null);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool Has(int value)
        {
            return value > -1 &&
                   value < Count;
        }

        /// <summary>
        ///     IsChanged
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool IsChanged(int value)
        {
            return Has(value) &&
                   value != _selectedIndex;
        }

        #endregion
    }
}
