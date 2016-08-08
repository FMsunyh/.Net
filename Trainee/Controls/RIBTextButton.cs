using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Trainee.Controls
{
    /// <summary>
    ///     RIBTextButton
    /// </summary>
    public class RIBTextButton : System.Windows.Controls.TextBlock
    {
        #region Constructors

        /// <summary>
        ///     RIBTextBlock
        /// </summary>
        public RIBTextButton()
        {
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            var style = App.Current.TryFindResource("textButton") as Style;
            if (style != null)
                Style = style;
        }

        #endregion

        #region ClickCommand

        /// <summary>
        ///     This is a Attached Property
        /// </summary>
        public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register(
            "MouseLeftButtonClick",
            typeof(ICommand),
            typeof(RIBTextButton)
            );

        /// <summary>
        ///     ClickCommand
        /// </summary>
        public ICommand MouseLeftButtonClick
        {
            get { return GetValue(ClickCommandProperty) as ICommand; }
            set { SetValue(ClickCommandProperty, value); }
        }

        /// <summary>
        ///     OnMouseLeftButtonDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMouseLeftButtonDown(
            object sender,
            MouseButtonEventArgs e
            )
        {
            var command = GetValue(ClickCommandProperty) as ICommand;
            if (command != null &&
                command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        #endregion
    }
}
