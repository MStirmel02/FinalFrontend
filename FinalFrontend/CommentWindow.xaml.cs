using FinalDataObjects;
using FinalLogic;
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

namespace FinalFrontend
{
    /// <summary>
    /// Interaction logic for CommentWindow.xaml
    /// </summary>
    public partial class CommentWindow : Window
    {
        CommentModel _commentModel = new CommentModel();
        UserModel _userModel = new UserModel();
        CommentManager _commentManager = new CommentManager();
        string _objectId = string.Empty;

        public CommentWindow()
        {
            InitializeComponent();
        }
        public CommentWindow(UserModel userModel, string objectId)
        {
            InitializeComponent();
            _objectId = objectId;
            _userModel = userModel;
            btnPost.Visibility = Visibility.Visible;
            tbxDescription.Text = _commentModel.Description;
            
            
        }
        public CommentWindow(CommentModel commentModel, UserModel userModel)
        {
            InitializeComponent();
            _commentModel = commentModel;
            _userModel = userModel;
            tbxDescription.Text = _commentModel.Description;
            if (userModel.UserId != null) {
                if (userModel.Roles.Contains("Admin"))
                {
                    btnDelete.Visibility = Visibility.Visible;
                }
                if (userModel.UserId == commentModel.UserId)
                {
                    btnSave.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                }
            }
        }


        public void AddComment()
        {
            CommentModel commentModel = new CommentModel();

            commentModel.Description = tbxDescription.Text;

            commentModel.TimePosted = DateTime.Now;

            commentModel.ObjectId = _objectId;

            commentModel.UserId = _userModel.UserId;

            bool result = _commentManager.PostComment(commentModel);

            if (result) 
            {
                MessageBox.Show("Comment Posted Successfully.", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Comment Failed to Post.", "Failure", MessageBoxButton.OK);
            }
            this.Close();

        }

        public void EditComment()
        {
            string oldDescription = _commentModel.Description;
            _commentModel.Description = tbxDescription.Text;
            bool result = _commentManager.EditComment(_commentModel, oldDescription);
            if (result)
            {
                MessageBox.Show("Comment Updated Successfully.", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Comment Failed to Update.", "Failure", MessageBoxButton.OK);
            }
            this.Close();  
        }

        public void DeactivateComment()
        {
            bool result = _commentManager.DeactivateComment(_commentModel.CommentID);
            if (result)
            {
                MessageBox.Show("Comment Deleted Successfully.", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Comment Failed to Delete.", "Failure", MessageBoxButton.OK);
            }
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbxDescription.Text == _commentModel.Description)
            {
                var answer = MessageBox.Show("Nothing has changed. Would you like to cancel editing?", "Cancel edit", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                EditComment();
            }

        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            if (tbxDescription.Text == string.Empty)
            {
                MessageBox.Show("Must write text for a comment.");
            }
            else
            {
                AddComment();
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                DeactivateComment();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
