using FinalDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalLogic
{
    public interface IObjectManager
    {
        List<ObjectModel> GetObjects(); 

        FullObjectModel GetObjectById(string id);

        bool EditObject(FullObjectModel model, string userId, string action);

    }
}
