using GiHub_MVVM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiHub_MVVM.Core.Services
{
    public interface IGitReposService
    {
        Task<List<GitRepository>> GetRepositories();
    }

    public interface IGitRepoSummaryService
    {
        Task<GitRepoSummary> GitRepoSummary(string full_name);
    }
}
