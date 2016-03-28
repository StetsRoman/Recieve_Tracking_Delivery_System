namespace RTDS.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PackagID { get; set; }

        [Required]
        [StringLength(5)]
        [Range(0, int.MaxValue, ErrorMessage = "������ ��������� �����")]
        public string Package_Number { get; set; }
       
        [Required(ErrorMessage = "������ ���� �������")]
        public double Delivery_Price { get; set; }

        [Required(ErrorMessage = "������ ����")]
        [Range(0, int.MaxValue, ErrorMessage = "������ ��������� �����")]
        public double Weith { get; set; }

       
        [StringLength(30)]
        [Required(ErrorMessage = "������ �����")]
        [Range(0, int.MaxValue, ErrorMessage = "������ ��������� �����")]
        public string Size { get; set; }

        [Required(ErrorMessage = "������ ���� � ������ dd/MM/yyyy")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime Send_Date { get; set; }

        public int StatusID { get; set; }

        public int SenderID { get; set; }

        public int ReceiverID { get; set; }

        public int CurrentLocation { get; set; }

        public int SourceLocation { get; set; }

        public int DestinationLocation { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Branch Branch1 { get; set; }

        public virtual Branch Branch2 { get; set; }

        public virtual Client Client { get; set; }

        public virtual Client Client1 { get; set; }

        public virtual Package_Statuses Package_Statuses { get; set; }
    }
}
