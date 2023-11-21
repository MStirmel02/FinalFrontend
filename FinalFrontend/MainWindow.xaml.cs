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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FinalDataObjects;
using FinalLogic;

namespace FinalFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserManager _userManager;
        private UserModel _userModel = new UserModel();

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(UserModel model)
        {
            _userModel = model;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _userManager = new UserManager();
            if (_userModel.Equals(new UserModel()))
            {
                ChangeMenuForLogin();
            } 
            else
            {
                ChangeMenuForLogOut();
            }
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

        }

        private void ChangeMenuForLogOut()
        {
            _userModel = new UserModel();
            mnuAccount.Visibility= Visibility.Visible;
            mnuCreateAccount.Visibility = Visibility.Visible;
            mnuLogout.Visibility = Visibility.Collapsed;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            ChangeMenuForLogOut();

        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if ( tbxCreateUsername == null)
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


        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void ViewObjList_Click(object sender, RoutedEventArgs e)
        {
            ObjectList objectList = new ObjectList(_userModel);
            var dialog = objectList.ShowDialog();
        }
    }
}
