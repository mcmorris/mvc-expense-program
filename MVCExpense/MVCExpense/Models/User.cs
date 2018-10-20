namespace MVCExpense
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Accounts = new HashSet<Account>();
            this.BankImports = new HashSet<BankImport>();
            this.Files = new HashSet<File>();
        }

        [StringLength(255)]
        public string Id { get; set; }

        [StringLength(255)]
        public string ManagerId { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string DepartmentName { get; set; }

        [StringLength(255)]
        public string JobTitle { get; set; }

        public bool Active { get; set; }

        public DateTime FirstCreated { get; set; }

        public DateTime LastModified { get; set; }

        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(255)]
        public string LastModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankImport> BankImports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<File> Files { get; set; }
    }
}
