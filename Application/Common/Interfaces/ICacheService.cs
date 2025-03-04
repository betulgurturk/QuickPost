using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICacheService
    {
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
        Task Clear(string key);
        void ClearAll();
    }
}
