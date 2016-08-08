using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee.Models
{
    /// <summary>
    ///     IModelSelector
    /// </summary>
    public interface IModelSelector<TModel> : INotifyPropertyChanged where TModel : ModelBase
    {
        /// <summary>
        ///     SelectedIndex
        /// </summary>
        int SelectedIndex { get; set; }

        /// <summary>
        ///     Selected
        /// </summary>
        TModel Selected { get; }

        /// <summary>
        ///     SeekToFirst
        /// </summary>
        /// <returns></returns>
        Tuple<int, TModel> First();

        /// <summary>
        ///     SeekToLast
        /// </summary>
        Tuple<int, TModel> Last();

        /// <summary>
        ///     SeekToIndex
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Tuple<int, TModel> Index(int index);

        /// <summary>
        ///     SeekToModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Tuple<int, TModel> Seek(TModel model);

        /// <summary>
        ///     Next
        /// </summary>
        /// <returns></returns>
        Tuple<int, TModel> Next();

        /// <summary>
        ///     Previous
        /// </summary>
        /// <returns></returns>
        Tuple<int, TModel> Previous();
    }
}
