﻿namespace Inka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public enum Gender
    {
        Erkek, Kadın
    }

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserForeignLanguages = new HashSet<UserForeignLanguage>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Kimlik No boş bırakılamaz")]
        [StringLength(11)]
        [Display(Name = "Kimlik No")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "Ad boş bırakılamaz")]
        [StringLength(100)]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad boş bırakılamaz")]
        [StringLength(50)]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Doğum Tarihi boş bırakılamaz")]
        [Column(TypeName = "date")]
        [Display(Name = "Doğum Tarihi")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Cinsiyet boş bırakılamaz")]
        [StringLength(10)]
        [Display(Name = "Cinsiyet")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Telefon No boş bırakılamaz")]
        [StringLength(10)]
        [Display(Name = "Telefon No")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Geçersiz telefon numarası")]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "E-posta boş bırakılamaz")]
        [StringLength(100)]
        [Display(Name = "E-posta")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi")]
        public string Email { get; set; }

        [Display(Name = "Boy(cm)")]
        public byte Height { get; set; }

        [Display(Name = "Ağırlık")]
        public byte Weight { get; set; }

        [Display(Name = "Beden")]
        public byte Size { get; set; }

        [Display(Name = "Ayakkabı No")]
        public byte ShoeSize { get; set; }

        public string PhotoPath { get; set; }

        [Display(Name = "Uyruk")]
        public int NationID { get; set; }

        [Display(Name = "İl")]
        public int CityID { get; set; }

        [Display(Name = "İlçe")]
        public int DistrictID { get; set; }

        [Display(Name = "Ehliyet")]
        public int LicenceID { get; set; }

        [Display(Name = "Eğitim Düzeyi")]
        public int EducationLevelID { get; set; }

        [Display(Name = "Üniversite")]
        public int? UniversityID { get; set; }

        [Display(Name = "Bölüm")]
        public int? UniversityDepartmentID { get; set; }

        public virtual City City { get; set; }

        public virtual District District { get; set; }

        public virtual EducationLevel EducationLevel { get; set; }

        public virtual Licence Licence { get; set; }

        public virtual Nation Nation { get; set; }

        public virtual University University { get; set; }

        public virtual UniversityDepartment UniversityDepartment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserForeignLanguage> UserForeignLanguages { get; set; }
    }
}
