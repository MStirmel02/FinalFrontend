using FinalDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FinalLogic;

namespace FinalFrontend
{
    /// <summary>
    /// Interaction logic for ObjectInfo.xaml
    /// </summary>
    public partial class ObjectInfo : Window
    {
        ObjectManager _objectManager = new ObjectManager();
        CommentManager _commentManager = new CommentManager();
        List<CommentModel> _comments = new List<CommentModel>();
        FullObjectModel _object = new FullObjectModel();
        UserModel _user = new UserModel();
        public ObjectInfo()
        {
            InitializeComponent();
        }
        public ObjectInfo(string id, UserModel user)
        {
            InitializeComponent();
            GetObjectInfo(id);
            GetObjectComments(id);
            _user = user;
            if (!user.Equals(new UserModel()))
            {
                UIForLoggedUser();
            }
            else
            {
                UIForGuest();
            }
        }

        public void UIForLoggedUser()
        {

        }

        public void UIForGuest()
        {

        }

        public void GetObjectInfo(string id)
        {
            _object = _objectManager.GetObjectById(id);
            lblObjectName.Text = _object.ObjectID;
            lblObjectType.Text = _object.ObjectTypeID;
            lblRightAscension.Text = _object.RightAscension;
            lblDeclination.Text = _object.Declination;
            lblRedshift.Text = _object.Redshift.ToString();
            lblApparentMagnitude.Text = _object.ApparentMagnitude.ToString();
            lblAbsoluteMagnitude.Text = _object.AbsoluteMagnitude.ToString();
            lblMass.Text = _object.Mass;
            tbxObjectDescription.Text = _object.Description;

        }

        public void GetObjectComments(string id)
        {
            _comments = _commentManager.GetCommentsByObjectId(id);
            datCommentList.ItemsSource = _comments;
        }
    }
}
