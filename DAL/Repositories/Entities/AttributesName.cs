using System;
using System.Collections.Generic;

namespace DAL.Repositories.Entities
{
    public partial class AttributesName
    {


        public int Id { get; set; }
        public string? AttributesName1 { get; set; }
        public int? TreeNodeId { get; set; }

        public virtual TreeNode? TreeNode { get; set; }
    }
}
