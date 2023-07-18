using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL.Repositories;
using DAL.Repositories.DTO;

using DAL.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TreContentDAL
    {
        public TreContentDAL() { }

        public List<TreeNodesDTO> GetALL()
        {
            var db = new TreContentdbContext();
            var dbNode = db.TreeNodes.ToList();
            var nodes = dbNode.Select(x => new TreeNodesDTO()
            {
                Id = x.Id,
                Desc = x.Desc,
                ParentId =x.ParentId,
                NodeType=x.NodeType,
                DateSave=x.DateSave,
                Owner=x.Owner,
                ApplicationKey = x.ApplicationKey
            }).ToList();
            return nodes;
        }
        /// <summary>
        /// 3 time push ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TreeNodesDTO> GetNodeInfo(int id)
        {
            var db = new TreContentdbContext();
            var dbNode =  db.TreeNodes.ToList();
            var result = dbNode.Select(x => new TreeNodesDTO()
            {
                Id = x.Id,
                Desc = x.Desc,
                ParentId = x.ParentId,
                NodeType = x.NodeType,
                DateSave = x.DateSave,
                Owner = x.Owner,
                ApplicationKey=x.ApplicationKey
                
            }).ToList();
            var node = result.FirstOrDefault(x=>x.Id == id);
            return node;
        }
        /// <summary>
        /// Get information attributename when you click in treednode ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public List<AttributesNameDTO> GetAttributesNamesByTreeNodeId(int id)
        {
            using (var db = new TreContentdbContext())
            {
                var query = from t in db.TreeNodes
                            join a in db.AttributesNames on t.Id equals a.TreeNodeId
                            where t.Id == id
                            select new AttributesNameDTO
                            {
                                Id = a.Id,
                                AttributesName = a.AttributesName1,
                                TreeNodeId=a.TreeNodeId                             
                            };
                return query.ToList();
            }
        }
        /// <summary>
        /// Update Node when u click any nodeTree u can edit nodetitle and nodeType
        /// </summary>
        /// <param name="stdi"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TreeNode> UpdateNodes(TreeNode stdi)
        {
            var db = new TreContentdbContext();

            var dbNode = await db.TreeNodes.FindAsync(stdi.Id);

            if (dbNode == null)
                throw new Exception("No Node Found");

            if (String.IsNullOrEmpty(stdi.Desc))
            {
                throw new Exception("Need InforMation");
            }
            else
            {
                dbNode.Id = stdi.Id;
                dbNode.Desc = stdi.Desc;
                dbNode.NodeType = stdi.NodeType;
                dbNode.ParentId = stdi.ParentId;
                dbNode.DateSave=stdi.DateSave;
                dbNode.Owner = stdi.Owner;
                dbNode.ApplicationKey = stdi.ApplicationKey;

                await db.SaveChangesAsync();
                return dbNode;
            }
        }
        /// <summary>
        /// update Attributes Name
        /// </summary>
        /// <param name="stdi"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AttributesName> UpdateAttribute(AttributesName stdi)
        {
            var db = new TreContentdbContext();
            var dbAttribute = await db.AttributesNames.FindAsync(stdi.Id);

            if (dbAttribute == null)
                throw new Exception("No Node Found");

            if (String.IsNullOrEmpty(stdi.AttributesName1))
            {
                throw new Exception("Need InforMation");
            }
            else
            {
                dbAttribute.Id = stdi.Id;
                dbAttribute.TreeNodeId = stdi.TreeNodeId;
                dbAttribute.AttributesName1 = stdi.AttributesName1;
               
                await db.SaveChangesAsync();
                return dbAttribute;
            }
        }
        /// <summary>
        /// Create New Attributes
        /// </summary>
        /// <param name="stdi"></param>
        /// <returns></returns>
        public async Task<List<AttributesName>> CreateAttributes(AttributesName stdi)
        {
            var db = new TreContentdbContext();
           
                db.AttributesNames.Add(stdi);
                await db.SaveChangesAsync();
                return await db.AttributesNames.ToListAsync();
                  
        }
        /// <summary>
        /// Delete Attributes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AttributesName> DeleteAttributes(int id)
        {
            var db = new TreContentdbContext();
            var dbAttribute = await db.AttributesNames.FindAsync(id);
            if (dbAttribute == null)
                throw new Exception("No Attributes Found");

            db.AttributesNames.Remove(dbAttribute);
            await db.SaveChangesAsync();
            return dbAttribute;
        }
        /// <summary>
        /// Create New TreeNode
        /// </summary>
        /// <param name="stdi"></param>
        /// <returns></returns>
        public async Task<List<TreeNode>> CreateTreeNode(TreeNode stdi)
        {
            var db = new TreContentdbContext();

            db.TreeNodes.Add(stdi);
            await db.SaveChangesAsync();
            return await db.TreeNodes.ToListAsync();

        }
        /// <summary>
        /// Delete TreeNode ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TreeNode> DeleteTreeNode(int id)
        {
            var db = new TreContentdbContext();
            var dbTreeNodes = await db.TreeNodes.FindAsync(id);
            if (dbTreeNodes == null)
                throw new Exception("No Tree Nodes Found");

            db.TreeNodes.Remove(dbTreeNodes);
            await db.SaveChangesAsync();
            return dbTreeNodes;
        }
        /// <summary>
        /// Get All Application
        /// </summary>
        /// <returns></returns>
        public List<ApplicationDTO> GetALLApplication()
        {
            var db = new TreContentdbContext();
            var dbApp = db.Applications.ToList();
            var Apps = dbApp.Select(x => new ApplicationDTO()
            {
                ApplicationKey= x.ApplicationKey,
                ApplicationName= x.ApplicationName,
                Owner= x.Owner,
            }).ToList();
            return Apps;
        }
        /// <summary>
        /// Get ApplicationKey ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationDTO> GetAppInfo(int id)
        {
            var db = new TreContentdbContext();
            var dbApp = db.Applications.ToList();
            var result = dbApp.Select(x => new ApplicationDTO()
            {
                ApplicationName = x.ApplicationName,
                Owner = x.Owner,
                ApplicationKey = x.ApplicationKey

            }).ToList();
            var Apps = result.FirstOrDefault(x => x.ApplicationKey == id);                
            return Apps;
        }
        /// <summary>
        /// Create New Application
        /// </summary>
        /// <param name="stdi"></param>
        /// <returns></returns>
        public async Task<List<Application>> CreateApplication(Application stdi)
        {
            var db = new TreContentdbContext();

            db.Applications.Add(stdi);
            await db.SaveChangesAsync();
            return  db.Applications.ToList();
        }
        /// <summary>
        /// Update Application
        /// </summary>
        /// <param name="stdi"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Application> UpdateApplication(Application stdi)
        {
            var db = new TreContentdbContext();
            var dbApp = await db.Applications.FindAsync(stdi.ApplicationKey);

            if (dbApp == null)
                throw new Exception("No Application Found");

            if (String.IsNullOrEmpty(stdi.ApplicationName))
            {
                throw new Exception("Need InforMation");
            }
            else
            {
                dbApp.ApplicationKey = stdi.ApplicationKey;
                dbApp.ApplicationName = stdi.ApplicationName;
                dbApp.Owner = stdi.ApplicationName;

                await db.SaveChangesAsync();
                return dbApp;
            }
        }
        /// <summary>
        /// Delete Application by ApplycationKey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Application> DeleteApp(int id)
        {
            var db = new TreContentdbContext();
            var dbApp = await db.Applications.FindAsync(id);
            if (dbApp == null)
                throw new Exception("No Attributes Found");

            db.Applications.Remove(dbApp);
            await db.SaveChangesAsync();
            return dbApp;
        }
        /// <summary>
        /// Get All Treenode By application ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TreeNodesDTO> GetTreeNodeByApplicationId(int id)
        {
            using (var db = new TreContentdbContext())
            {
                var query = from t in db.TreeNodes
                            join a in db.Applications on t.ApplicationKey equals a.ApplicationKey
                            where t.ApplicationKey == id
                            select new TreeNodesDTO()
                            {
                                Id = t.Id,
                                Desc =t.Desc ,
                                NodeType = t.NodeType,
                                ParentId= t.ParentId,
                                DateSave=t.DateSave,
                                Owner=t.Owner,
                                ApplicationKey=t.ApplicationKey
                            };
                return query.ToList();
            }
        }
    }
}
