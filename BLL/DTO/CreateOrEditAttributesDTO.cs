using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CreateOrEditAttributesDTO
    {
        public int Id { get; set; }
        public int? TreeNodeId { get; set; }
        [Required]
        public string? AttributesName { get; set; }
    }
}
