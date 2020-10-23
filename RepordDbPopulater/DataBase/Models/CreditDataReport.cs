namespace RepordDbPopulater.DataBase
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Report", Schema = "Admin")]
    public class CreditDataReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        public Guid ApiUserId { get; set; }

    
        [StringLength(50, MinimumLength = 0)]
        public string SysUserId { get; set; }


        [StringLength(50, MinimumLength = 0)]
        public string ActorCode { get; set; }


        [StringLength(13, MinimumLength = 0)]
        public string ConsumerIdentifier { get; set; }


        [StringLength(50, MinimumLength = 0)]
        public string ConsumerFirstname { get; set; }

        [Required]
        public string ReportName { get; set; }

        [Required]
        public byte[] PdfReport { get; set; }

        [Required]
        public byte[] XmlReport { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}