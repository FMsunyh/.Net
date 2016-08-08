using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Trainee.Models
{
    /// <summary>
    ///     Staff
    /// </summary>
    [JsonObject]
    public class Staff : IEquatable<Staff>
    {
        #region Properties

        /// <summary>
        ///     StaffNo
        /// </summary>
        [JsonProperty]
        public int StaffNo { get; set; }

        /// <summary>
        ///     Chinese
        /// </summary>
        [JsonProperty]
        public string Chinese { get; set; }

        /// <summary>
        ///     ENName
        /// </summary>
        [JsonProperty]
        public string English { get; set; }

        /// <summary>
        ///     Department
        /// </summary>
        [JsonProperty]
        public Position Position { get; set; }

        /// <summary>
        ///     Gender
        /// </summary>
        [JsonProperty]
        public Gender Gender { get; set; }

        /// <summary>
        ///     
        /// </summary>
        [JsonProperty]
        public int State { get; set; }

        #endregion

        #region Promotion

        /// <summary>
        ///     Promotion
        /// </summary>
        public void Promotion(Position newPosition)
        {
            Position = newPosition;
        }

        #endregion

        #region Resign

        /// <summary>
        ///     Resign
        /// </summary>
        public void Resign()
        {
            State = 1;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Staff other)
        {
            return other != null &&
                   other.StaffNo == StaffNo;
        }

        /// <summary>
        ///     Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Staff);
        }

        /// <summary>
        ///     GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return StaffNo;
        }

        #endregion

        #region Comparer

        /// <summary>
        /// /   lazy
        /// </summary>
        protected static readonly Lazy<StaffComparer> _lazy = new Lazy<StaffComparer>(
            () => new StaffComparer()
            );

        /// <summary>
        ///     Comparer
        /// </summary>
        public static IEqualityComparer<Staff> Comparer
        {
            get { return _lazy.Value; }
        }

        /// <summary>
        ///     StaffComparer
        /// </summary>
        protected sealed class StaffComparer : IEqualityComparer<Staff>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public bool Equals(Staff x, Staff y)
            {
                return x.StaffNo == y.StaffNo;
            }

            /// <summary>
            ///     
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int GetHashCode(Staff obj)
            {
                return obj.StaffNo;
            }
        }

        #endregion
    }
}
