using GiHub_MVVM.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiHub_MVVM.Core.Services
{
    public class SettingsService : ISettingsService
    {
        public int GetListCount()
        {
            return AppSettings.RepoListCount;
        }

        public void SetListCount(int count)
        {
            AppSettings.RepoListCount = count;
        }
    }
}
