using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphQL.Models
{
    public class QueryRequest
    {
        public string ItemPath { get; set; }

        public string ItemTemplateName { get; set; }

        public QueryTypes QueryType { get; set; }

        public string CustomQuery { get; set; }

        public List<string> Fields { get; set; }
    }
}