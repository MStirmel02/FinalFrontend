using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using FinalDataObjects;
using FinalLogic;

namespace FinalFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserManager _userManager = new UserManager();
        private ObjectManager _objectManager = new ObjectManager();
        private UserModel _userModel = new UserModel();

        private bool _isInit = false;

        private List<string> _userList;
        private List<string> _userListQueried;

        private List<ObjectModel> _objectList;
        private List<ObjectModel> _objectListQueried;

        private List<ObjectModel> _requestList;
        private List<ObjectModel> _requestListQueried;

        private List<string> _funFacts;

        public MainWindow()
        {
            InitializeComponent();
            _isInit = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _funFacts = _userManager.GetFacts();

            _userList = _userManager.GetUsers();
            _userListQueried = _userList;

            _objectList = _objectManager.GetObjects();
            _objectListQueried = _objectList;

            _requestList = _objectManager.GetRequests();
            _requestListQueried = _requestList;

            FillObjectList();
            FillRequestList();
            FillUserList();

            Facts();

            

        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = tbxUsername.Text;
            string pwd = _userManager.HashSha256(pbxPassword.Password);

            UserModel userModel = new UserModel();
            userModel.UserId = user;
            userModel.PasswordHash = pwd;
            userModel.Roles = new List<string>();

            tbxUsername.Text = null;
            pbxPassword.Password = null;

            userModel = _userManager.LoginUser(userModel);

            try
            {
                if (!userModel.Equals(null))
                {
                    _userModel = userModel;
                    ChangeMenuForLogin();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect username or password.", "Incorrect information", MessageBoxButton.OK, MessageBoxImage.Warning);
                ChangeMenuForLogOut();
            }



        }


        private void ChangeMenuForLogin()
        {
            mnuAccount.Visibility = Visibility.Collapsed;
            mnuLogout.Visibility = Visibility.Visible;
            mnuCreateAccount.Visibility = Visibility.Collapsed;
            mnuLogout.Header = _userModel.UserId;

            if (_userModel.Roles.Contains("Reviewer"))
            {
                tbmReview.Visibility = Visibility.Visible;
            }
            if (_userModel.Roles.Contains("Admin"))
            {
                tbmAdmin.Visibility = Visibility.Visible;
            }

        }

        private void ChangeMenuForLogOut()
        {
            _userModel = new UserModel();
            mnuAccount.Visibility = Visibility.Visible;
            mnuCreateAccount.Visibility = Visibility.Visible;
            mnuLogout.Visibility = Visibility.Collapsed;
            tbmReview.Visibility = Visibility.Collapsed;
            tbmAdmin.Visibility = Visibility.Collapsed;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            ChangeMenuForLogOut();

        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (tbxCreateUsername == null)
            {
                MessageBox.Show("Please input a username");
            }
            if (pbxCreatePassword == null)
            {
                MessageBox.Show("Please input a password");
            }

            string user = tbxCreateUsername.Text;
            //In the interest of security, the password is hashed before it is given to a variable.
            string pwd = _userManager.HashSha256(pbxCreatePassword.Password);

            UserModel userModel = new UserModel()
            {
                UserId = user,
                PasswordHash = pwd,
                Roles = new List<string>()
            };

            if (_userManager.CreateUser(userModel))
            {
                tbxCreateUsername = null;
                pbxCreatePassword = null;

                MessageBox.Show("Account created successfully.", "Created", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                UserModel model = _userManager.LoginUser(userModel);
                if (!model.Equals(null)) {
                    _userModel = model;
                    ChangeMenuForLogin();
                }
                else
                {
                    MessageBox.Show("Account login unsuccessful.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("User already exists.", "Duplicate entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillObjectList()
        {
            datObjectList.ItemsSource = _objectListQueried;
        }

        private void FillRequestList()
        {
            datRequestList.ItemsSource = _requestListQueried;
        }

        private void FillUserList()
        {
            lbxUserList.ItemsSource = _userListQueried;
        }

        private void tbxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxSearch.Text == "Search")
            {
                tbxSearch.Text = string.Empty;
            }
        }

        private void tbxSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxSearch.Text == string.Empty)
            {
                tbxSearch.Text = "Search";
            }
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isInit)
            {
                return;
            }
            if (tbxSearch.Text == "Search" || tbxSearch.Text == string.Empty)
            {
                _userListQueried = _userList;
                _objectListQueried = _objectList;
                _requestListQueried = _requestList;
                FillRequestList();
                FillObjectList();
                FillUserList();
                return;
            }
            // This doesn't actually work as I thought it would. Oops
            if (tbmAdmin.Visibility == Visibility.Visible)
            {
                _userListQueried = _userList.Where(file => (file.ToUpperInvariant().Contains(tbxSearch.Text.ToUpperInvariant()))).ToList<string>();
                FillUserList();
            }

            if (tbmReview.Visibility == Visibility.Visible)
            {
                _requestListQueried = _requestList.Where(file => (file.ObjectID.ToUpperInvariant().Contains(tbxSearch.Text.ToUpperInvariant()))).ToList<ObjectModel>();
                FillRequestList();
            }

            if (tbmObject.Visibility == Visibility.Visible)
            {
                _objectListQueried = _objectList.Where(file => (file.ObjectID.ToUpperInvariant().Contains(tbxSearch.Text.ToUpperInvariant()))).ToList<ObjectModel>();
                FillObjectList();
            }


        }

        private void Facts()
        {
            //Take unix time and use the remainder of it divided by the count of facts in our list to get a pseudorandom index to show one specific fact.
            long num = DateTimeOffset.Now.ToUnixTimeSeconds();

            int index = (int) num % _funFacts.Count();
            
            tbkFunFacts.Text = _funFacts[index];
        }

        private void datObjectList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datObjectList.SelectedItem != null)
            {
                var obj = datObjectList.SelectedItem as ObjectModel;
                var objWindow = new ObjectInfo(obj.ObjectID, _userModel);
                objWindow.ShowDialog();
            }
        }

        private void lbxUserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(_userModel.UserId != (string) lbxUserList.SelectedItem)
            {
                if (lbxUserList.SelectedItem != null)
                {
                    var userId = lbxUserList.SelectedItem as string;
                    var userWindow = new UserInfo(userId, _userModel);
                    userWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Cannot edit your own roles.", "Nope", MessageBoxButton.OK);
            }
            
        }
    }
}
