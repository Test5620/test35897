using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildTest.Services.Pushover
{
    public interface IPushoverService
    {
        Task<bool> Message(string appToken, string userKey, string message);
    }
}
