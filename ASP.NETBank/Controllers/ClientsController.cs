using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bll.Core.Interface;
using Dal.Core.ModelDTO;

namespace ASP.NETBank.Controllers
{
    [RoutePrefix("api/Clients")]
    public class ClientsController : ApiController
    {
        private IBllFactory _bllFactory;

        public ClientsController(IBllFactory bllFactory)
        {
            _bllFactory = bllFactory ?? throw new ArgumentNullException(nameof(bllFactory));
        }

        /// <summary>
        ///getAllClients
        /// </summary>
        /// <returns></returns>
        [Route("getAllClients")]
        [HttpGet]
        public IHttpActionResult GetAllClients()
        {
            try
            {
                List<ClientsDto> result = _bllFactory.UserBll.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }
    }
}
