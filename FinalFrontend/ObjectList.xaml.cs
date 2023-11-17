using FinalDataObjects;
using FinalLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
    /// Interaction logic for ObjectList.xaml
    /// </summary>
    public partial class ObjectList : Window
    {
        private UserModel _userModel = new UserModel();
        private UserManager _userManager;
        private ObjectManager _objectManager;

        public ObjectList()
        {
            InitializeComponent();
        }
        public ObjectList(UserModel userModel)
        {
            InitializeComponent();
            _userModel = userModel;
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
            mnuAccount.Visibility = Visibility.Visible;
            mnuCreateAccount.Visibility = Visibility.Visible;
            mnuLogout.Visibility = Visibility.Collapsed;
        }

        private void GetObjects()
        {

        }
    }
}
