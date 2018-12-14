using GiHub_MVVM.Core.Models;
using Newtonsoft.Json;
using PortableRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GiHub_MVVM.Core.Common
{
    public class GitApiReader
    {
        private string repoListUrl = "https://api.github.com/repositories?page=1&per_page=";
        private string repoSummaryUrl = "https://api.github.com/repos/";
        private int repoItemsCount = 0;

        public GitApiReader()
        {
            repoItemsCount = AppSettings.RepoListCount;
        }

        public async Task<List<GitRepository>> GetRepositories()
        {
            var retList = new List<GitRepository>();
            string apiData = "";
            string url = repoListUrl + repoItemsCount.ToString();

            try
            {
                var client = new RestClient { BaseUrl = repoListUrl, UserAgent = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36 OPR/56.0.3051.104" };
                var req = new RestRequest(repoItemsCount.ToString(), HttpMethod.Get);
                var result = await client.SendAsync<string>(req);
                apiData = result.Content;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            var list = JsonConvert.DeserializeObject<List<GitRepository>>(apiData).Take(repoItemsCount);
            retList.AddRange(list);

            return retList;
        }

        public async Task<GitRepoSummary> GitRepoSummary(string full_name)
        {
            GitRepoSummary retVal = null;
            string apiData = "";

            using (HttpClient web = new HttpClient())
            {
                string url = repoSummaryUrl + full_name;
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(url);
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");
                    request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36 OPR/56.0.3051.104");

                    HttpResponseMessage response = await web.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        HttpContent responseContent = response.Content;
                        apiData = await responseContent.ReadAsStringAsync();
                        retVal = JsonConvert.DeserializeObject<GitRepoSummary>(apiData);
                    }
                }
                catch (Exception)
                {
                    apiData = "{}";
                }
            }

            return retVal;
        }

    }
}