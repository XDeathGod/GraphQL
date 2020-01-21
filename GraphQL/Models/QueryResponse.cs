using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphQL.Models
{
    public class QueryResponse
    {
        //
        // Summary:
        //     The data of the response
        public dynamic Data { get; set; }
        //
        // Summary:
        //     The Errors if ocurred
        public List<string> Errors { get; set; }
    }
}