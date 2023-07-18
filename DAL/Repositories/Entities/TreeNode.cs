using System;
using System.Collections.Generic;

namespace DAL.Repositories.Entities
{
    public partial class TreeNode
    {
        public TreeNode()
        {
            AttributesNames = new HashSet<AttributesName>();
            InverseParent = new HashSet<TreeNode>();
        }

        public int Id { get; set; }
        public string? Desc { get; set; }
        public int? ParentId { get; set; }
        public string? NodeType { get; set; }
        public string? DateSave { get;set; }
        public string? Owner { get;set; }
        public int? ApplicationKey { get; set; }
        public virtual Application? ApplicationKeyNavigation { get; set; }

        public virtual TreeNode? Parent { get; set; }
        public virtual ICollection<AttributesName> AttributesNames { get; set; }
        public virtual ICollection<TreeNode> InverseParent { get; set; }
    }
}
