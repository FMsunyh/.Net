using RIB.Visual.Workshop.BP.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIB.Visual.Workshop.BP.Core.ISelector
{
    public interface ISubsidiarySelector
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
        Subsidiary SelectedItem
        {
            get;
        }
    }
}
