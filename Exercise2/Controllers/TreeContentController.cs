using BLL.DTO;
using DAL.Repositories;
using DAL.Repositories.DTO;
using DAL.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.XPath;


namespace Exercise2.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class TreeContentController : Controller
    {
        private BLL.TreContentBLL _BLL;
        public readonly ILogger<TreeContentController> _logger;
        public TreeContentController(ILogger<TreeContentController> logger)
        {
            _logger = logger;
            _BLL = new BLL.TreContentBLL();
        }
        //[HttpGet("getTreeView")]
        //public List<TreeNodesDTO> GetAll()
        //{
        //    return _BLL.GetALL();          
        //}
        [HttpGet("getAllTreeView")]
        public ActionResult<List<TreeNodesDTO>> GetTree()
        {
            _logger.LogInformation("GetAllTreeView method started");
            List<TreeNodesDTO> tree = _BLL.GetTree();
            return Ok(tree);
        }
        [HttpGet("detail/{id}")]
        public ActionResult<TreeNodesDTO> GetNodeInfo(int id)
        {
            var treenode = _BLL.GetNodeInfo(id);
            //BadRequest - 400 - Badrequest - Client error
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
            if (treenode == null)
            {
                _logger.LogError("TreeNode not found with given Id");
                return NotFound("Invailid ID");
            }
            else return Ok( treenode.Result);
        }
        //[HttpGet("parentID/{id}")]
        //public JsonResult Remote_Data_Binding_Get_Employees(int? id)
        //{
        //    using (TreContentdbContext entities = new TreContentdbContext())
        //    {
        //        var data = from e in entities.TreeNodes
        //                   where (id.HasValue ? e.ParentId == id : e.ParentId == null)
        //                   select new
        //                   {
        //                       id = e.Id,
        //                       desc = e.Desc,
        //                       hasChildren = e.InverseParent.Any()
        //                   };
        //        return Json(data.ToList());
        //    }
        //}
        [HttpPost("addTreeNodes")]

        public Task<CreateOrEditNodeDTO> CreateTreeNode(CreateOrEditNodeDTO stdi)
        {

            return _BLL.CreateTreeNode(stdi);
        }
        [HttpPut("editNode")]

        public Task<CreateOrEditNodeDTO> UpdateNodes(CreateOrEditNodeDTO stdi)
        {
            if (stdi == null || stdi.Id <= 0)
                BadRequest();
            return _BLL.UpdateNodes(stdi);
        }
        [HttpDelete("deleteTreeNodes")]
        public Task<TreeNode> DeleteTreeNodes(int id)
        {
            return _BLL.DeleteTreeNode(id);
        }
        /// <summary>
        /// Get all attributes name when click nodeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getAllAttributeName/{id}")]
        public async Task<ActionResult<AttributesNameDTO>> GetAttributesName(int id)
        {
            _logger.LogInformation("GetAttributesName method started");
            var treenode = _BLL.GetAttributesInfo(id);
            if (treenode == null)
            {
                return NotFound("Invailid ID");
            }
            else return Ok(treenode);
        }
        /// <summary>
        /// u can edit treenode
        /// </summary>
        /// <param name="stdi"></param>
        /// <returns></returns>
        [HttpPost("addAttributes")]
        public Task<CreateOrEditAttributesDTO> CreateAttributes(CreateOrEditAttributesDTO stdi)
        {
            return _BLL.CreateAttributes(stdi);
        }
        /// <summary>
        /// edit attributesName value
        /// </summary>
        /// <param name="stdi"></param>
        /// <returns></returns>
        [HttpPut("editAttributes")]

        public Task<CreateOrEditAttributesDTO> UpdateAttributes(CreateOrEditAttributesDTO stdi)
        {
            if (stdi == null || stdi.Id <= 0)
                BadRequest();

            return _BLL.UpdateAttributes(stdi);
        }
        /// <summary>
        /// Delete TreeNode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deleteAttributes")]
        public Task<AttributesName> DeleteAttributes(int id)
        {
            if (id <= 0)
                BadRequest();
            return _BLL.DeleteAttributes(id);
        }
        [HttpGet("getAllApplication")]
        public ActionResult<List<ApplicationDTO>> GetApp()
        {
            _logger.LogInformation("GetAllApp method started");
            List<ApplicationDTO> tree = _BLL.GetApp();
            return Ok(tree);
        }
        [HttpGet("appdetail/{id}")]
        public ActionResult<ApplicationDTO> GetAppInfo(int id)
        {
            var App = _BLL.GetAppInfo(id);
            //BadRequest - 400 - Badrequest - Client error
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
            if (App == null)
            {
                _logger.LogError("App not found with given Id");
                return NotFound("Invailid ID");
            }
            else return Ok(App.Result);
        }
        [HttpGet("getTreebyApp/{id}")]
        public ActionResult<List<TreeNodesDTO>> GetTreeNodeByApplicationId(int id)
        {
            _logger.LogInformation("GetAllTreeView by ApplicationID method started");
            List<TreeNodesDTO> tree = _BLL.GetTreeNodeByApplicationId(id);
            return Ok(tree);
        }
        [HttpPost("addApp")]
        public Task<CreateOrEditApplicationDTO> CreateApp(CreateOrEditApplicationDTO stdi)
        {
            return _BLL.CreateApplication(stdi);
        }
        [HttpPut("editApp")]
        public Task<CreateOrEditApplicationDTO> UpdateApp(CreateOrEditApplicationDTO stdi)
        {
            if (stdi == null || stdi.ApplicationKey <= 0)
                BadRequest();

            return _BLL.UpdateApps(stdi);
        }
        [HttpDelete("deleteApp")]
        public Task<Application> DeleteApp(int id)
        {
            if (id <= 0)
                BadRequest();
            return _BLL.DeleteApplication(id);
        }
        

    }
}
