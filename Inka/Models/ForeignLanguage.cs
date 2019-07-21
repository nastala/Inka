namespace Inka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ForeignLanguage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ForeignLanguage()
        {
            UserForeignLanguages = new HashSet<UserForeignLanguage>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name= "Dil")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserForeignLanguage> UserForeignLanguages { get; set; }
    }
}
