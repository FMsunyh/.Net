﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Wrox.ProCSharp.Documents
{

  public partial class MenuPlannerPage : Page
  {
    private ObservableCollection<MenuEntry> menus = new ObservableCollection<MenuEntry>();
    public MenuPlannerPage()
    {
      InitializeComponent();

      this.daySelection.BlackoutDates.AddDatesInPast();


      this.DataContext = menus;
    }


    private void FillMenuList(DateTime startDay)
    {
      menus.Clear();
      MenuConfiguration config = Properties.Settings.Default.MenuConfigDays;
      if (config == null) // default fill
      {
        menus.Add(new MenuEntry { Day = startDay, Price = 6.70m });
        menus.Add(new MenuEntry { Day = startDay + TimeSpan.FromDays(1), Price = 6.70m });
        menus.Add(new MenuEntry { Day = startDay + TimeSpan.FromDays(2), Price = 6.70m });
        menus.Add(new MenuEntry { Day = startDay + TimeSpan.FromDays(3), Price = 6.70m });
        menus.Add(new MenuEntry { Day = startDay + TimeSpan.FromDays(4), Price = 7.30m });
        menus.Add(new MenuEntry { Day = startDay + TimeSpan.FromDays(5), Price = 6.70m });
      }
      else
      {
        foreach (var c in config.MenuConfig)
        {
          menus.Add(new MenuEntry
          {
            Day = startDay + TimeSpan.FromDays((int)c.DayOfWeek - 1),
            Price = c.Price
          });
        }
      }
    }

    private void OnDateSelection(object sender, SelectionChangedEventArgs e)
    {
      DateTime date = daySelection.SelectedDate ?? DateTime.Today;
      if (date.DayOfWeek != DayOfWeek.Monday) // start the menu with Monday
      {
        date = date + TimeSpan.FromDays(-((int)date.DayOfWeek - 1));
        daySelection.SelectedDate = date; // fire event once more
        return;
      }

      FillMenuList(date);
    }

    private void OnRemoveDay(object sender, RoutedEventArgs e)
    {
      this.menus.Remove(this.gridMenus.SelectedItem as MenuEntry);
    }

    private void OnAddDay(object sender, RoutedEventArgs e)
    {
      MenuEntry selectedMenu = this.gridMenus.SelectedItem as MenuEntry;

      var mi = new MenuEntry
      {
        Day = selectedMenu.Day,
        Price = selectedMenu.Price
      };
      menus.Insert(menus.IndexOf(selectedMenu), mi);

    }

    private void OnCreateDoc(object sender, RoutedEventArgs e)
    {
      if (menus.Count == 0)
      {
        MessageBox.Show("Select a date first", "Menu Planner", MessageBoxButton.OK);
        return;
      }
      var page2 = new DocumentPage();
      NavigationService.LoadCompleted += page2.NavigationService_LoadCompleted;
      NavigationService.Navigate(page2, menus);
      // NavigationService.Navigate(new Uri("pack://application:,,,/DocumentPage.xaml"), menus);
    }

    private void OnSaveConfig(object sender, RoutedEventArgs e)
    {
      var config = new MenuConfiguration();
      var configMenus = from m in menus
                        select new MenuConfigDay
                        {
                          DayOfWeek = m.Day.DayOfWeek,
                          Price = m.Price
                        };
      config.MenuConfig.AddRange(configMenus);
      Properties.Settings.Default.MenuConfigDays = config;
      Properties.Settings.Default.Save();
    }
  }
}
