

using Newtonsoft.Json;

namespace GiHub_MVVM.Core.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GitRepository
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        [JsonProperty("full_name")]
        public string FullName { get; set; } = "";
        [JsonProperty("owner")]
        public Owner Owner { get; set; } = new Owner();
        [JsonProperty("description")]
        public string Description { get; set; } = "";
        [JsonProperty("url")]
        public string Url { get; set; } = "";
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Owner
    {
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; } = "";
        [JsonProperty("login")]
        public string Login { get; set; } = "";
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class GitRepoSummary
    {
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }
        [JsonProperty("watchers_count")]
        public int WatchersCount { get; set; }
        [JsonProperty("forks_count")]
        public int ForksCount { get; set; }
        [JsonProperty("open_issues_count")]
        public int OpenIssuesCount { get; set; }
    }
}