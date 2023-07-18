using DAL.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.DTO
{
    public class TreeNodesDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Desc { get; set; }
        public int? ParentId { get; set; }
        public string? NodeType { get; set; }
        public string? DateSave { get; set; }
        [Required]
        public string? Owner { get; set; }

        public int? ApplicationKey { get; set; }
        public List<TreeNodesDTO>? Children { get; set; }

        //public virtual ICollection<TreeNodesDTO> TreeNodesDTOs1 { get; set; }
       // public virtual ICollection<AttributesName> AttributesNames { get; set; }
       
    }
}
