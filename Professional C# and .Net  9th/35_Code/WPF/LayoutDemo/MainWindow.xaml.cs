﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LayoutDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowStackPanel(object sender, RoutedEventArgs e)
        {
            new StackPanelWindow().Show();
        }

        private void ShowWrapPanel(object sender, RoutedEventArgs e)
        {
            new WrapPanelWindow().Show();
        }

        private void ShowCanvas(object sender, RoutedEventArgs e)
        {
            new CanvasWindow().Show();
        }

        private void ShowDockPanel(object sender, RoutedEventArgs e)
        {
            new DockPanelWindow().Show();
        }

        private void ShowGrid(object sender, RoutedEventArgs e)
        {
            new GridWindow().Show();
        }

    }
}
