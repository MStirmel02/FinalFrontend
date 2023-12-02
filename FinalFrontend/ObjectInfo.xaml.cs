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
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;

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
        string[] _postFilePath;
        string _postFile = string.Empty;
        string[] _postType;
        ImageSource _imageSource;
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
        public ObjectInfo(UserModel user)
        {
            InitializeComponent();
            GetPath();
            GetTypes();
            btnPostObject.Visibility = Visibility.Visible;
            btnCancelEdit.Visibility = Visibility.Visible;
            btnUploadImage.Visibility = Visibility.Visible;

            lblObjectName.IsReadOnly = false;
            lblObjectType.IsReadOnly = false;
            lblRightAscension.IsReadOnly = false;
            lblDeclination.IsReadOnly = false;
            lblRedshift.IsReadOnly = false;
            lblApparentMagnitude.IsReadOnly = false;
            lblAbsoluteMagnitude.IsReadOnly = false;
            lblMass.IsReadOnly = false;
            tbxObjectDescription.IsReadOnly = false;
            
            lblObjectType.Visibility = Visibility.Collapsed;
            cbxObjectType.Visibility = Visibility.Visible;

            _user.UserId = user.UserId;


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
            {
                string img = _filePath + "\\" + _object.Image;
                try
                {
                    imgObjectImage.Source = new BitmapImage(new Uri(img, UriKind.Absolute));
                    _imageSource = imgObjectImage.Source;
                }
                catch (Exception)
                {
                    MessageBox.Show("Image failed to load.", "Image Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (_object.AcceptUser == null)
            {
                UpdateForRequest();
            }
        }

        public void UpdateForRequest()
        {
            btnEditObject.Visibility = Visibility.Collapsed;
            btnPostComment.Visibility = Visibility.Collapsed;

            btnAcceptObject.Visibility = Visibility.Visible;
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
            lblObjectName.IsReadOnly = true;
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
            var response = MessageBox.Show("Cancel?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (_object.ObjectID == null)
            {
                this.Close();
            }
            if (response == MessageBoxResult.Yes)
            {
                StopEdit();
            }
        }

        private void btnSaveEdit_Click(object sender, RoutedEventArgs e)
        {

            if (_imageSource != imgObjectImage.Source)
            {
                Random rnd = new Random();
                int fileName = rnd.Next();

                try
                {
                    System.IO.File.Copy(_postFile, _filePath + "\\" + fileName.ToString() + '.' + _postType[_postType.Length - 1]);
                    _object.Image = (fileName.ToString() + '.' + _postType[_postType.Length - 1]);
                }
                catch (Exception)
                {
                    fileName = rnd.Next();
                    System.IO.File.Copy(_postFile, _filePath + "\\" + fileName.ToString() + '.' + _postType[_postType.Length - 1]);
                    _object.Image = fileName + '.' + _postType[_postType.Length - 1];
                }
            }

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

        private void lblApparentMagnitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isInit)
            {
                return;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(lblApparentMagnitude.Text, @"^(?!.*-.*-)(?!.*\..*\.)[0-9.-]*$"))
            {
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("Please enter only numbers.");
                lblApparentMagnitude.Text = lblApparentMagnitude.Text.Remove(lblApparentMagnitude.Text.Length - 1);
            }
        }

        private void lblAbsoluteMagnitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isInit)
            {
                return;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(lblAbsoluteMagnitude.Text, @"^(?!.*-.*-)(?!.*\..*\.)[0-9.-]*$"))
            {
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("Please enter only numbers.");
                lblAbsoluteMagnitude.Text = lblAbsoluteMagnitude.Text.Remove(lblAbsoluteMagnitude.Text.Length - 1);
            }
        }


        private void btnAcceptObject_Click(object sender, RoutedEventArgs e)
        {
            _object.AcceptUser = _user.UserId;
            _object.DateAccepted = DateTime.Now;

            bool result = _objectManager.EditObject(_object, _user.UserId, "Accepted");

            if(result)
            {
                MessageBox.Show("Accepted", "Accepted", MessageBoxButton.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed To Accept", "Failure", MessageBoxButton.OK);
                this.Close();
            }

        }

        private void btnPostObject_Click(object sender, RoutedEventArgs e)
        {


            if (lblObjectName.Text == string.Empty ||
                (string)cbxObjectType.SelectedItem == string.Empty ||
                lblRightAscension.Text == string.Empty ||
                lblDeclination.Text == string.Empty ||
                lblRedshift.Text == string.Empty ||
                lblApparentMagnitude.Text == string.Empty ||
                lblAbsoluteMagnitude.Text == string.Empty ||
                lblMass.Text == string.Empty || 
                tbxObjectDescription.Text == string.Empty ||
                lblObjectName.Text == "Object Name" ||
                lblRightAscension.Text == "Right Ascension" ||
                lblDeclination.Text == "Declination" ||
                lblRedshift.Text == "Redshift" ||
                lblApparentMagnitude.Text == "Apparent Magnitude" ||
                lblAbsoluteMagnitude.Text == "Absolute Magnitude" ||
                lblMass.Text == "Mass" ||
                tbxObjectDescription.Text == "Description"
                )
            {
                MessageBox.Show("All Fields must be filled.", "Fill Out", MessageBoxButton.OK);
                return;
            }
            if (imgObjectImage.Source == null)
            {
                MessageBox.Show("A Picture must be added.", "Fill Out", MessageBoxButton.OK);
                return;
            }


            Random rnd = new Random();
            string filename = rnd.Next().ToString();

            try
            {
                System.IO.File.Copy(_postFile, _filePath + "\\" + filename + '.' + _postType[_postType.Length - 1]);
                _object.Image = (filename + '.' + _postType[_postType.Length - 1]);
            }
            catch (Exception)
            {
                string try2 = rnd.Next().ToString();

                System.IO.File.Copy(_postFile, _filePath + "\\" + try2 + '.' + _postType[_postType.Length - 1]);
                _object.Image = (try2 + '.' + _postType[_postType.Length - 1]);
            }

            FullObjectModel postObject = new FullObjectModel()
            {
                ObjectTypeID = cbxObjectType.SelectedItem.ToString(),
                RightAscension = lblRightAscension.Text,
                Declination = lblDeclination.Text,
                Redshift = Convert.ToDouble(lblRedshift.Text),
                ApparentMagnitude = Convert.ToDouble(lblApparentMagnitude.Text),
                AbsoluteMagnitude = Convert.ToDouble(lblAbsoluteMagnitude.Text),
                Mass = lblMass.Text,
                Description = tbxObjectDescription.Text,
                AcceptUser = null,
                SubmitUser = _user.UserId,
                DateAccepted = null,
                DateSubmitted = DateTime.Now,
                Image = _object.Image,
                ObjectID = lblObjectName.Text,  
            };

            int result = _objectManager.PostObject(postObject);
            if (result == 1)
            {
                MessageBox.Show("Object Request Successfully Sent.", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Object Failed to Send.", "Failure", MessageBoxButton.OK);
            }


            
            this.Close();

        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Upload An Image";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";


            if (op.ShowDialog() == true)
            {
                imgObjectImage.Source = new BitmapImage(new Uri(op.FileName));
                _postFilePath = op.FileName.Split('\\');
                _postFile = op.FileName;
                _postType = _postFilePath[_postFilePath.Length - 1].Split('.');
            }

        }

    }
}
