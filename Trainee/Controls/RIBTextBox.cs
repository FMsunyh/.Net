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
    ///     RIBTextBox
    /// </summary>
    public class RIBTextBox : System.Windows.Controls.TextBox
    {
        #region TextChangedCommand

        /// <summary>
        ///     This is a Attached Property
        /// </summary>
        public static readonly DependencyProperty TextChangedCommandProperty = DependencyProperty.Register(
            "TextChangedCommand",
            typeof(ICommand),
            typeof(RIBTextBox)
            );

        /// <summary>
        ///     TextChangedCommand
        /// </summary>
        public ICommand TextChangedCommand
        {
            get { return (ICommand)GetValue(TextChangedCommandProperty); }
            set { SetValue(TextChangedCommandProperty, value); }
        }

        /// <summary>
        ///     OnTextChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(System.Windows.Controls.TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            ICommand command = TextChangedCommand;
            if (command != null &&
                command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        #endregion

        #region LostFocusCommand

        /// <summary>
        ///     This is a Attached Property
        /// </summary>
        public static readonly DependencyProperty LostFocusCommandProperty = DependencyProperty.Register(
            "LostFocusCommand",
            typeof(ICommand),
            typeof(RIBTextBox)
            );

        /// <summary>
        ///     LostFocusCommand
        /// </summary>
        public ICommand LostFocusCommand
        {
            get { return (ICommand)GetValue(LostFocusCommandProperty); }
            set { SetValue(LostFocusCommandProperty, value); }
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            ICommand command = LostFocusCommand;
            if (command != null &&
                command.CanExecute(e))
            {
                command.Execute(e);
            }
        }

        #endregion
    }
}
