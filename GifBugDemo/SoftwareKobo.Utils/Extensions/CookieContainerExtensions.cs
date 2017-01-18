using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace SoftwareKobo.Extensions
{
    public static class CookieContainerExtensions
    {
        private static readonly FieldInfo DomainTableField = typeof(CookieContainer).GetRuntimeFields().First(temp => temp.Name == "m_domainTable");

        public static void Import(this CookieContainer cookieContainer, string json)
        {
            if (cookieContainer == null)
            {
                throw new ArgumentNullException(nameof(cookieContainer));
            }
            if (json == null)
            {
                throw new ArgumentNullException(nameof(json));
            }
            if (json.Length <= 0)
            {
                return;
            }

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(json);
            foreach (var keyValuePair in dictionary)
            {
                var values = keyValuePair.Value["Values"].ToObject<JArray>();
                foreach (var value in values)
                {
                    foreach (var cookie in value.ToObject<List<Cookie>>())
                    {
                        var domain = cookie.Domain;
                        var port = cookie.Port;

                        var uriBuilder = new StringBuilder();
                        uriBuilder.Append(cookie.Secure ? "https" : "http");
                        uriBuilder.Append("://");
                        if (domain.StartsWith("."))
                        {
                            uriBuilder.Append("0");
                        }
                        uriBuilder.Append(domain);
                        if (string.IsNullOrEmpty(port) == false)
                        {
                            uriBuilder.Append(":");
                            uriBuilder.Append(port);
                        }
                        uriBuilder.Append(cookie.Path);

                        var uri = new Uri(uriBuilder.ToString(), UriKind.Absolute);
                        cookieContainer.Add(uri, cookie);
                    }
                }
            }
        }

        public static string Export(this CookieContainer cookieContainer)
        {
            if (cookieContainer == null)
            {
                throw new ArgumentNullException(nameof(cookieContainer));
            }

            var value = DomainTableField.GetValue(cookieContainer);
            return JsonConvert.SerializeObject(value);
        }
    }
}