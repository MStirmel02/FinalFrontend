using FinalDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDataLayer
{
    public interface IObjectAccess
    {

        List<ObjectModel> GetObjectList();

        List<ObjectModel> GetRequestList();

        FullObjectModel GetObjectById(string id);

        bool EditObjectById(FullObjectModel objModel, string userId, string action);

        List<string> GetObjectTypes();

        string GetPath();

        int PostObject(FullObjectModel objModel);
    }
}
