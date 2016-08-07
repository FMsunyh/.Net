﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Microsoft.Win32;

namespace Wrox.ProCSharp.Documents
{
  /// <summary>
  /// Interaction logic for DocumentPage.xaml
  /// </summary>
  public partial class DocumentPage : Page
  {
    private ObservableCollection<MenuEntry> menus;
    private FixedDocument fixedDocument;

    public DocumentPage()
    {
      InitializeComponent();
    }

    internal void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
    {
      menus = e.ExtraData as ObservableCollection<MenuEntry>;

      fixedDocument = new FixedDocument();
      var pageContent1 = new PageContent();
      fixedDocument.Pages.Add(pageContent1);
      var page1 = new FixedPage();
      pageContent1.Child = page1;
      page1.Children.Add(GetHeaderContent());
      page1.Children.Add(GetLogoContent());
      page1.Children.Add(GetDateContent());
      page1.Children.Add(GetMenuContent());

      viewer.Document = fixedDocument;

      NavigationService.LoadCompleted -= NavigationService_LoadCompleted;
    }

    private static UIElement GetHeaderContent()
    {
      var text1 = new TextBlock
      {
        FontFamily = new FontFamily("Segoe UI"),
        FontSize = 34,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      text1.Inlines.Add(new Bold(new Run("cn|elements")));
      FixedPage.SetLeft(text1, 170);
      FixedPage.SetTop(text1, 40);
      return text1;
    }

    private static UIElement GetLogoContent()
    {
      var ellipse = new Ellipse
      {
        Width = 90,
        Height = 40,
        Fill = new RadialGradientBrush(Colors.Yellow, Colors.DarkRed)
      };

      FixedPage.SetLeft(ellipse, 600);
      FixedPage.SetTop(ellipse, 50);
      return ellipse;
    }

    private UIElement GetDateContent()
    {
      string dateString = String.Format("{0:d} to {1:d}",
          menus[0].Day, menus[menus.Count - 1].Day);
      var text1 = new TextBlock
      {
        FontSize = 24,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      text1.Inlines.Add(new Bold(new Run(dateString)));
      FixedPage.SetLeft(text1, 130);
      FixedPage.SetTop(text1, 90);
      return text1;
    }

    private UIElement GetMenuContent()
    {
      var grid1 = new Grid { ShowGridLines = true };

      grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
      grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(520) });
      grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });

      for (int i = 0; i < menus.Count; i++)
      {
        grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

        var t1 = new TextBlock(new Run(String.Format("{0:ddd}", menus[i].Day)));
        var t2 = new TextBlock(new Run(menus[i].Menu));
        var t3 = new TextBlock(new Run(menus[i].Price.ToString()));
        var textBlocks = new TextBlock[] { t1, t2, t3 };

        for (int column = 0; column < textBlocks.Length; column++)
        {
          textBlocks[column].VerticalAlignment = VerticalAlignment.Center;
          textBlocks[column].Margin = new Thickness(5, 2, 5, 2);
          Grid.SetColumn(textBlocks[column], column);
          Grid.SetRow(textBlocks[column], i);
          grid1.Children.Add(textBlocks[column]);
        }
      }

      FixedPage.SetLeft(grid1, 100);
      FixedPage.SetTop(grid1, 140);
      return grid1;
    }


    private void OnClose(object sender, RoutedEventArgs e)
    {
      NavigationService.GoBack();
    }

    private void OnPrint(object sender, RoutedEventArgs e)
    {
      //           viewer.Print();

      var dlg = new PrintDialog();
      if (dlg.ShowDialog() == true)
      {
        dlg.PrintDocument(fixedDocument.DocumentPaginator, "Menu Plan");
      }

    }

    private void OnCreateXPS(object sender, RoutedEventArgs e)
    {
      var c = new GregorianCalendar();
      int weekNumber = c.GetWeekOfYear(menus[0].Day, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
      string fileName = String.Format("menuplan{0}", weekNumber);

      var dlg = new SaveFileDialog
      {
        FileName = fileName,
        DefaultExt = "xps",
        Filter = "XPS Documents|*.xps|All Files|*.*",
        AddExtension = true
      };

      if (dlg.ShowDialog() == true)
      {
        var doc = new XpsDocument(dlg.FileName, FileAccess.Write, CompressionOption.Fast);
        XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
        writer.WritingCompleted += delegate
        {
          doc.Close();
        };
        writer.WriteAsync(fixedDocument);

        //writer.Write(fixedDocument);

        //doc.Close();


      }
    }

  }
}
