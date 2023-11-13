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
        private UserModel _userModel = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _userManager = new UserManager();
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = tbxUsername.Text;
            string pwd = _userManager.HashSha256(pbxPassword.Password);

            UserModel userModel = new UserModel();
            userModel.UserId = user;
            userModel.PasswordHash = pwd;
            userModel.Roles = new List<string>();

            _userManager.LoginUser(userModel);

        }

    }
}
