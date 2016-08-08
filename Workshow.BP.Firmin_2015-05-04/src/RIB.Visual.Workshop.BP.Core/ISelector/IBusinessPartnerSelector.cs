using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using RIB.Visual.Workshop.BP.Core.Models;

namespace RIB.Visual.Workshop.BP.Core.ISelector
{
    /// <summary>
    ///     interface IBusinessPartnerSelector
    /// </summary>
    public interface IBusinessPartnerSelector
    {
        /// <summary>
        ///     DataView
        /// </summary>
        ICollectionView DataView
        {
            get;
        }

        /// <summary>
        ///     SelectedIndex
        /// </summary>
        int SelectedIndex
        {
            get;
            set;
        }

        /// <summary>
        ///     SelectedItem
        /// </summary>
        BusinessPartner SelectedItem
        {
            get;
        }
    }
}
