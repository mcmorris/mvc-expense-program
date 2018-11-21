namespace ExpenseModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("AuditAll")]
    public class AuditAll
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserId { get; set; }

        [Required]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [Required]
        [MaxLength(255)]
        public string Table { get; set; }

        [Required]
        public Xml ChangeMade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuditAll()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuditAll(int id, string userId, byte[] timeStamp, string table, Xml changeMade)
        {
            this.Id = id;
            this.UserId = userId;
            this.TimeStamp = timeStamp;
            this.Table = table;
            this.ChangeMade = changeMade;
        }
    }
}