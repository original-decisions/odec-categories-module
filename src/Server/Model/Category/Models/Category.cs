using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using odec.Framework.Generic;

namespace odec.Server.Model.Category
{
    public class Category : Glossary<int>
    {
        //TODO: ubcomment as soon as EF 7 supports Index attr 
        [StringLength(255)]// [Index("ix_Categories_Name",Order = 0), StringLength(255)]
        public override string Name { get; set; }
        // [Index("ix_Categories_Name", Order = 1),Index("ix_Categories_IsApproved")]
        public bool IsApproved { get; set; }
    }
}