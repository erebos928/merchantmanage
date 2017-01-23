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
            return await Req(mer.uri, currentNode);
           
        }
     
         async Task<String> Req(String Uri, String param)
        {
            HttpClient cli = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri);// "http://localhost:8733/Design_Time_Addresses/AztechService/Service1/Explore");


            request.Content = new StringContent(param);//"<?xml version=\"1.0\" encoding=\"utf-8\"?><string>2.1</string>");
            request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            cli.BaseAddress = new Uri(Uri);//"http://localhost:8733/Design_Time_Addresses/AztechService/Service1/Explore");

            cli.DefaultRequestHeaders.Add("Accept-Encoding", "gzip,deflate");
            cli.DefaultRequestHeaders.Add("User-Agent", "Apachee");
            HttpResponseMessage resp = await cli.SendAsync(request);
            resp.EnsureSuccessStatusCode();
            String j = await resp.Content.ReadAsStringAsync();
            return j;
        }

    }
}