using ClientApplication.HTTPClient;
using ClientApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace ClientApplication
{
    public partial class Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Consuming a Web API (Demo)";
        }

        protected void ContactAPI(object sender, EventArgs e)
        {
            int serviceId = Convert.ToInt32(ServiceId.Text);
            int securityId = Convert.ToInt32(SecurityId.Text);
            bool writeDataFlag = WriteDataFlag.Checked;

            try
            {
                DisplayGrid.DataSource = null;
                DisplayGrid.DataBind();

                List<LoanDetailModel> retrievedModel = GetData(serviceId, securityId, writeDataFlag);
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("Security Id", typeof(int)));
                dt.Columns.Add(new DataColumn("Date of Loan", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("Account type", typeof(string)));
                dt.Columns.Add(new DataColumn("Loan Qty", typeof(double)));
                foreach (LoanDetailModel response in retrievedModel)
                {
                    dr = dt.NewRow();
                    dr["Security Id"] = response.SecurityId;
                    dr["Date of Loan"] = response.DateOfLoan != null ? (object)response.DateOfLoan : DBNull.Value;
                    dr["Account Type"] = response.AccountType;
                    dr["Loan Qty"] = response.LoanQty;
                    dt.Rows.Add(dr);
                }

                DisplayGrid.DataSource = dt;
                DisplayGrid.DataBind();
            }
            catch (Exception ex)
            {
              
            }
        }

        private List<LoanDetailModel> GetData(int serviceId, int securityId, bool writeDataFlag)
        {
            try
            {
                using (HttpClient client = HTTPClientFactory.GetClient())
                {
                    var apiResponse =  client.GetAsync(ConfigurationManager.AppSettings["APIBaseURL"] + "/api/" + serviceId + "/" + securityId + "/" + writeDataFlag).Result;

                    if (apiResponse.IsSuccessStatusCode)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<LoanDetailModel>));
                        using (Stream stream = apiResponse.Content.ReadAsStreamAsync().Result)
                        {
                            return (List<LoanDetailModel>) serializer.Deserialize(stream);
                        }
                    }
                    else
                    {
                        throw new Exception(apiResponse.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}