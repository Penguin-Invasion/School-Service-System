using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services
{
    public interface IEntryService
    {
        public Task<bool> addEntry(string SecretKey, string Plaque);
    }
}
