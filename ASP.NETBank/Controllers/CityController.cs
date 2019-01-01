using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bll.Core.Interface;

namespace ASP.NETBank.Controllers
{
    [RoutePrefix("api/City")]
    public class CityController : ApiController
    {
        private IBllFactory _bllFactory;

        public CityController(IBllFactory bllFactory)
        {
            _bllFactory = bllFactory ?? throw new ArgumentNullException(nameof(bllFactory));
        }

        /// <summary>
        ///getAllCity
        /// </summary>
        /// <returns></returns>
        [Route("getAllCity")]
        [HttpGet]
        public IHttpActionResult GetAllCity()
        {
            try
            {
                var result = _bllFactory.CityBll.GetAllCity();
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
