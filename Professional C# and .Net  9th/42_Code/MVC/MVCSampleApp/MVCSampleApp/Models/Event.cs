﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSampleApp.Models
{
  public class Event
  {
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime Day { get; set; }
  }
}