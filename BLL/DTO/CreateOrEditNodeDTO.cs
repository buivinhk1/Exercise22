using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CreateOrEditNodeDTO
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
    }
}
