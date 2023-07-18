using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CreateOrEditApplicationDTO
    {
        public int ApplicationKey { get; set; }
        public string? ApplicationName { get; set; }
        public string? Owner { get; set; }
    }
}
