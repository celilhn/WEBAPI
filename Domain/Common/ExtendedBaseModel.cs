using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public class ExtendedBaseModel
    {
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime"), DefaultValue("getdate()")]
        public DateTime InsertionDate { get; set; }
        [Column(TypeName = "datetime"), DefaultValue("getdate()")]
        public DateTime UpdateDate { get; set; }
        [Column(TypeName = "tinyint"), DefaultValue(1)]
        public short Status { get; set; }
    }
}
