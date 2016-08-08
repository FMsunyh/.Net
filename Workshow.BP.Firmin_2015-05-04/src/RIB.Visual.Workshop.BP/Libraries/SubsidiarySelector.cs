using RIB.Visual.Workshop.BP.Core.ISelector;
using RIB.Visual.Workshop.BP.Core.Models;
using RIB.Visual.Workshop.BP.Core.Service;
using RIB.Visual.Workshop.BP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RIB.Visual.Workshop.BP.Libraries
{
    /// <summary>
    ///     class SubsidiarySelector
    /// </summary>
    public class SubsidiarySelector : INotifyPropertyChanged, ISubsidiarySelector
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
        protected readonly ObservableCollection<Subsidiary> _dataSource;

        /// <summary>
        ///     _searchDataSource
        /// </summary>
        protected List<Subsidiary> _searchDataSource;

        /// <summary>
        ///     _subsidiaryService
        /// </summary>
        protected readonly EntityFrameWorkSubsidiaryService _subsidiaryService;

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
        private Subsidiary _selectedItem;

        /// <summary>
        ///     SelectedItem
        /// </summary>
        public Subsidiary SelectedItem
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
        ///     SubsidiarySelector
        /// </summary>
        public SubsidiarySelector(ISubsidiaryService service)
        {
            _subsidiaryService = (EntityFrameWorkSubsidiaryService)service;

            _dataSource = service == null ?
                new ObservableCollection<Subsidiary>()
                : new ObservableCollection<Subsidiary>(service.GetSubsidiary(1)).ToObservableCollection<Subsidiary>();

            _searchDataSource = _dataSource.ToList<Subsidiary>(); 
        }

        /// <summary>
        ///     Save
        /// </summary>
        /// <param name="subsidiary"></param>
        /// <returns></returns>
        public virtual int Save(Subsidiary subsidiary)
        {
            if (subsidiary == null)
                return -1;
            var id = subsidiary.Id;
            int index;
            if (_indexes.TryGetValue(id, out index))
            {
                //already exist
                _dataSource[index] = subsidiary; //update
                _defaultDataView.Refresh();
                _subsidiaryService.Update(subsidiary);
            }
            else
            {
                index = _dataSource.Count;
                _indexes.Add(id, index);
                _dataSource.Add(subsidiary);
                _subsidiaryService.Add(subsidiary);
            }
            return index;
        }

        /// <summary>
        ///     Delete
        /// </summary>
        /// <param name="subsidiary"></param>
        /// <returns></returns>
        public virtual int Delete(Subsidiary subsidiary)
        {
            if (subsidiary == null)
            {
                return -1;
            }
            _dataSource.Remove(subsidiary);
            _subsidiaryService.Delete(subsidiary.Id);
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

            List<Subsidiary> newDataSource = new List<Subsidiary>();
            foreach (var item in _searchDataSource)
            {
                if (item.Description.Contains(filterText))
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

        /// <summary>
        ///     DataSourceChanged
        /// </summary>
        /// <param name="bpId"></param>
        public void DataSourceChanged(int bpId)
        {
            IEnumerable<Subsidiary> dataSource = _subsidiaryService.GetSubsidiary(bpId);
            if (!Equals(dataSource, _dataSource))
            {
                _dataSource.Clear();

                foreach (var item in dataSource)
                {
                    _dataSource.Add(item);
                }

                _searchDataSource = _dataSource.ToList<Subsidiary>();
            }
        }
    }
}
