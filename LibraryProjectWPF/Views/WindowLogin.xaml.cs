using LibraryProjectWPF.ViewModels;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace LibraryProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        public WindowLogin()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            MultiBindingExpression passwordChanged = BindingOperations.GetMultiBindingExpression(btnLogin, ButtonBase.CommandParameterProperty);
            passwordChanged.UpdateTarget();
        }
    }
}