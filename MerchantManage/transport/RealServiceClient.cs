using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MerchantManage.transport
{
    public class RealServiceClient : ServiceClient
    {
        public async Task<String> SendRequest(Merchant mer, String currentNode)
        {
            return await Req(mer, currentNode);
           
        }
     
         async Task<String> Req(Merchant mer, String param)
        {
            HttpClient cli = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, mer.uri);
            String token = mer.username + ":" + mer.password;
            byte[] t = System.Text.Encoding.ASCII.GetBytes(token);
            String base64Str = Convert.ToBase64String(t);
            String authHeader = "Basic " + base64Str;
            System.Net.Http.Headers.AuthenticationHeaderValue header = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",base64Str);
            cli.DefaultRequestHeaders.Add("Accept-Encoding", "gzip,deflate");

            cli.DefaultRequestHeaders.Authorization = header;
            request.Content = new StringContent(param);
            request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            cli.BaseAddress = new Uri(mer.uri);

            
            //cli.DefaultRequestHeaders.Add("User-Agent", "Apachee");
            
            HttpResponseMessage resp = await cli.SendAsync(request);
            resp.EnsureSuccessStatusCode();
            String j = await resp.Content.ReadAsStringAsync();
            return j;
        }

    }
}