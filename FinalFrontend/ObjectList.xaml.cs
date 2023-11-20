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
using ObjectManager = FinalLogic.ObjectManager;

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
            _userManager = new UserManager();
            _objectManager = new ObjectManager();

            InitializeComponent();
        }
        public ObjectList(UserModel userModel)
        {
            _userManager = new UserManager();
            _objectManager = new ObjectManager();
            _userModel = userModel;

            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateObjectList();

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

        private void UpdateObjectList()
        {

            datObjectList.ItemsSource = _objectManager.GetObjects();
            datObjectList.Columns.RemoveAt(4);
            datObjectList.Columns[0].Header = "Object Name";
            datObjectList.Columns[1].Header = "Object Type";
            datObjectList.Columns[2].Header = "Date Submitted";
            datObjectList.Columns[3].Header = "User Who Submitted";
        }
    }
}
