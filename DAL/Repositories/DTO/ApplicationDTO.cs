using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.DTO
{
    public class ApplicationDTO
    {

        public int ApplicationKey { get; set; }
        [Required]
        public string? ApplicationName { get; set; }
        [Required]
        public string? Owner { get; set; }
    }
}
