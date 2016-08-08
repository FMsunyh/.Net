using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIB.Visual.Workshop.BP.Core.Models;
using RIB.Visual.Workshop.BP.Core.Service;
using System.Windows.Data;
using RIB.Visual.Workshop.BP.Core.ISelector;

namespace RIB.Visual.Workshop.BP.Libraries
{
    /// <summary>
    ///     class BusinessPartnerSelector
    /// </summary>
    public class BusinessPartnerSelector : INotifyPropertyChanged, IBusinessPartnerSelector
    {
        /// <summary>
        ///     PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     _indexes
        /// </summary>
        private readonly Dictionary<int, int> _indexes = new Dictionary<int, int>();

        /// <summary>
        ///     _defaultDataView
        /// </summary>
        private ICollectionView _defaultDataView;

        /// <summary>
        ///     _dataSource
        /// </summary>
        protected readonly ObservableCollection<BusinessPartner> _dataSource;

        /// <summary>
        ///     _searchDataSource
        /// </summary>
        protected readonly List<BusinessPartner> _searchDataSource;

        /// <summary>
        ///     _bpService
        /// </summary>
        protected readonly EntityFrameWorkBusinessPartnerService _bpService;

        /// <summary>
        ///     DataView
        /// </summary>
        public virtual ICollectionView DataView
        {
            get
            {
                if (_defaultDataView == null)
                {
                    _defaultDataView = CollectionViewSource.GetDefaultView(_dataSource);
                }
                return _defaultDataView;
            }
        }

        /// <summary>
        ///     _selectIndex
        /// </summary>
        private int _selectIndex;

        /// <summary>
        ///     SelectedIndex
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return _selectIndex;
            }
            set
            {
                if (value != _selectIndex)
                {
                    _selectIndex = value;
                    var handler = PropertyChanged;
                    if (handler != null)
                        handler(this, new PropertyChangedEventArgs("SelectedIndex"));
                }
            }
        }

        /// <summary>
        ///     _selectedItem
        /// </summary>
        private BusinessPartner _selectedItem;

        /// <summary>
        ///     SelectedItem
        /// </summary>
        public BusinessPartner SelectedItem
        {
            get
            {
                if (_selectIndex > -1 &&
                    _selectIndex < _dataSource.Count)
                {
                    _selectedItem = _dataSource[_selectIndex];
                }
                return _selectedItem;
            }

            set { }
        }

        /// <summary>
        ///     BusinessPartnerSelector
        /// </summary>
        public BusinessPartnerSelector(IBusinessPartnerService service)
        {
            _bpService = (EntityFrameWorkBusinessPartnerService)service;
            _dataSource = service == null ?
                new ObservableCollection<BusinessPartner>()
                : new ObservableCollection<BusinessPartner>(service.Load());
            _searchDataSource = _dataSource.ToList<BusinessPartner>();
        }

        //public virtual int ReLoad()
        //{
        //    IEnumerable<BusinessPartner> newDatasource = new ObservableCollection<BusinessPartner>(_bpService.Load());
        //    foreach(var item in newDatasource)
        //    {
        //        _dataSource.Add(item); 
        //    }
        //    return 1;
        //}

        /// <summary>
        ///     Save()
        /// </summary>
        /// <param name="bp"></param>
        /// <returns></returns>
        public virtual int Save(BusinessPartner bp)
        {
            if (bp == null)
                return -1;
            var id = bp.Id;
            int index;
            if (_indexes.TryGetValue(id, out index))
            {
                //already exist
                _dataSource[index] = bp; //update
                _defaultDataView.Refresh();
                _bpService.Update(bp);
            }
            else
            {
                index = _dataSource.Count;
                _indexes.Add(id, index);
                _dataSource.Add(bp);
                _bpService.Add(bp);
            }
            return index;
        }

        /// <summary>
        ///     Delete()
        /// </summary>
        /// <param name="bp"></param>
        /// <returns></returns>
        public virtual int Delete(BusinessPartner bp)
        {
            if (bp == null)
            {
                return -1;
            }
            _dataSource.Remove(bp);
            _bpService.Delete(bp.Id);
            return 1;
        }

        /// <summary>
        ///     Search
        /// </summary>
        /// <param name="filterText"></param>
        /// <returns></returns>
        public virtual int Search(string filterText)
        {
            //IEnumerable<BusinessPartner> newDataSource = _dataSource.Select(x => x.CompanyName.Equals(filterText) ? x : null);
            //var newDataSource = from businessPartner in _dataSource
            //                    where businessPartner.CompanyName.Contains("")
            //                    select businessPartner;

            List<BusinessPartner> newDataSource = new List<BusinessPartner>();
            foreach (var item in _searchDataSource)
            {
                if (item.CompanyName.Contains(filterText))
                {
                    newDataSource.Add(item);
                }
            }

            if (newDataSource != null)
            {
                _dataSource.Clear();
                foreach (var item in newDataSource)
                {
                    _dataSource.Add(item);
                }
            }
            return 1;
        }
    }
}
