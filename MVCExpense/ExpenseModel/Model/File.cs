namespace ExpenseModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("File")]
    public class File : TrackedEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int    Id           { get; set; }

        [MaxLength(255)]
        [Index("IDX_FileUserId")]
        public string UserId       { get; set; }

        [MaxLength(255)]
        [ForeignKey("UserId")]
        public virtual User Uploader     { get; set; }

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Text)]
        public string FileName     { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string FilePath     { get; set; }

        [MaxLength(255)]
        public string ContentType  { get; set; }

        [DataType(DataType.Text)]
        public string Description  { get; set; }

        public int    FileSize     { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public File()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public File(
            int       id,
            User      uploader,
            string    fileName,
            string    filePath,
            string    contentType,
            string    description,
            int       fileSize,
            DateTime  created,
            string    createdBy,
            DateTime? modified,
            string    modifiedBy,
            DateTime? inactiveSince,
            bool      active)
            : base(created, createdBy, modified, modifiedBy, inactiveSince, active)
        {
            this.Id = id;
            this.Uploader = uploader;
            this.UserId = this.Uploader.Id;
            this.FileName = fileName;
            this.FilePath = filePath;
            this.ContentType = contentType;
            this.Description = description;
            this.FileSize = fileSize;
        }
    }
}