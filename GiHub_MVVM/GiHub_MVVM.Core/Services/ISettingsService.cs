using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiHub_MVVM.Core.Services
{
    public interface ISettingsService
    {
        void SetListCount(int count);
        int GetListCount();
    }
}
