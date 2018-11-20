namespace ExpenseModel
{
    using System;

    public class ISO4217Currency : TrackedEntity
    {
        public string        Id             { get; set; }
        public int           Exponent       { get; set; }
        public string        Name           { get; set; }
        public DateTime?     WithdrawalDate { get; set; }

        public ISO4217Currency(
            string    alphabeticCode,
            int       exponent,
            string    name,
            DateTime? withdrawalDate,
            DateTime  created,
            string    createdBy,
            DateTime? modified,
            string    modifiedBy,
            DateTime? inactiveSince,
            bool      active)
            : base(created, createdBy, modified, modifiedBy, inactiveSince, active)
        {
            this.Id = alphabeticCode;
            this.Exponent = exponent;
            this.Name = name;
            this.WithdrawalDate = withdrawalDate;
        }
    }
}
