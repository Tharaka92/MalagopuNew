using GenericAPI.Data_Access;
using GenericAPI.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenericAPI.Controllers
{
    public class LoanController : ApiController
    {
        WebAPIConfigRepository apiConfigurationsRepository = null;
        LoanDetailRepository loanDetailRepository = null;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public LoanController()
        {
            apiConfigurationsRepository = new WebAPIConfigRepository();
            loanDetailRepository = new LoanDetailRepository();
        }

        // GET: api/Loan
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/{serviceId}/{securityId}/{writeDataFlag}")]
        public IHttpActionResult Get(int serviceId, int securityId, bool writeDataFlag)
        {
            try
            {
                //WebApiConfigModel retrievedConfigs = apiConfigurationsRepository.Retrieve(serviceId);
                //if (retrievedConfigs != null)
                //{
                //    IEnumerable<LoanDetailModel> retrievedLoanDetails = loanDetailRepository.Retrieve(retrievedConfigs, securityId);

                //    if (writeDataFlag)
                //    {
                //        foreach (LoanDetailModel modelToSave in retrievedLoanDetails)
                //        {
                //            loanDetailRepository.Create(modelToSave);
                //        }
                //    }
                //    return Content(HttpStatusCode.OK, retrievedLoanDetails, Configuration.Formatters.XmlFormatter);
                //}
                //return null;
                throw new DivideByZeroException();
            }
            catch (DivideByZeroException ex)
            {
                logger.ErrorException("Exception Occured", ex);
                return null;
                //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //{
                //    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                //    ReasonPhrase = "Critical Exception"
                //});
            }
        }

        // POST: api/Loan
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Loan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Loan/5
        public void Delete(int id)
        {
        }
    }
}
