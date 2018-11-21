namespace Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    // Thanks to: https://stackoverflow.com/questions/1406046/data-annotation-ranges-of-dates
    public class DateRangeBetweenYear2000AndNowAttribute : RangeAttribute
    {
        public DateRangeBetweenYear2000AndNowAttribute()
            : base(typeof(DateTime), new DateTime(2000, 1, 1).ToShortDateString(), DateTime.Now.ToShortDateString()) { }
    }
}
