using System;
using System.Collections.Generic;

namespace DAL.Repositories.Entities
{
    public partial class Application
    {
        public Application()
        {
            TreeNodes = new HashSet<TreeNode>();
        }

        public int ApplicationKey { get; set; }
        public string? ApplicationName { get; set; }
        public string? Owner { get; set; }

        public virtual ICollection<TreeNode> TreeNodes { get; set; }
    }
}
