namespace Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateAfter200011Attribute : ValidationAttribute
    {
        private readonly long _minValue;

        public DateAfter200011Attribute()
        {
            this._minValue = new DateTime(2000, 1, 1).Ticks;
        }

        public override bool IsValid(object value)
        {
            return ((DateTime)value).Ticks >= this._minValue;
        }
    }
}
