using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;

namespace SisVac.Framework.Http.Loggin
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        string _token = "";
        public HttpLoggingHandler(HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        { }
        async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var token = await authTokenStore.GetToken();

            //request.Headers.Add("type", "");
            //request.Headers.Add("project_id", "");
            //request.Headers.Add("private_key_id", "");
            //request.Headers.Add("private_key", "");
            //request.Headers.Add("client_email", "");
            //request.Headers.Add("client_id", "");
            //request.Headers.Add("auth_uri", "");
            //request.Headers.Add("token_uri", "");
            //request.Headers.Add("auth_provider_x509_cert_url", "");
            //request.Headers.Add("client_x509_cert_url", "");

            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            //// -------------------------------

            //var auth = request.Headers.Authorization;
            //if (auth != null && !string.IsNullOrWhiteSpace(_token))
            //{
            //    request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, _token);
            //}
            //else
            //{
            //    request.Headers.Remove("Authorization");
            //}

            if (!string.IsNullOrWhiteSpace(request?.RequestUri?.Query))            
                request.RequestUri = new Uri(request.RequestUri.AbsoluteUri + "&key=" + App.ApiKey);
            else
                request.RequestUri = new Uri(request.RequestUri.AbsoluteUri + "?key=" + App.ApiKey);

#if DEBUG
            return await new TestHttpLoggingHandler().SendAsync(base.SendAsync(request, cancellationToken), request, cancellationToken).ConfigureAwait(false);
#else
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
#endif
        }

        //private async Task<string> GetAuthorizationToken(string sa_email, string audience)
        //{
        //    string iat = DateTime.Now.ToString();
        //    string exp = DateTime.Now.AddYears(1).ToString();
        //    string iss = sa_email;
        //    string aud = audience;
        //    string sub = sa_email;
        //    string email = sa_email;

        //    var pauload = new Dictionary<string, string>();
        //    pauload.Add("iat", DateTime.Now.ToString());
        //    pauload.Add("exp", DateTime.Now.AddYears(1).ToString());
        //    pauload.Add("iss", sa_email);
        //    pauload.Add("aud", audience);
        //    pauload.Add("sub", sa_email);
        //    pauload.Add("email", sa_email);

        //    return "";
        //}


    }

    public class TestHttpLoggingHandler
    {
        public async Task<HttpResponseMessage> SendAsync(Task<HttpResponseMessage> task, HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var req = request;
            var id = Guid.NewGuid().ToString();
            var msg = $"[{id} -   Request]";

            Debug.WriteLine($"{msg}========Start==========");
            Debug.WriteLine($"{msg} {req.Method} {req.RequestUri.PathAndQuery} {req.RequestUri.Scheme}/{req.Version}");
            Debug.WriteLine($"{msg} Host: {req.RequestUri.Scheme}://{req.RequestUri.Host}");

            foreach (var header in req.Headers)
                Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

            if (req.Content != null)
            {
                foreach (var header in req.Content.Headers)
                    Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

                if (req.Content is StringContent || this.IsTextBasedContentType(req.Headers) || this.IsTextBasedContentType(req.Content.Headers))
                {
                    var result = await req.Content.ReadAsStringAsync();

                    Debug.WriteLine($"{msg} Content:");
                    Debug.WriteLine($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");

                }
            }

            var start = DateTime.Now;

            var response = await task.ConfigureAwait(false);

            var end = DateTime.Now;

            Debug.WriteLine($"{msg} Duration: {end - start}");
            Debug.WriteLine($"{msg}==========End==========");

            msg = $"[{id} - Response]";
            Debug.WriteLine($"{msg}=========Start=========");

            var resp = response;

            Debug.WriteLine($"{msg} {req.RequestUri.Scheme.ToUpper()}/{resp.Version} {(int)resp.StatusCode} {resp.ReasonPhrase}");

            foreach (var header in resp.Headers)
                Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

            if (resp.Content != null)
            {
                foreach (var header in resp.Content.Headers)
                    Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

                if (resp.Content is StringContent || this.IsTextBasedContentType(resp.Headers) || this.IsTextBasedContentType(resp.Content.Headers))
                {
                    start = DateTime.Now;
                    var result = await resp.Content.ReadAsStringAsync();
                    end = DateTime.Now;

                    Debug.WriteLine($"{msg} Content:");
                    Debug.WriteLine($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");
                    Debug.WriteLine($"{msg} Duration: {end - start}");
                }
            }

            Debug.WriteLine($"{msg}==========End==========");
            return response;
        }

        readonly string[] types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        bool IsTextBasedContentType(HttpHeaders headers)
        {
            IEnumerable<string> values;
            if (!headers.TryGetValues("Content-Type", out values))
                return false;
            var header = string.Join(" ", values).ToLowerInvariant();

            return types.Any(t => header.Contains(t));
        }
    }
}
