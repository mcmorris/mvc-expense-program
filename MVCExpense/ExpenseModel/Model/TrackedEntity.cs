namespace ExpenseModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using global::Validation;

    public abstract class TrackedEntity
    {
        [Required]
        [DataType(DataType.DateTime)]
        [DateRangeBetweenYear2000AndNow]
        public DateTime Created   { get; set; }

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Text)]
        public string CreatedBy   { get; set; }

        [DataType(DataType.DateTime)]
        [DateRangeBetweenYear2000AndNow]
        public DateTime? Modified { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Text)]
        public string ModifiedBy  { get; set; }

        [DataType(DataType.DateTime)]
        [DateRangeBetweenYear2000AndNow]
        public DateTime? InactiveSince { get; set; }

        [Required]
        public bool Active        { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrackedEntity()
        {
            this.Created = DateTime.Now;
            this.CreatedBy = "System";
            this.Modified = null;
            this.ModifiedBy = null;
            this.InactiveSince = null;
            this.Active = true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrackedEntity(
            DateTime  created,
            string    createdBy,
            DateTime? modified,
            string    modifiedBy,
            DateTime? inactiveSince,
            bool      active)
        {
            this.Created = created;
            this.CreatedBy = createdBy;
            this.Modified = modified;
            this.ModifiedBy = modifiedBy;
            this.InactiveSince = inactiveSince;
            this.Active = active;
        }
    }
}