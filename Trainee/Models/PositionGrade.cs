using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainee.Annotations;

namespace Trainee.Models
{
    /// <summary>
    ///     PositionGrade
    /// </summary>
    public enum PositionGradeId
    {
        /// <summary>
        ///     Trainee
        /// </summary>
        Trainee = 0,

        /// <summary>
        ///     Junior
        /// </summary>
        Junior = 1,

        /// <summary>
        ///     Normal
        /// </summary>
        Normal = 2,

        /// <summary>
        ///     Senior
        /// </summary>
        Senior = 3
    }

    /// <summary>
    ///     Grade
    /// </summary>
    public class Grade : ModelBase , IEquatable<Grade>
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description { get; set; }

        /// <summary>
        ///     Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Grade other)
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
            return Equals(obj as Grade);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id;
        }
    }

    /// <summary>
    ///     TraineeGrade
    /// </summary>
    public class TraineeGrade : Grade
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static readonly TraineeGrade Default = new TraineeGrade();

        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) PositionGradeId.Trainee; }
            set { }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Trainee"; }
            set { }
        }
    }

    /// <summary>
    ///     JuniorGrade
    /// </summary>
    public class JuniorGrade : Grade
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static readonly JuniorGrade Default = new JuniorGrade();

        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int)PositionGradeId.Junior; }
            set { }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Junior"; }
            set { }
        }
    }

    /// <summary>
    ///     NormalGrade
    /// </summary>
    public class NormalGrade : Grade
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static readonly NormalGrade Default = new NormalGrade();

        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int)PositionGradeId.Normal; }
            set { }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Normal"; }
            set { }
        }
    }

    /// <summary>
    ///     SeniorGrade
    /// </summary>
    public class SeniorGrade : Grade
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static readonly SeniorGrade Default = new SeniorGrade();

        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) PositionGradeId.Senior; }
            set { }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Senior"; }
            set { }
        }
    }

    /// <summary>
    ///     GradeSelector
    /// </summary>
    public class GradeSelector : ModelObserver<Grade>
    {
        /// <summary>
        /// 
        /// </summary>
        public GradeSelector()
            : base(new List<Grade>
                {
                    TraineeGrade.Default,
                    JuniorGrade.Default,
                    NormalGrade.Default,
                    SeniorGrade.Default
                })
        {
            SelectedIndex = 0;
        }
    }
}
