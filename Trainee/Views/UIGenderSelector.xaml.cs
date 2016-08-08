using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trainee.Models;

namespace Trainee.Views
{
    /// <summary>
    /// Interaction logic for UIGenderSelector.xaml
    /// </summary>
    public partial class UIGenderSelector
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public UIGenderSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     GenderProperty
        /// </summary>
        public static readonly DependencyProperty GenderProperty = DependencyProperty.Register(
            "Gender", typeof (Gender), typeof (UIGenderSelector),
            new PropertyMetadata(new MaleGender(),
                                 (sender, args) =>
                                     {
                                         var selector = sender as UIGenderSelector;
                                         if (selector != null)
                                         {
                                             var nv = args.NewValue as Gender;
                                             var ov = args.OldValue as Gender;
                                             if (nv != null &&
                                                 !nv.Equals(ov))
                                             {
                                                 if (nv.Id == (int) GenderId.Male)
                                                     selector.OnMale();
                                                 else if (nv.Id == (int) GenderId.Female)
                                                     selector.OnFemale();
                                             }
                                         }
                                     })
            );

        /// <summary>
        ///     Gender
        /// </summary>
        public Gender Gender
        {
            get { return GetValue(GenderProperty) as Gender; }
            set { SetValue(GenderProperty, value); }
        }

        /// <summary>
        ///     
        /// </summary>
        protected virtual void OnMale()
        {
            var opa = new DoubleAnimation
                {
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1)
                };
            Storyboard.SetTarget(opa, maleBorder);
            Storyboard.SetTargetProperty(opa, new PropertyPath("(UIElement.Opacity)"));

            var opb = new DoubleAnimation
                {
                    To = 0, 
                    Duration = TimeSpan.FromSeconds(1)
                };
            Storyboard.SetTarget(opb, femaleBorder);
            Storyboard.SetTargetProperty(opb, new PropertyPath("(UIElement.Opacity)"));

            var sto = new Storyboard();
            sto.Children.Add(opa);
            sto.Children.Add(opb);

            sto.Begin();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMale(object sender, MouseButtonEventArgs e)
        {
            Gender = new MaleGender();
        }

        /// <summary>
        ///     OnFemale
        /// </summary>
        protected virtual void OnFemale()
        {
            var opa = new DoubleAnimation
                {
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1)
                };
            Storyboard.SetTarget(opa, maleBorder);
            Storyboard.SetTargetProperty(opa, new PropertyPath("(UIElement.Opacity)"));

            var opb = new DoubleAnimation
                {
                    To = 1, 
                    Duration = TimeSpan.FromSeconds(1)
                };
            Storyboard.SetTarget(opb, femaleBorder);
            Storyboard.SetTargetProperty(opb, new PropertyPath("(UIElement.Opacity)"));

            var sto = new Storyboard();
            sto.Children.Add(opa);
            sto.Children.Add(opb);
            sto.Begin();
        }

        /// <summary>
        ///     OnFemale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnFemale(object sender, MouseButtonEventArgs e)
        {
            Gender = new FemaleGender();
        }
    }
}
