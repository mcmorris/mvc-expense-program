namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class User : TrackedEntity
    {
        public string Id             { get; set; }
        public string ManagerId      { get; set; }
        public string FullName       { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle       { get; set; }

        // Calculated fields
        public decimal DebitSum => this.Accounts.Sum(a => a.DebitSum);

        public decimal CreditSum => this.Accounts.Sum(a => a.CreditSum);

        public decimal Balance => this.DebitSum - this.CreditSum;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Account> Accounts { get; set; }

        public User(
            string               id,
            string               managerId,
            string               fullName,
            string               department,
            string               job,
            ICollection<Account> accounts,
            DateTime             created,
            string               createdBy,
            DateTime?            modified,
            string               modifiedBy,
            DateTime?            inactiveSince,
            bool                 active)
            : base(created, createdBy, modified, modifiedBy, inactiveSince, active)
        {
            this.Id = id;
            this.ManagerId = managerId;
            this.FullName = fullName;
            this.DepartmentName = department;
            this.JobTitle = job;
            this.Accounts = accounts;
        }
    }
}