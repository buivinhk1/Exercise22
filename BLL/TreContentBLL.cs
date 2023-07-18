using BLL.DTO;
using DAL.Repositories;
using DAL.Repositories.DTO;
using DAL.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace BLL
{
    public class TreContentBLL
    {
        private readonly DAL.TreContentDAL _DAL;
        private static readonly int CHECK_FULLNAME = 1;
        private static readonly int CHECK_FULLNAMENode = 2;
        private static readonly int CHECK_FULLNAMEApp = 3;
        public TreContentBLL()
        { 
            _DAL = new DAL.TreContentDAL();
        }
        //public List<TreeNodesDTO> GetALL()
        //{
        //    List<TreeNodesDTO> data=  _DAL.GetALL();
        //    return data;
        //}

        #region checkAttributes
        private bool checkValidation(string str, int value)
        {
            if (value == 1)
            {
                var db = new TreContentdbContext();
                var a = db.AttributesNames.FirstOrDefault(p => p.AttributesName1 == str);
                if (a != null)
                    return true;
            }
            else if(value==2)
            {
                var db = new TreContentdbContext();
                var a = db.TreeNodes.FirstOrDefault(p => p.Desc == str);
                if (a != null)
                    return true;
            }
            else if(value==3)
            {
                var db = new TreContentdbContext();
                var a = db.Applications.FirstOrDefault(p => p.ApplicationName == str);
                if (a != null)
                    return true;
            }
            return false;
        }
        #endregion

        #region GetAllNode
        /// <summary>
        /// Firt u need to get ID of this database then u can buildTree
        /// </summary>
        /// <returns></returns>
        public List<TreeNodesDTO> GetTree()
        {
            List<TreeNodesDTO> nodes = _DAL.GetALL();
            return BuildTree(nodes, null);
        }
        /// <summary>
        /// After u get ALLTree now u recursive this database and get them into a object
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<TreeNodesDTO> BuildTree(List<TreeNodesDTO> nodes, int? parentId)
        {
            List<TreeNodesDTO> tree = new List<TreeNodesDTO>();
            foreach (var node in nodes)
            {
                if (node.ParentId == parentId)
                {
                    node.Children = BuildTree(nodes, node.Id);
                    tree.Add(node);
                }
            }
            return tree;
        }
        #endregion

        #region Get Node ID
        /// <summary>
        /// Get NodeTitle and NodeType when u click in nodeTree
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TreeNodesDTO> GetNodeInfo(int id)
        {         
            var data = await _DAL.GetNodeInfo(id);
            return data;
        }
        #endregion

        #region Get Attributes ID
        /// <summary>
        /// when click nodeTree if this node have AttributesName will show for u infomation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AttributesNameDTO> GetAttributesInfo(int id)
        {
            return _DAL.GetAttributesNamesByTreeNodeId(id);
            
        }
        #endregion

        #region Get All Application
        public List<ApplicationDTO> GetApp()
        {
            List<ApplicationDTO> apps = _DAL.GetALLApplication();
            return apps;
        }
        #endregion

        #region Get All Treenode by application ID
        public List<TreeNodesDTO> GetTreeNodeByApplicationId(int id)
        {
            List<TreeNodesDTO> nodes = _DAL.GetTreeNodeByApplicationId(id);
            return BuildTree(nodes, null);
           // return _DAL.GetTreeNodeByApplicationId(id);

        }
        #endregion

        #region Get Application by ID
        public async Task<ApplicationDTO> GetAppInfo(int id)
        {
            var data = await _DAL.GetAppInfo(id);
            return data;
        }
        #endregion

        #region Update Node When Click 
        /// <summary>
        /// update nodeTree for u when u want to update somthing about title or type
        /// </summary>
        /// <param name="stdi"></param>
        /// <returns></returns>
        public async Task<CreateOrEditNodeDTO> UpdateNodes(CreateOrEditNodeDTO stdi)
        {
            var dbNode = new TreeNode();
            dbNode.Id = stdi.Id;
            dbNode.Desc = stdi.Desc;
            dbNode.NodeType = stdi.NodeType;
            dbNode.ParentId = stdi.ParentId;
            dbNode.DateSave = stdi.DateSave;
            dbNode.Owner=stdi.Owner;
            dbNode.ApplicationKey = stdi.ApplicationKey;


            var dbNodeEdit = new CreateOrEditNodeDTO();

            dbNodeEdit.Id = stdi.Id;
            dbNodeEdit.Desc = stdi.Desc;
            dbNodeEdit.NodeType = stdi.NodeType;
            dbNodeEdit.ParentId = stdi.ParentId;
            dbNodeEdit.DateSave = stdi.DateSave;
            dbNodeEdit.Owner = stdi.Owner;
            dbNodeEdit.ApplicationKey = stdi.ApplicationKey;

            await _DAL.UpdateNodes(dbNode);

            return dbNodeEdit;
        }
        #endregion
        
        #region Update Attributes in Node
        /// <summary>
        /// when u got id from TreeNode(treeNodeId) into AttributesName(ID) so u update this attributes name
        /// </summary>
        /// <param name="stdi"></param>
        /// <returns></returns>
        public async Task<CreateOrEditAttributesDTO> UpdateAttributes(CreateOrEditAttributesDTO stdi)
        {
            var dbNode = new AttributesName();
            dbNode.Id=stdi.Id;
            dbNode.TreeNodeId = stdi.TreeNodeId;
            dbNode.AttributesName1 = stdi.AttributesName;


            var dbNodeEdit = new CreateOrEditAttributesDTO();
            dbNodeEdit.Id = stdi.Id;
            dbNodeEdit.TreeNodeId = stdi.TreeNodeId;
            dbNodeEdit.AttributesName = stdi.AttributesName;

            await _DAL.UpdateAttribute(dbNode);

            return dbNodeEdit;
        }
        #endregion

        #region Update Application
        public async Task<CreateOrEditApplicationDTO> UpdateApps(CreateOrEditApplicationDTO stdi)
        {
            var dbApp = new Application();
            dbApp.ApplicationName = stdi.ApplicationName;
            dbApp.Owner = stdi.Owner;
            dbApp.ApplicationKey = stdi.ApplicationKey;


            var dbAppEdit = new CreateOrEditApplicationDTO();

            dbAppEdit.ApplicationName = stdi.ApplicationName;
            dbAppEdit.Owner = stdi.Owner;
            dbAppEdit.ApplicationKey = stdi.ApplicationKey;

            await _DAL.UpdateApplication(dbApp);

            return dbAppEdit;        
        }
        #endregion

        #region Add Node
        public async Task<CreateOrEditNodeDTO> CreateTreeNode(CreateOrEditNodeDTO stdi)
        {
            var dbNode = new TreeNode();
            dbNode.Id = stdi.Id;
            dbNode.Desc = stdi.Desc;
            dbNode.ParentId = stdi.ParentId;
            dbNode.NodeType = stdi.NodeType;
            dbNode.DateSave = stdi.DateSave;
            dbNode.Owner = stdi.Owner;
            dbNode.ApplicationKey = stdi.ApplicationKey;

            var dbNodeEdit = new CreateOrEditNodeDTO();
            dbNodeEdit.Id = stdi.Id;
            dbNodeEdit.Desc = stdi.Desc;
            dbNodeEdit.ParentId = stdi.ParentId;
            dbNodeEdit.NodeType = stdi.NodeType;
            dbNodeEdit.DateSave = stdi.DateSave;
            dbNodeEdit.Owner = stdi.Owner;
            dbNodeEdit.ApplicationKey = stdi.ApplicationKey;

            if (String.IsNullOrEmpty(stdi.Desc))
            {
                throw new Exception("Need InforMation");
            }
            else if (checkValidation(stdi.Desc, CHECK_FULLNAMENode))
            {
                throw new Exception("TreeNode Already");
            }
            else
            {
                await _DAL.CreateTreeNode(dbNode);
                return dbNodeEdit;
            }
        }
        #endregion

        #region add New Attributes
        public async Task<CreateOrEditAttributesDTO> CreateAttributes(CreateOrEditAttributesDTO stdi)
        {
            var dbNode = new AttributesName();
            dbNode.Id = stdi.Id;
            dbNode.TreeNodeId = stdi.TreeNodeId;
            dbNode.AttributesName1 = stdi.AttributesName;


            var dbNodeEdit = new CreateOrEditAttributesDTO();
            dbNodeEdit.Id=stdi.Id;
            dbNodeEdit.TreeNodeId = stdi.TreeNodeId;
            dbNodeEdit.AttributesName = stdi.AttributesName;

            if (String.IsNullOrEmpty(stdi.AttributesName))
            {

                throw new Exception("Need InforMation");
            }
            else if (checkValidation(stdi.AttributesName, CHECK_FULLNAME))
            {
                throw new Exception("Attributes Already");
            }
            else
            {
                await _DAL.CreateAttributes(dbNode);
                return dbNodeEdit;
            }
        }
        #endregion

        #region Add new Application
        public async Task<CreateOrEditApplicationDTO> CreateApplication(CreateOrEditApplicationDTO stdi)
        {
            var dbApp = new Application();
            dbApp.ApplicationName = stdi.ApplicationName;
            dbApp.Owner = stdi.Owner;
            dbApp.ApplicationKey = stdi.ApplicationKey;


            var dbAppEdit = new CreateOrEditApplicationDTO();
            dbAppEdit.ApplicationName = stdi.ApplicationName;
            dbAppEdit.Owner = stdi.Owner;
            dbAppEdit.ApplicationKey = stdi.ApplicationKey;

            if (String.IsNullOrEmpty(stdi.ApplicationName))
            {
                throw new Exception("Need InforMation");
            }
            else if (checkValidation(stdi.ApplicationName, CHECK_FULLNAMEApp))
            {
                throw new Exception("Application Already");
            }
            else
            {
                await _DAL.CreateApplication(dbApp);
                return dbAppEdit;
            }
        }
        #endregion

        #region Delete Attributes
        public Task<AttributesName> DeleteAttributes(int id)
    {
        if (id == null)
        {
            throw new Exception("No ID Found");
        }
        else return _DAL.DeleteAttributes(id);
    }
    #endregion

        #region Delete TreeNode
        public Task<TreeNode> DeleteTreeNode(int id)
        {
            if (id == null)
            {
                throw new Exception("No ID Found");
            }
            else return _DAL.DeleteTreeNode(id);
        }
        #endregion

        #region Delete Application
        public Task<Application> DeleteApplication(int id)
        {
            if (id == null)
            {
                throw new Exception("No ID Found");
            }
            else return _DAL.DeleteApp(id);
        }
        #endregion


    }
}
