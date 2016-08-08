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
    ///     class ContactSelector
    /// </summary>
    public class ContactSelector : INotifyPropertyChanged, IContactSelector
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
        private ObservableCollection<Contact> _dataSource;

        /// <summary>
        ///     _searchDataSource
        /// </summary>
        protected List<Contact> _searchDataSource;

        /// <summary>
        ///     _contactService
        /// </summary>
        protected readonly EntityFrameWorkContactService _contactService;

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
        private Contact _selectedItem;

        /// <summary>
        ///     SelectedItem
        /// </summary>
        public Contact SelectedItem
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
        ///     ContactSelector
        /// </summary>
        public ContactSelector(IContactService service)
        {
            _contactService = (EntityFrameWorkContactService)service;
            _dataSource = service == null ?
                new ObservableCollection<Contact>()
                : new ObservableCollection<Contact>(service.GetContact(1)).ToObservableCollection<Contact>();
            _searchDataSource = _dataSource.ToList<Contact>();
        }

        /// <summary>
        ///     Save
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public virtual int Save(Contact contact)
        {
            if (contact == null)
                return -1;
            var id = contact.Id;
            int index;
            if (_indexes.TryGetValue(id, out index))
            {
                //already exist
                _dataSource[index] = contact; //update
                _defaultDataView.Refresh();
                _contactService.Update(contact);
            }
            else
            {
                index = _dataSource.Count;
                _indexes.Add(id, index);
                _dataSource.Add(contact);
                _contactService.Add(contact);
            }
            return index;
        }

        /// <summary>
        ///     Delete
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public virtual int Delete(Contact contact)
        {
            if (contact == null)
            {
                return -1;
            }
            _dataSource.Remove(contact);
            _contactService.Delete(contact.Id);
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

            List<Contact> newDataSource = new List<Contact>();
            foreach (var item in _searchDataSource)
            {
                if (item.Name.Contains(filterText))
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
            IEnumerable<Contact> dataSource = _contactService.GetContact(bpId);
            if (!Equals(dataSource, _dataSource))
            {
                _dataSource.Clear();

                //_dataSource = dataSource.ToObservableCollection<Contact>()
                foreach (var item in dataSource)
                {
                    _dataSource.Add(item);
                }

                _searchDataSource = _dataSource.ToList<Contact>();
            }
        }
    }
}
