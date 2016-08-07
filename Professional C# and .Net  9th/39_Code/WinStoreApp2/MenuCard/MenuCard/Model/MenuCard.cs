﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Wrox.ProCSharp.Extensions;

namespace Wrox.ProCSharp.Model
{
  public class MenuCard : BindableBase
  {
    private string title;
    public string Title { get { return title; } set { SetProperty(ref title, value); } }

    private string description;
    public string Description { get { return description; } set { SetProperty(ref description, value); } }

    private ImageSource image;
    public ImageSource Image { get { return image; } set { SetProperty(ref image, value); } }
    public string ImagePath { get; set; }


    private readonly ICollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();
    public ICollection<MenuItem> MenuItems
    {
      get { return menuItems; }
    }

    public void RestoreReferences()
    {
      foreach (var menuItem in MenuItems)
      {
        menuItem.MenuCard = this;
      }
    }


  }
}
