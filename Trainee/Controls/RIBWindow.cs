using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace Trainee.Controls
{
    /// <summary>
    ///     Aero
    /// </summary>
    public class RIBWindow : System.Windows.Window
    {
        #region Margins

        /// <summary>
        ///     
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        protected struct MARGINS
        {
            /// <summary>
            ///     This Constructor
            /// </summary>
            /// <param name="thickness"></param>
            public MARGINS(Thickness thickness)
            {
                Left = (int)thickness.Left;
                Right = (int)thickness.Right;
                Top = (int)thickness.Top;
                Bottom = (int)thickness.Bottom;
            }

            /// <summary>
            ///     Left
            /// </summary>
            public int Left;

            /// <summary>
            ///     Right
            /// </summary>
            public int Right;

            /// <summary>
            ///     Top
            /// </summary>
            public int Top;

            /// <summary>
            ///     Bottom
            /// </summary>
            public int Bottom;
        }

        #endregion

        #region Static Methods

        /// <summary>
        ///     DwmExtendFrameIntoClientArea
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="pMargins"></param>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        protected static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMargins);

        /// <summary>
        ///     DwmIsCompositionEnabled
        /// </summary>
        /// <returns></returns>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        protected static extern bool DwmIsCompositionEnabled();

        /// <summary>
        ///     Frame
        /// </summary>
        /// <param name="window"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        protected static bool Frame(Window window, Thickness thickness)
        {
            if (!DwmIsCompositionEnabled())
                return false;

            IntPtr hWnd = new WindowInteropHelper(window).Handle;
            if (hWnd == IntPtr.Zero)
                throw new InvalidOperationException();

            window.Background = Brushes.Transparent;
            HwndSource.FromHwnd(hWnd).CompositionTarget.BackgroundColor = Colors.Transparent;

            MARGINS margins = new MARGINS(thickness);
            DwmExtendFrameIntoClientArea(hWnd, ref margins);
            return true;
        }

        #endregion

        #region OnSourceInitialized

        /// <summary>
        ///     OnSourceInitialized
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            Frame(this, new Thickness(-1));
        }

        #endregion
    }
}
