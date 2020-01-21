using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GraphQL.Controllers
{
    public class HomeController : Controller
    {
        private const string FieldQuery = @"{
                                                 item(path: ""{ItemPath}"") {
                                                   ... on {TemplateName} {
                                                   {CustomFields}
                                                 }
                                               }
                                            }";

        private const string Field = @" {CustomField} { value }";

        private readonly GraphQLClient _client;

        public HomeController()
        {
            _client = Models.AppConfiguration.Get;
        }
        public async Task<string> Index()
        {
            ViewBag.Title = "Home Page";

            //  GraphQlClient graphQl = new GraphQlClient();

            QueryRequest req = new QueryRequest
            {
                ItemPath = "/Home",
                CustomQuery = "",
                Fields = new List<string>()
                  { "title" ,"text"},
                ItemTemplateName = "SampleItem",
                QueryType = QueryTypes.FieldQuery
            };

            var replacedQuery = BuildGraphQuery(req);
            QueryResponse queryResponse = new QueryResponse();
            var request = new GraphQLRequest()
            {
                Query = replacedQuery
            };

            var result = await _client.PostAsync(request);
            queryResponse.Data = result?.Data;
            // var res = graphQl.GetItem(req);
            string d = JsonConvert.SerializeObject(queryResponse.Data);
            return d;
        }

        public static string BuildGraphQuery(QueryRequest queryRequest)
        {
            string CustomFields = string.Empty;

            if (!string.IsNullOrEmpty(queryRequest.ItemPath) && queryRequest.QueryType == QueryTypes.FieldQuery
                && queryRequest.Fields.Count > 0)
            {
                queryRequest.ItemPath = $"/sitecore/content/{queryRequest.ItemPath.TrimStart('/')}";

                for (int i = 0; i < queryRequest.Fields.Count; i++)
                {
                    if (i > 0)
                        CustomFields += ",";

                    CustomFields += Field.Replace("{CustomField}", queryRequest.Fields[i].ToString());
                }

                return FieldQuery.Replace("{ItemPath}", queryRequest.ItemPath).Replace("{TemplateName}", queryRequest.ItemTemplateName).Replace("{CustomFields}", CustomFields);


            }
            return queryRequest.CustomQuery;
        }
    }
}