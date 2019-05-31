using ECO3.Automation_TREE.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace ECO3.Automation_TREE.Web.Controllers
{
    public class TreeController : ApiController
    {
        ITreeService TreeService { get; set; }

        public TreeController()
        {
            this.TreeService = new TreeService();
        }

        // POST api/<controller>
        public IHttpActionResult Post()
        {
            var treeExexPath = WebConfigurationManager.AppSettings["TreeExePath"];
            var output = TreeService.Run(treeExexPath, null);
            return Ok(output);
        }
    }
}