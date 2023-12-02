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

        public List<ObjectModel> GetRequests()
        {
            try
            {
                return _objectAccess.GetRequestList();
            }
            catch (Exception)
            {
                return new List<ObjectModel>();
            }
        }

        public bool EditObject(FullObjectModel model, string userId, string action)
        {
            try
            {
                return _objectAccess.EditObjectById(model, userId, action);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public FullObjectModel GetObjectById(string id)
        {
            try
            {
                return _objectAccess.GetObjectById(id);
            } catch (Exception)
            {
                return new FullObjectModel();
            }
        }

        public List<string> GetObjectTypes()
        {
           return _objectAccess.GetObjectTypes();        
        }

        public string GetPath()
        {
            return _objectAccess.GetPath();
        }

        public int PostObject(FullObjectModel objModel)
        {
            try
            {
                return _objectAccess.PostObject(objModel);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
