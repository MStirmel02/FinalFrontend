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
        List<string> _objectTypes = new List<string>();
        FullObjectModel _object = new FullObjectModel();
        UserModel _user = new UserModel();
        bool _isInit = false;
        string _filePath = string.Empty;

        public ObjectInfo()
        {
            InitializeComponent();
        }
        public ObjectInfo(string id, UserModel user)
        {
            InitializeComponent();
            GetPath();
            GetObjectInfo(id);
            GetObjectComments(id);
            _user = user;
            if (_user.UserId != null)
            {
                UIForLoggedUser();
            }
            else
            {
                UIForGuest();
            }
            _isInit = true;
        }

        public void GetPath()
        {
            _filePath = _objectManager.GetPath();
        }

        public void GetTypes()
        {
            _objectTypes = _objectManager.GetObjectTypes();
            cbxObjectType.Items.Clear();
            cbxObjectType.ItemsSource = _objectTypes;
            cbxObjectType.SelectedIndex = cbxObjectType.Items.IndexOf(_object.ObjectTypeID);
        }

        public void UIForLoggedUser()
        {
            btnPostComment.Visibility = Visibility.Visible;
            if (_user.Roles.Contains("Reviewer") || _user.Roles.Contains("Admin"))
            {
                btnEditObject.Visibility = Visibility.Visible;
            }
        }

        public void UIForGuest()
        {
            btnPostComment.Visibility = Visibility.Collapsed;
            btnEditObject.Visibility = Visibility.Collapsed;
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

            if (_filePath != string.Empty)
            {//ImageViewer1.Source = new BitmapImage(new Uri("Creek.jpg", UriKind.Relative));
                string img = _filePath + "\\" + _object.Image;
                try
                {
                    imgObjectImage.Source = new BitmapImage(new Uri(img, UriKind.Absolute));
                }
                catch (Exception)
                {
                    MessageBox.Show("Image failed to load.", "Image Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        public void GetObjectComments(string id)
        {
            _comments = _commentManager.GetCommentsByObjectId(id);
            datCommentList.ItemsSource = _comments;
        }

        private void datCommentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CommentWindow commentWindow = new CommentWindow((CommentModel) datCommentList.SelectedItem, _user);
            commentWindow.ShowDialog();
            datCommentList.ItemsSource = _comments;
            GetObjectComments(_object.ObjectID);
        }

        private void btnPostComment_Click(object sender, RoutedEventArgs e)
        {
            CommentWindow commentWindow = new CommentWindow(_user, _object.ObjectID);
            commentWindow.ShowDialog();
            GetObjectComments(_object.ObjectID);
        }

        private void btnEditObject_Click(object sender, RoutedEventArgs e)
        {
            lblObjectType.Visibility = Visibility.Collapsed;
            cbxObjectType.Visibility = Visibility.Visible;
            lblRightAscension.IsReadOnly = false;
            lblDeclination.IsReadOnly = false;
            lblRedshift.IsReadOnly = false;
            lblApparentMagnitude.IsReadOnly = false;
            lblAbsoluteMagnitude.IsReadOnly = false;
            lblMass.IsReadOnly = false;
            tbxObjectDescription.IsReadOnly = false;

            btnEditObject.Visibility = Visibility.Collapsed;
            btnCancelEdit.Visibility = Visibility.Visible;
            btnSaveEdit.Visibility = Visibility.Visible;

            GetTypes();
        }

        public void StopEdit()
        {
            lblObjectType.Visibility = Visibility.Visible;
            cbxObjectType.Visibility = Visibility.Collapsed;
            lblRightAscension.IsReadOnly = true;
            lblDeclination.IsReadOnly = true;
            lblRedshift.IsReadOnly = true;
            lblApparentMagnitude.IsReadOnly = true;
            lblAbsoluteMagnitude.IsReadOnly = true;
            lblMass.IsReadOnly = true;
            tbxObjectDescription.IsReadOnly = true;

            btnEditObject.Visibility = Visibility.Visible;
            btnCancelEdit.Visibility = Visibility.Collapsed;
            btnSaveEdit.Visibility = Visibility.Collapsed;

            cbxObjectType.ItemsSource = null;
        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("Cancel Editing?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (response == MessageBoxResult.Yes)
            {
                StopEdit();
            }
        }

        private void btnSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            

            FullObjectModel editObject = new FullObjectModel()
            {
                ObjectTypeID = cbxObjectType.SelectedItem.ToString(),
                RightAscension = lblRightAscension.Text,
                Declination = lblDeclination.Text,
                Redshift = Convert.ToDouble(lblRedshift.Text),
                ApparentMagnitude = Convert.ToDouble(lblApparentMagnitude.Text),
                AbsoluteMagnitude = Convert.ToDouble(lblAbsoluteMagnitude.Text),
                Mass = lblMass.Text,
                Description = tbxObjectDescription.Text,
                AcceptUser = _object.AcceptUser,
                SubmitUser = _object.SubmitUser,
                DateAccepted = _object.DateAccepted,
                DateSubmitted = _object.DateSubmitted,
                Image = _object.Image,
                ObjectID = _object.ObjectID,
                
            };

            bool result = _objectManager.EditObject(editObject, _user.UserId, "Edit");

            if (result)
            {
                MessageBox.Show("Object Edited Successfully.", "Success", MessageBoxButton.OK);
                StopEdit();
            }
            else
            {
                MessageBox.Show("Edit Failure.", "Failure", MessageBoxButton.OK);
            }
            GetObjectInfo(_object.ObjectID);

        }

        private void lblRedshift_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isInit)
            {
                return;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(lblRedshift.Text, @"^(?!.*-.*-)(?!.*\..*\.)[0-9.-]*$"))
            {
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("Please enter only numbers.");
                lblRedshift.Text = lblRedshift.Text.Remove(lblRedshift.Text.Length - 1);
            }
        }
    }
}
