using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Bll.Core.Ex;
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

        /// <summary>
        ///справочник гражданства
        /// </summary>
        /// <returns></returns>
        [Route("getAllCitizenship")]
        [HttpGet]
        public IHttpActionResult GetAllCitizenship()
        {
            try
            {
                var result = _bllFactory.UserBll.GetAllCitizenship();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        /// <summary>
        ///справочник место работы
        /// </summary>
        /// <returns></returns>
        [Route("getAllPlaceOfWork")]
        [HttpGet]
        public IHttpActionResult GetAllPlaceOfWork()
        {
            try
            {
                var result = _bllFactory.UserBll.GetAllPlaceOfWork();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        /// <summary>
        ///справочник инвалидность
        /// </summary>
        /// <returns></returns>
        [Route("getAllDisability")]
        [HttpGet]
        public IHttpActionResult GetAllDisability()
        {
            try
            {
                var result = _bllFactory.UserBll.GetAllDisability();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        [Route("addClientDataBase")]
        [HttpPost]
        public IHttpActionResult AddClientDataBase( ClientsDto client)
        {
            try
            {
                _bllFactory.UserBll.AddClientDataBase(client);
                return Ok(true);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        [Route("editClientDataBase")]
        [HttpPut]
        public IHttpActionResult EditClientDataBase(ClientsDto client)
        {
            try
            {
                _bllFactory.UserBll.EditClientDataBase(client);
                return Ok(true);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        [Route("deleteClientDataBase")]
        [HttpDelete]
        public IHttpActionResult DeleteClientDataBase( int id)
        {
            try
            {
                _bllFactory.UserBll.DeleteClientDataBase(id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        [Route("getAllUsers")]
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
              var result = _bllFactory.UserBll.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        [Route("addDeposit")]
        [HttpPost]
        public IHttpActionResult AddDeposit(DepositDto deposit)
        {
            try
            {
                _bllFactory.UserBll.AddDeposit(deposit);
                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        [Route("getSummBank")]
        [HttpGet]
        public IHttpActionResult GetSummBank()
        {
            try
            {
                var result = _bllFactory.UserBll.GetSummBank();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        [Route("getDepositsUser")]
        [HttpGet]
        public IHttpActionResult GetDepositsUser(int id)
        {
            try
            {
                List<TransactDto> result = _bllFactory.UserBll.GetDepositsUser(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }

        }

        [Route("ReportDeposit")]
        [HttpPost]
        public HttpResponseMessage ReportDeposit(DepositDto deposit)
        {
            try
            {
                var namefile = "Отчет о депозите";
                byte[] generateExcelresult = _bllFactory.UserBll.ReportDeposit(deposit);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(generateExcelresult);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = namefile + ".xls" };
                return response;
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                throw new ValidationException(ex.Message,"");
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }
    }
}
