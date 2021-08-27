using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team2Application.Services
{
    public interface ILibraryResourceBroadcastService
    {
        void LibraryResourceAdded(int id, string name, string recommandation, string url);

        void LibraryResourceDeleted(int id);

        void LibraryResourceUpdated(int id, string name, string recommandation, string url);
    }
}