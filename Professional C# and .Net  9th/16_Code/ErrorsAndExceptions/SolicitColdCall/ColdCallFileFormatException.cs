﻿using System;

namespace Wrox.ProCSharp.ErrorsAndExceptions
{
  class ColdCallFileFormatException : Exception
  {
    public ColdCallFileFormatException(string message)
      : base(message)
    {
    }

    public ColdCallFileFormatException(
       string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }

}
