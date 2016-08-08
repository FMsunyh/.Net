using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Newtonsoft.Json;
using Trainee.Annotations;

namespace Trainee.Models
{
    /// <summary>
    ///     PositionId
    /// </summary>
    public enum PositionId
    {
        /// <summary>
        ///     Developer
        /// </summary>
        DEV,
        /// <summary>
        ///     ProjectManager
        /// </summary>
        PM,
        /// <summary>
        ///     Tester
        /// </summary>
        TER,
        /// <summary>
        ///     System Architect
        /// </summary>
        SA,
        /// <summary>
        ///     User Interface
        /// </summary>
        UI
    }

    /// <summary>
    ///     Position
    /// </summary>
    public class Position : ModelBase, IEquatable<Position>
    {
        #region Properties

        /// <summary>
        ///     GroupId
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description { get; set; }

        /// <summary>
        ///     Grade
        /// </summary>
        public virtual Grade Grade { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Position other)
        {
            return other != null &&
                   other.Id == Id &&
                   (other.Grade != null &&
                    Grade != null &&
                    other.Grade.Id == Grade.Id
                   );
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            return Equals(other as Position);
        }

        /// <summary>
        ///     GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Id;
                hashCode = (hashCode*397) ^ (Grade != null ? Grade.Id : 0);
                return hashCode;
            }
        }

        #endregion
    }

    /// <summary>
    ///     TraineeDeveloper
    /// </summary>
    public class Developer : Position
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static readonly Developer Default = new Developer();

        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) PositionId.DEV; }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Develper"; }
        }
    }

    /// <summary>
    ///     Tester
    /// </summary>
    public class Tester : Position
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static readonly Tester Default = new Tester();

        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int)PositionId.TER; }
            set { }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Tester"; }
            set { }
        }
    }

    /// <summary>
    ///     SystemArchitect
    /// </summary>
    public class SystemArchitect : Position
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static readonly SystemArchitect Default = new SystemArchitect();

        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int)PositionId.SA; }
            set { }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "System Architect"; }
            set { }
        }
    }

    /// <summary>
    ///     UserInterface
    /// </summary>
    public class UserInterface : Position
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static readonly UserInterface Default = new UserInterface();

        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int)PositionId.UI; }
            set { }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "User Interface Designer"; }
            set { }
        }
    }

    /// <summary>
    ///     ProjectManager
    /// </summary>
    public class ProjectManager : Position
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static readonly ProjectManager Default = new ProjectManager();

        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) PositionId.PM; }
            set { }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Project Manager"; }
            set { }
        }
    }

    /// <summary>
    ///     PositionSelector
    /// </summary>
    public class PositionSelector : ModelObserver<Position>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public PositionSelector()
            : base(new Position[]
                {
                    Developer.Default,
                    ProjectManager.Default,
                    Tester.Default,
                    UserInterface.Default,
                    SystemArchitect.Default
                })
        {

        }
    }
}
