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

        private UserManager _userManager;
        private ObjectManager _objectManager;
        private UserModel _userModel = new UserModel();
        public ObjectList()
        {
            InitializeComponent();
        }
        public ObjectList(UserModel model)
        {
            _userModel = model;
        }
    }
}
