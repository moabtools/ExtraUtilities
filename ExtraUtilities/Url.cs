using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;

namespace ExtraUtilities
{
    public class Url
    {
        public static IEnumerable<string> NormalizeHost(string uri)
        {
            if (uri.ToLowerInvariant().StartsWith("https://")) uri = uri.Remove(0, 8);
            if (uri.ToLowerInvariant().StartsWith("http://")) uri = uri.Remove(0, 7);
            if (uri.ToLowerInvariant().StartsWith("www.")) uri = uri.Remove(0, 4);

            List<string> hosts = new()
            {
                $"https://{uri}",
                $"https://www.{uri}",
                $"http://{uri}",
                $"http://www.{uri}",
            };

            return hosts;
        }

        public static async Task<IEnumerable<Redirect>> DetectRedirectsAsync(Uri uri)
        {
            HttpClientHandler handler = new()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true,
                AllowAutoRedirect = false
            };
            using HttpClient httpClient = new(handler);

            List<Redirect> redirects = new();

            int step = 0;

            while (true)
            {
                HttpResponseMessage response;

                try
                {
                    response = await httpClient.GetAsync(uri);
                }
                catch
                {
                    redirects.Add(new(step, uri, null, null));
                    break;
                }

                if (response.StatusCode == HttpStatusCode.Found ||
                    response.StatusCode == HttpStatusCode.Redirect ||
                    response.StatusCode == HttpStatusCode.TemporaryRedirect ||
                    response.StatusCode == HttpStatusCode.PermanentRedirect ||
                    response.StatusCode == HttpStatusCode.Moved ||
                    response.StatusCode == HttpStatusCode.MovedPermanently)
                {
                    HttpResponseHeaders headers = response.Headers;

                    if (headers is not null && headers.Location is not null)
                    {
                        redirects.Add(new(step, uri, response.Headers.Location, response.StatusCode));
                        uri = headers.Location;
                        step++;
                        continue;
                    }
                    else
                    {
                        redirects.Add(new(step, uri, null, null));
                        break;
                    }

                }
                else
                {
                    redirects.Add(new(step, uri, null, response.StatusCode, response.StatusCode == HttpStatusCode.OK && redirects.Count == 0));
                    break;
                }
            }

            return redirects;
        }
    }

    public class Redirect
    {
        public int Step { get; set; }
        public Uri From { get; set; } = null!;
        public Uri? To { get; set; } = null;
        public HttpStatusCode? StatusCode { get; set; } = null;
        public bool IsMain { get; set; }

        public Redirect(int step, Uri from, Uri? to, HttpStatusCode? statusCode, bool isMain = false)
        {
            Step = step;
            From = from;
            To = to;
            StatusCode = statusCode;
            IsMain = isMain;
        }
    }
}
