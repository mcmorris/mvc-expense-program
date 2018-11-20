namespace ExpenseModel.Validation
{
    using global::Validation.Properties;

    using DateTime = System.DateTime;

    public static class DateTimeStringValidationExtension
    {
        private static readonly DateTime _minimumDateTime = new DateTime(1990, 1, 1);

        public static string IsValidOldDateTime(this string potentialDateTime)
        {
            if (DateTime.TryParse(potentialDateTime, out var result) == false) { return Resources.InvalidDateFormat; }
            if (result.Ticks <= DateTimeStringValidationExtension._minimumDateTime.Ticks) { return Resources.InvalidDateBelowMinimum; }
            if (result.Ticks > DateTime.Now.Ticks) { return Resources.InvalidDateAboveMaximum; }
            return null;
        }
    }
}
