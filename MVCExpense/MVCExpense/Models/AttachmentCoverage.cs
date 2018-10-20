namespace MVCExpense
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttachmentCoverage")]
    public partial class AttachmentCoverage
    {
        public int Id { get; set; }

        public int AttachmentId { get; set; }

        public int? StatementCovered { get; set; }

        public int? TransactionCovered { get; set; }

        public virtual Attachment Attachment { get; set; }

        public virtual Statement Statement { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
