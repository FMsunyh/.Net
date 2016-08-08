using GalaSoft.MvvmLight.Command;
using RIB.Visual.Workshop.BP.Core.ISelector;
using RIB.Visual.Workshop.BP.Core.Service;
using RIB.Visual.Workshop.BP.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RIB.Visual.Workshop.BP.ViewModels
{
    /// <summary>
    ///     class VMBusinessPartnerMenu
    /// </summary>
    public class VMBusinessPartnerMenu : VMMenu
    {
        #region Fields

        /// <summary>
        ///     _selector
        /// </summary>
        protected readonly BusinessPartnerSelector _selector;

        #endregion

        #region Constructor

        /// <summary>
        ///     VMBusinessPartnerMenu
        /// </summary>
        /// <param name="selector"></param>
        public VMBusinessPartnerMenu()
        {
        }

        /// <summary>
        ///     VMBusinessPartnerMenu
        /// </summary>
        /// <param name="selector"></param>
        public VMBusinessPartnerMenu(IBusinessPartnerSelector selector)
        {
            _selector = selector == null
            ? new BusinessPartnerSelector(ServiceGateway.BPService)
            : (BusinessPartnerSelector)selector;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     OnAddButtonClicked
        /// </summary>
        protected override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();

            var vm = new ViewModels.VMBusinessPartnerCreator(_selector);
            var ui = new View.UIBusinessPartnerCreator
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
                Title = "BusinessPartner Factory",
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
            var bp = _selector.SelectedItem;
            if (bp != null)
                _selector.Delete(bp);
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
