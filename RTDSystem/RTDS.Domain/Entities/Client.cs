namespace RTDS.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Packages = new HashSet<Package>();
            Packages1 = new HashSet<Package>();
        }

        public int ClientID { get; set; }

       
        [StringLength(32)]
        [Required(ErrorMessage = "¬вед≥ть ≥м'€")]
        public string First_Name { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "¬вед≥ть пр≥звище")]
        public string Last_Name { get; set; }

       
        [StringLength(16)]
        [Required(ErrorMessage = "¬вед≥ть номер телефону")]
        public string Phone_Number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Package> Packages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Package> Packages1 { get; set; }
    }
}
