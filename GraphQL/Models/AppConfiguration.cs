using GraphQL.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GraphQL.Models
{
    public static class AppConfiguration
    {
        public static GraphQLClient Get
        {
            get { return GetGraphQLClient(); }
        }

        /// <summary>
        /// ConnectionMultiplexer instance
        /// </summary>
        private static GraphQLClient GetGraphQLClient()
        {
            JsonSerializerSettings JsonSettings = new JsonSerializerSettings();
            JsonSettings.Formatting = Formatting.Indented;
            GraphQLClientOptions options = new GraphQLClientOptions();
            options.EndPoint = new Uri(ConfigurationManager.AppSettings["EndPoint"]);
            options.JsonSerializerSettings = JsonSettings;
            var client = new GraphQLClient(options);
            // client.DefaultRequestHeaders.Add("AllowedHosts", ConfigurationManager.AppSettings["AllowedHost"]);
            // client.DefaultRequestHeaders.Add("User-Agent", "deinok");

            return client;
        }
    }
}