using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Trainee.Annotations;

namespace Trainee.Models
{
    /// <summary>
    ///     Gender
    /// </summary>
    public enum GenderId
    {
        /// <summary>
        ///     female
        /// </summary>
        Female = 0,

        /// <summary>
        ///     male
        /// </summary>
        Male = 1,
    }

    /// <summary>
    ///     Gender
    /// </summary>
    public class Gender : ModelBase, IEquatable<Gender>
    {
        #region ModelBase

        /// <summary>
        ///     Id
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Gender other)
        {
            return other != null &&
                   other.Id == Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Gender);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id;
        }

        #endregion
    }

    /// <summary>
    ///     Female
    /// </summary>
    public class FemaleGender : Gender
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get
            {
                return (int) GenderId.Female;
            }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get
            {
                return "Female";
            }
        }
    }

    /// <summary>
    ///     MaleGender
    /// </summary>
    public class MaleGender : Gender
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get
            {
                return (int) GenderId.Male;
            }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get
            {
                return "Male";
            }
        }
    }

    /// <summary>
    ///     GenderSelector
    /// </summary>
    public class GenderSelector : ModelObserver<Gender>
    {
        /// <summary>
        ///     Constructors
        /// </summary>
        public GenderSelector()
            : base(
                new List<Gender>
                    {
                        new MaleGender(),
                        new FemaleGender()
                    }
                )
        {

        }
    }
}
