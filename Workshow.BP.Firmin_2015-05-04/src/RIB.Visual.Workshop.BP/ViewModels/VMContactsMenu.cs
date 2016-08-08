using RIB.Visual.Workshop.BP.Core.ISelector;
using RIB.Visual.Workshop.BP.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RIB.Visual.Workshop.BP.ViewModels
{
    public class VMContactsMenu:VMMenu
    {
        #region Fields

        /// <summary>
        ///     
        /// </summary>
        protected readonly ContactSelector _selector;

        #endregion

        #region Constructor

        /// <summary>
        ///     
        /// </summary>
        /// <param name="selector"></param>
        public VMContactsMenu(IContactSelector selector)
        {
            _selector = selector == null
            ? new ContactSelector(ServiceGateway.CTService)
            : (ContactSelector)selector;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     OnAddButtonClicked
        /// </summary>
        protected override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();

            var vm = new ViewModels.VMContactCreator(_selector);
            var ui = new View.UIContactCreator
            {
                DataContext = vm
            };
            var window = new Window
            {
                Height = 400,
                Width = 500,
                ShowInTaskbar = true,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.None,
                Title = "Contact Factory",
                Content = ui
            };
            App.Current.Properties["creator"] = window;
            window.ShowDialog();
        }

        /// <summary>
        ///     OnAddButtonClicked
        /// </summary>
        protected override void OnDeleteButtonClicked()
        {
            base.OnDeleteButtonClicked();
            var contact = _selector.SelectedItem;
            if (contact != null)
                _selector.Delete(contact);
        }

        /// <summary>
        ///     TextChanged
        /// </summary>
        protected override void TextChanged()
        {
            base.TextChanged();

            if (this.FilterText != null)
            {
                _selector.Search(FilterText);
            }
        }

        #endregion
    }
}
