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
    }
}
