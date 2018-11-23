namespace Validation
{
    using System;
    using System.ComponentModel;

    public class DefaultDateTimeNowAttribute : DefaultValueAttribute
    {
        public DefaultDateTimeNowAttribute()
            : base(DateTime.Now) { }
    }
}
