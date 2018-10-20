namespace MVCExpense
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AuditAll")]
    public partial class AuditAll
    {
        public int Id { get; set; }

        public DateTime OccurredAt { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string TableName { get; set; }

        [Column(TypeName = "xml")]
        public string AuditEntry { get; set; }
    }
}
