using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Newtonsoft.Json;

namespace Trainee.Models
{
    /// <summary>
    ///     RIBStaffBase
    /// </summary>
    public class StaffManager
    {
        #region Fields

        private readonly ObservableCollection<Staff> _dataSource = new ObservableCollection<Staff>();
        private readonly Dictionary<int, int> _indexes = new Dictionary<int, int>();
        private ICollectionView _defaultDataView;

        #endregion

        #region Methods

        /// <summary>
        ///     Contains
        /// </summary>
        /// <param name="staffNo"></param>
        /// <returns></returns>
        public virtual bool Contains(int staffNo)
        {
            return _indexes.ContainsKey(staffNo);
        }

        /// <summary>
        ///     Insert
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public virtual int Save(Staff staff)
        {
            if (staff == null)
                return -1; //Null Object

            var staffNo = staff.StaffNo;
            int index;
            if (_indexes.TryGetValue(staffNo, out index))
            {
                //already exist
                _dataSource[index] = staff; //update
                _defaultDataView.Refresh();
            }
            else
            {
                index = _dataSource.Count;
                _indexes.Add(staffNo, index);
                _dataSource.Add(staff);
            }
            return index;
        }

        /// <summary>
        ///     Delete
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public virtual int Delete(Staff staff)
        {
            if (staff == null)
                return -1;

            var staffNo = staff.StaffNo;
            int index;
            if (_indexes.TryGetValue(staffNo, out index) &&
                _indexes.Remove(staffNo))
            {
                _dataSource.RemoveAt(index);
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        ///     Clear
        /// </summary>
        public void Clear()
        {
            _dataSource.Clear();
        }

        /// <summary>
        ///     CreateSerializer
        /// </summary>
        /// <returns></returns>
        public StaffSerializer CreateSerializer()
        {
            return new StaffSerializer
                {
                    Data = _dataSource.ToArray()
                };
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Count
        /// </summary>
        public int Count
        {
            get
            {
                return _indexes.Count;
            }
        }

        /// <summary>
        ///     DataView
        /// </summary>
        public ICollectionView DataView
        {
            get
            {
                if (_defaultDataView == null)
                {
                    _defaultDataView = CollectionViewSource.GetDefaultView(_dataSource);
                    _defaultDataView.SortDescriptions.Add(new SortDescription("State", ListSortDirection.Ascending));
                }
                return _defaultDataView;
            }
        }

        /// <summary>
        ///     Selected
        /// </summary>
        public Staff Selected { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     StaffManager
        /// </summary>
        public StaffManager()
        {
           
        }

        /// <summary>
        ///     StaffManager
        /// </summary>
        public StaffManager(IEnumerable<Staff> staffs)
        {
            if (staffs != null)
            {
                foreach (var staff in staffs.Distinct(Staff.Comparer))
                {
                    var index = _dataSource.Count;
                    _indexes.Add(staff.StaffNo, index);
                    _dataSource.Add(staff);
                }
            }
        }

        #endregion
    }
}