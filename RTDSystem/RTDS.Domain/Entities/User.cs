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
        [Required(ErrorMessage = "������ ��'� ��� ����� ")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "������ ������")]
        public string PasswordHash { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "������ ��'� ")]
        public string First_Name { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "������ �������")]
        public string Last_Name { get; set; }

        [StringLength(16)]
        [Required(ErrorMessage = "������ ����� ��������")]
        public string Phone_Number { get; set; }

        public int RoleID { get; set; }

        public int BranchID { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Role Role { get; set; }
    }
}
