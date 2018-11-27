namespace ExpenseModel
{
    using System;

    public class CurrencyFactory
    {
        public ISO4217Currency ISO4217Currency(string code, int exponent, string name, DateTime? withdrawalDate)
        {
            return new ISO4217Currency(code, exponent, name, withdrawalDate);
        }

        public ISO4217Currency ADP => this.ISO4217Currency("ADP", 0, "Andorran Peseta", DateTime.Parse("2003-07-01 12:00:00"));
        public ISO4217Currency AED => this.ISO4217Currency("AED", 2, "United Arab Emirates Dirham", null);
        public ISO4217Currency AFA => this.ISO4217Currency("AFA", 0, "Afghan Afghani", DateTime.Parse("2003-01-01 12:00:00"));
        public ISO4217Currency AFN => this.ISO4217Currency("AFN", 2, "Afghan Afghani", null);
        public ISO4217Currency ALK => this.ISO4217Currency("ALK", 0, "Afghan Old Lek", DateTime.Parse("1989-12-01 12:00:00"));
        public ISO4217Currency ALL => this.ISO4217Currency("ALL", 2, "Albanian Lek", null);
        public ISO4217Currency CAD => this.ISO4217Currency("CAD", 2, "Canadian Dollar", null);
        public ISO4217Currency CHF => this.ISO4217Currency("CHF", 2, "Swiss Franc", null);
        public ISO4217Currency EUR => this.ISO4217Currency("EUR", 2, "Euro", null);
        public ISO4217Currency FIM => this.ISO4217Currency("FIM", 0, "Ãland Islands Markka", DateTime.Parse("2002-03-01 12:00:00"));
        public ISO4217Currency GBP => this.ISO4217Currency("GBP", 2, "Pound Sterling", null);
        public ISO4217Currency ILS => this.ISO4217Currency("ILS", 2, "Israeli New Shekel", null);
        public ISO4217Currency IMP => this.ISO4217Currency("IMP", 2, "Isle of Man pound (also Manx pound)", null);
        public ISO4217Currency INR => this.ISO4217Currency("INR", 2, "Indian Rupee", null);
        public ISO4217Currency TVD => this.ISO4217Currency("TVD", 2, "Tuvalu Dollar", null);
        public ISO4217Currency TWD => this.ISO4217Currency("TWD", 2, "New Taiwan Dollar", null);
        public ISO4217Currency USD => this.ISO4217Currency("USD", 2, "United States Dollar", null);
        public ISO4217Currency VND => this.ISO4217Currency("VND", 0, "Vietnamese Dồng", null);
        public ISO4217Currency XBT => this.ISO4217Currency("XBT", 8, "Bitcoin", null);
        public ISO4217Currency XXX => this.ISO4217Currency("XXX", 0, "No ISO4217Currency", null);
        public ISO4217Currency YER => this.ISO4217Currency("YER", 2, "Yemeni Rial", null);
        public ISO4217Currency ZAR => this.ISO4217Currency("ZAR", 2, "South African Rand", null);
    }
}