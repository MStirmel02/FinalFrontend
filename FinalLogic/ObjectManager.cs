using FinalDataLayer;
using FinalDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalLogic
{
    public class ObjectManager : IObjectManager
    {
        IObjectAccess _objectAccess;
        public ObjectManager() 
        {
            _objectAccess = new ObjectAccess();
        }
        public ObjectManager(IObjectAccess objectAccess)
        {
            _objectAccess = objectAccess;
        }


        public List<ObjectModel> GetObjects()
        {
            try
            {
                return _objectAccess.GetObjectList();
            }
            catch (Exception)
            {
                return new List<ObjectModel>();
            }
        }

        public bool AcceptObject(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeactivateObject(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeclineObject(string id)
        {
            throw new NotImplementedException();
        }

        public bool EditObject(FullObjectModel model)
        {
            throw new NotImplementedException();
        }

        public FullObjectModel GetObjectById(string id)
        {
            throw new NotImplementedException();
        }

    }
}
