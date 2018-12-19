using GiHub_MVVM.Core.Models;
using GiHub_MVVM.Core.Services;
using Newtonsoft.Json;
using PortableRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GiHub_MVVM.Core.Common
{
    public class GitApiReader : IGitReposService, IGitRepoSummaryService
    {
        private string repoListUrl = "https://api.github.com/repositories?page=1&per_page=";
        private string repoSummaryUrl = "https://api.github.com/repos/";
        private int RepoItemsCount { get; set; }

        public GitApiReader()
        {
            RepoItemsCount = AppSettings.RepoListCount;
        }

        public GitApiReader(int count)
        {
            RepoItemsCount = count;
        }

        public async Task<List<GitRepository>> GetRepositories()
        {
            var retList = new List<GitRepository>();
            string apiData = "";

            try
            {
                var client = new RestClient { BaseUrl = repoListUrl, UserAgent = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36 OPR/56.0.3051.104" };
                var request = new RestRequest(RepoItemsCount.ToString(), HttpMethod.Get);
                var result = await client.SendAsync<string>(request);
                apiData = result.Content;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            if (!String.IsNullOrWhiteSpace(apiData))
            {
                var list = JsonConvert.DeserializeObject<List<GitRepository>>(apiData).Take(RepoItemsCount);
                retList.AddRange(list);
            }

            return retList;
        }

        public async Task<GitRepoSummary> GitRepoSummary(string full_name)
        {
            GitRepoSummary retVal = null;
            string apiData = "";

            try
            {
                var client = new RestClient { BaseUrl = repoSummaryUrl, UserAgent = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36 OPR/56.0.3051.104" };
                var request = new RestRequest(full_name, HttpMethod.Get);
                var result = await client.SendAsync<string>(request);
                apiData = result.Content;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            if(!String.IsNullOrWhiteSpace(apiData))
                retVal = JsonConvert.DeserializeObject<GitRepoSummary>(apiData);

            return retVal;
        }

    }
}