namespace ExpenseModel
{
    using System;

    public class File : TrackedEntity
    {
        public int    Id           { get; set; }
        public User   Uploader     { get; set; }
        public string FileName     { get; set; }
        public string FilePath     { get; set; }
        public string ContentType  { get; set; }
        public string Description  { get; set; }
        public int    FileSize     { get; set; }

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
            this.FileName = fileName;
            this.FilePath = filePath;
            this.ContentType = contentType;
            this.Description = description;
            this.FileSize = fileSize;
        }
    }
}