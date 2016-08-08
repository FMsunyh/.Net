using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIB.Visual.Workshop.BP.ViewModels
{
    public static class ExtendMethods
    {
        /// <summary>
        /// IEnumerable<T> Convert to ObservableCollection<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerableList"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableList)
        {
            if (enumerableList != null)
            {
                var observableCollection = new ObservableCollection<T>();
                foreach (var e in enumerableList)
                {
                    observableCollection.Add(e);
                }
                return observableCollection;
            }
            return null;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observableCollection"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToIEnumerable<T>(this ObservableCollection<T> observableCollection)
        {
            IEnumerable<T> enumerableList = null;
            if (observableCollection != null)
            {
                foreach (var e in observableCollection)
                {
                    enumerableList = observableCollection.Cast<T>();
                }
            }
            return enumerableList;
        }
    }
}
