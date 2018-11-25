namespace ExpenseModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ChangeStatus")]
    public class ChangeStatus : SelfValidator
    {
        [Key][Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required][MaxLength(255)][DataType(DataType.Text)][Index("IDX_ChangeStatusName")]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChangeStatus()
        {

        }

        // Included for JSON/XML/Serialization.  Do not use for initialization.
        public ChangeStatus(int id, string name, bool active)
        {
            this.Id   = id;
            this.Name = name;
            this.Active = active;
        }

        // Use strong-typed constructor for initialization.
        public ChangeStatus(ChangeTrackingType type, bool active=true)
        {
            this.Id   = (int)type;
            this.Name = type.ToString();
            this.Active = active;
        }
    }
}