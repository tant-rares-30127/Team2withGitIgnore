using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team2Application.Services
{
    public interface IInternBroadcastService
    {
        void InternAdded(int id, string name, DateTime birthDate, string emailAddress);

        void InternDeleted(int id);

        void InternUpdated(int id, string name, DateTime birthDate, string emailAddress);
    }
}