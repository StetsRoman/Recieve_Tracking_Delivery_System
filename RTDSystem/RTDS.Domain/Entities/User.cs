namespace RTDS.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }


        [StringLength(20)]
        [Required(ErrorMessage = "¬вед≥ть ≥м'€ дл€ входу ")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "¬вед≥ть пароль")]
        public string PasswordHash { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "¬вед≥ть ≥м'€ ")]
        public string First_Name { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "¬вед≥ть пр≥звище")]
        public string Last_Name { get; set; }

        [StringLength(16)]
        [Required(ErrorMessage = "¬вед≥ть номер телефону")]
        public string Phone_Number { get; set; }

        public int RoleID { get; set; }

        public int BranchID { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Role Role { get; set; }
    }
}
