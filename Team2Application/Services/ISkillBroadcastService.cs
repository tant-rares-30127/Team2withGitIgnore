using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team2Application.Services
{
    public interface ISkillBroadcastService
    {
        void SkillAdded(int id, string name, string skillMatrixUrl, string description);

        void SkillDeleted(int id);

        void SkillUpdated(int id, string name, string skillMatrixUrl, string description);
    }
}