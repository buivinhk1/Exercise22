using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.DTO
{
    public class AttributesNameDTO
    {
        public int Id { get; set; }
        public int? TreeNodeId { get; set; }
        [Required]
        public string? AttributesName { get; set; }
        //public List<AttributesNameDTO>? Children { get; set; }
    }
}
