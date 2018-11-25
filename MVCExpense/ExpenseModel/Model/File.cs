namespace ExpenseModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("File")]
    public class File : TrackedSelfValidatorEntity
    {
        [Key][Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int    Id           { get; set; }

        [MaxLength(255)][Index("IDX_FileUserId")]
        public string UserId       { get; set; }

        [Required][MaxLength(255)][DataType(DataType.Text)]
        public string FileName     { get; set; }

        [Required][DataType(DataType.Url)]
        public string FilePath     { get; set; }

        [MaxLength(255)]
        public string ContentType  { get; set; }

        [DataType(DataType.Text)]
        public string Description  { get; set; }

        public int    FileSize     { get; set; }

        [MaxLength(255)]
        [ForeignKey("UserId")]
        public virtual User Uploader
        {
            get => this.Uploader;
            set
            {
                this.Uploader = value;
                this.UserId = value?.Id;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public File()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public File(User uploader, string fileName, string filePath, string contentType, string description, int fileSize)
        {
            this.Uploader    = uploader;
            this.FileName    = fileName;
            this.FilePath    = filePath;
            this.ContentType = contentType;
            this.Description = description;
            this.FileSize    = fileSize;
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
            ICollection<TrackedChange> changes,
            bool      active)
            : base(changes, active)
        {
            this.Id = id;
            this.Uploader = uploader;
            this.FileName = fileName;
            this.FilePath = filePath;
            this.ContentType = contentType;
            this.Description = description;
            this.FileSize = fileSize;
        }
    }
}