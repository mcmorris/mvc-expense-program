namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("User")]
    public class User : TrackedSelfValidatorEntity
    {
        [Key][Required][MaxLength(255)]
        public string Id             { get; set; }

        [MaxLength(255)]
        public string ManagerId      { get; set; }

        [Required][MaxLength(255)][DataType(DataType.Text)]
        public string FullName       { get; set; }

        [MaxLength(255)][DataType(DataType.Text)]
        public string DepartmentName { get; set; }

        [MaxLength(255)][DataType(DataType.Text)]
        public string JobTitle       { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Account> Accounts { get; set; }

        #region Calculated fields
        [NotMapped]
        public decimal DebitSum => this.Accounts.Sum(a => a.DebitSum);

        [NotMapped]
        public decimal CreditSum => this.Accounts.Sum(a => a.CreditSum);

        [NotMapped]
        public decimal Balance => this.DebitSum - this.CreditSum;
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Accounts = new HashSet<Account>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User(string id, string managerId, string fullName, string department, string job)
        {
            this.Id             = id;
            this.ManagerId      = managerId;
            this.FullName       = fullName;
            this.DepartmentName = department;
            this.JobTitle       = job;
            this.Accounts       = new HashSet<Account>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User(
            string               id,
            string               managerId,
            string               fullName,
            string               department,
            string               job,
            ICollection<Account> accounts,
            ICollection<TrackedChange> changes,
            bool                       active)
            : base(changes, active)
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