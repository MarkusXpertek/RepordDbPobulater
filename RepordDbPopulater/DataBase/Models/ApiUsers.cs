using System;
using System.Collections.Generic;
using System.Text;

namespace RepordDbPopulater.DataBase
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ApiUsers", Schema = "Admin")]
    public class ApiUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        public Guid ApiKey { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string BranchId { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 1)]
        public string ReasonCode { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string EnquirerCode { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Password { get; set; }


    }
}
