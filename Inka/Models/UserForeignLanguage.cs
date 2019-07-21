namespace Inka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserForeignLanguage
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ForeignLanguageID { get; set; }

        [Required]
        [StringLength(10)]
        public string Reading { get; set; }

        [Required]
        [StringLength(10)]
        public string Writing { get; set; }

        [Required]
        [StringLength(10)]
        public string Speaking { get; set; }

        public virtual ForeignLanguage ForeignLanguage { get; set; }

        public virtual User User { get; set; }
    }
}
