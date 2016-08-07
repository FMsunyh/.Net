﻿using System;

namespace WhatsNewAttributes
{
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Method,
        AllowMultiple = true, Inherited = false)]
    public class LastModifiedAttribute : Attribute
    {
        private readonly DateTime _dateModified;
        private readonly string _changes;

        public LastModifiedAttribute(string dateModified, string changes)
        {
            _dateModified = DateTime.Parse(dateModified);
            _changes = changes;
        }

        public DateTime DateModified
        {
            get { return _dateModified; }
        }

        public string Changes
        {
            get { return _changes; }
        }

        public string Issues { get; set; }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    public class SupportsWhatsNewAttribute : Attribute
    {
    }
}