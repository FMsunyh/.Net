﻿using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RIB.Visual.Workshop.BP.View
{
    /// <summary>
    /// Interaction logic for UIBusinessPartnersDataGrid.xaml
    /// </summary>
    public partial class UIBusinessPartnersDataGrid : UserControl
    {

        public UIBusinessPartnersDataGrid()
        {
            InitializeComponent();
        }

        private void dgBusinessPartnersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //SubsidiariesLibrary = SbService.GetSubsidiary(SelectedItem.Id);

            //SubsidiariesList = SubsidiariesLibrary.ToObservableCollection();
        }
    }
}
