﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wrox.ProCSharp.Extensions
{
  public class BindableBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    protected virtual bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
    {
      if (!EqualityComparer<T>.Default.Equals(property, value))
      {
        property = value;
        IsDirty = true;
        OnPropertyChanged(propertyName);
        return true;
      }
      return false;
    }

    public bool IsDirty { get; private set; }

    public void ClearDirty()
    {
      IsDirty = false;
    }
  }
}
