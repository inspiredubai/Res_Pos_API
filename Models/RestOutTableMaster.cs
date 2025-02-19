
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspirePO.Models
{
    [Table("Rest_OutTableMaster")] // Map to the exact table name in the database
    public class RestOutTableMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // No auto-increment for ID
        public long ID { get; set; }

        public int? OutSElName { get; set; }

        [StringLength(50)]
        public string? TblName { get; set; }

        public long? TblCount { get; set; }

        [StringLength(20)]
        public string? Status { get; set; }

        public int? Addtable { get; set; }

        public long? ForColor { get; set; }

        public long? BackColor { get; set; }

        public int? CategoryID { get; set; }

        public int? OutLetID { get; set; }

        public int? OutletSectID { get; set; }

        public int? AddedTable { get; set; }

        public int? ManualCode { get; set; }
    }
}
