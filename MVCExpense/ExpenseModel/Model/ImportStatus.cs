namespace ExpenseModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ImportStatus")]
    public class ImportStatus
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Text)]
        [Index("IDX_ImportStatusName")]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImportStatus()
        {

        }

        // Included for JSON/XML/Serialization.  Do not use for initialization.
        public ImportStatus(int id, string name, bool active)
        {
            this.Id   = id;
            this.Name = name;
            this.Active = active;
        }

        // Use strong-typed constructor for initialization.
        public ImportStatus(StatusTypes type, bool active=true)
        {
            this.Id   = (int)type;
            this.Name = type.ToString();
            this.Active = active;
        }
    }
}