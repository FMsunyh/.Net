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
    public class VMSubsidiariesMenu:VMMenu
    {
        #region Fields

        /// <summary>
        ///     
        /// </summary>
        protected readonly SubsidiarySelector _selector;

        #endregion

        #region Constructor

        /// <summary>
        ///     VMSubsidiariesMenu
        /// </summary>
        /// <param name="selector"></param>
        public VMSubsidiariesMenu(ISubsidiarySelector selector)
        {
            _selector = selector == null
            ? new SubsidiarySelector(ServiceGateway.SBService)
            : (SubsidiarySelector)selector;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     OnAddButtonClicked
        /// </summary>
        protected override void OnAddButtonClicked()
        {
            base.OnAddButtonClicked();

            var vm = new ViewModels.VMSubsidirayCreator(_selector);
            var ui = new View.UISubsidiaryCreator
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
                Title = "Subsidiary Factory",
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
            var subsidiray = _selector.SelectedItem;
            if (subsidiray != null)
                _selector.Delete(subsidiray);
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
