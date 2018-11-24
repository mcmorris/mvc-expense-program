namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public abstract class TrackedSelfValidatorEntity : SelfValidator
    {
        public virtual ICollection<TrackedChange> Changes { get; set; }

        [Required]
        public bool Active { get; set; }

        #region Calculated fields
        [NotMapped]
        public TrackedChange Created => this.Changes.FirstOrDefault(c => c.ChangedTo.Id == (int)ChangeTrackingType.Created);

        [NotMapped]
        public TrackedChange Modified => this.Changes.FirstOrDefault(c => c.ChangedTo.Id == (int)ChangeTrackingType.Modified);
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrackedSelfValidatorEntity()
        {
            this.Changes = new HashSet<TrackedChange>();
            this.Active = true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrackedSelfValidatorEntity(
            ICollection<TrackedChange> changes,
            bool active)
        {
            this.Changes = changes;
            this.Active = active;
        }

        public void TrackChange(DateTime occurredAt, string changedBy, ChangeTrackingType changedTo)
        {
            this.Changes.Append(new TrackedChange(occurredAt, changedBy, changedTo));
        }
    }
}