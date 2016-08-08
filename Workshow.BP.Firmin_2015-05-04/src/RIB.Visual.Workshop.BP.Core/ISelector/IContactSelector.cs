using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIB.Visual.Workshop.BP.Core.Models;

namespace RIB.Visual.Workshop.BP.Core.ISelector
{
    /// <summary>
    ///     interface IContactSelector
    /// </summary>
    public interface IContactSelector
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
        Contact SelectedItem
        {
            get;
        }
    }
}
