namespace ExpenseModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TrackedChange
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime OccurredAt   { get; set; }

        [Required, MaxLength(255), DataType(DataType.Text), DefaultValue("System")]
        public string ChangedBy   { get; set; }

        public ChangeStatus ChangedTo { get; set; }

        public TrackedChange(DateTime occurredAt, string changedBy, ChangeTrackingType changedTo)
        {
            this.OccurredAt = occurredAt;
            this.ChangedBy = changedBy;
            this.ChangedTo = new ChangeStatus(changedTo);
        }
    }
}