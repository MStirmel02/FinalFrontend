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
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        UserManager _userManager = new UserManager();
        UserModel _user = new UserModel();
        UserModel _editUser = new UserModel();
        bool _isInit = false;

        public UserInfo()
        {
            InitializeComponent();
        }
        public UserInfo(string UserId, UserModel editUser)
        {
            InitializeComponent();
            _editUser = editUser;
            _user.UserId = UserId;
            lblUser.Content = UserId;
            RefreshRoles();
        }

        public void RefreshRoles()
        {
            _isInit = false;
            List<string> userroles = _userManager.GetUserRoles(_user);
            _user.Roles = new List<string>();
            foreach (string role in userroles)
            {
                _user.Roles.Add(role);
            }
            if (_user.Roles.Contains("User"))
            {
                cbxUserCheck.IsChecked = true;
            }
            if (_user.Roles.Contains("Reviewer"))
            {
                cbxReviewerCheck.IsChecked = true;
            }
            if (_user.Roles.Contains("Admin"))
            {
                cbxAdminCheck.IsChecked = true;
            }
            _isInit = true;
        }

        private void cbxUserCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (_isInit)
            {
                try
                {
                    bool result = _userManager.AddRole(_user.UserId, "User", _editUser.UserId);
                    if (result)
                    {
                        MessageBox.Show("Updated successfully", "Updated", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Update Failed", "Failed", MessageBoxButton.OK);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Update Failed", "Failed", MessageBoxButton.OK);
                }
                RefreshRoles();
            }
            
        }

        private void cbxUserCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_isInit)
            {
                try
                {
                    bool result = _userManager.RemoveRole(_user.UserId, "User", _editUser.UserId);
                    if (result)
                    {
                        MessageBox.Show("Removed Successfully", "Removed", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Remove Failed", "Failed", MessageBoxButton.OK);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Remove Failed", "Failed", MessageBoxButton.OK);
                }
                RefreshRoles();
            }
            

        }

        private void cbxReviewerCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (_isInit)
            {
                try
                {
                    bool result = _userManager.AddRole(_user.UserId, "Reviewer", _editUser.UserId);
                    if (result)
                    {
                        MessageBox.Show("Updated successfully", "Updated", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Update Failed", "Failed", MessageBoxButton.OK);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Update Failed", "Failed", MessageBoxButton.OK);
                }
                RefreshRoles();
            }
            

        }

        private void cbxReviewerCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_isInit)
            {
                try
                {
                    bool result = _userManager.RemoveRole(_user.UserId, "Reviewer", _editUser.UserId);
                    if (result)
                    {
                        MessageBox.Show("Removed Successfully", "Removed", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Remove Failed", "Failed", MessageBoxButton.OK);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Remove Failed", "Failed", MessageBoxButton.OK);
                }
                RefreshRoles();
            }
            

        }

        
        private void cbxAdminCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (_isInit)
            {
                try
                {
                    bool result = _userManager.AddRole(_user.UserId, "Admin", _editUser.UserId);
                    if (result)
                    {
                        MessageBox.Show("Updated successfully", "Updated", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Update Failed", "Failed", MessageBoxButton.OK);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Update Failed", "Failed", MessageBoxButton.OK);
                }
                RefreshRoles();

            }
        }

        private void cbxAdminCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_isInit)
            {
                try
                {
                    bool result = _userManager.RemoveRole(_user.UserId, "Admin", _editUser.UserId);
                    if (result)
                    {
                        MessageBox.Show("Removed Successfully", "Removed", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Remove Failed", "Failed", MessageBoxButton.OK);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Remove Failed", "Failed", MessageBoxButton.OK);
                }
                RefreshRoles();
            }
        }

        
    }
}
