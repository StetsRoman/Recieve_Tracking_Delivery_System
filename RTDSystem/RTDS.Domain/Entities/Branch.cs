namespace RTDS.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Branch()
        {
            Packages = new HashSet<Package>();
            Packages1 = new HashSet<Package>();
            Packages2 = new HashSet<Package>();
            Users = new HashSet<User>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BranchID { get; set; }
    [Required(ErrorMessage="������� ����")]
        public int CityID { get; set; }

       
        [StringLength(32)]
        [Required(ErrorMessage = "������ ������")]
        public string Street { get; set; }

    
        [StringLength(32)]
        [Required(ErrorMessage = "������ ����� �������")]
        [Range(0, int.MaxValue, ErrorMessage = "������ ��������� �����")]
        public string House { get; set; }
        [Required(ErrorMessage = "������� ��� ��������")]
        public int BranchTypeID { get; set; }

        public virtual BranchType BranchType { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Package> Packages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Package> Packages1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Package> Packages2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
