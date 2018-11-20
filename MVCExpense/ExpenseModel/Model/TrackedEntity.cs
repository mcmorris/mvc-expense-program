namespace ExpenseModel
{
    using System;

    public abstract class TrackedEntity
    {
        public DateTime Created   { get; set; }
        public string CreatedBy   { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy  { get; set; }
        public DateTime? InactiveSince { get; set; }
        public bool Active        { get; set; }

        public TrackedEntity()
        {
            this.Created = DateTime.Now;
            this.CreatedBy = "System";
            this.Modified = null;
            this.ModifiedBy = null;
            this.InactiveSince = null;
            this.Active = true;
        }

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