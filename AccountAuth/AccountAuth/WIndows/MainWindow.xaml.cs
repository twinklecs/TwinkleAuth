using AccountAuth.WIndow;
using System.Windows;
using System.Windows.Input;

namespace AccountAuth
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool isEnter = false;

        public MainWindow()
        {
            InitializeComponent();           
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWin registrationWin = new RegistrationWin();
            registrationWin.Show();

            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var users = context.Users;

                if (tbLogin.Text != "" && tbPass.Text != "")
                {
                    foreach (User user in users)
                    {
                        if (user.login == tbLogin.Text && user.pass == tbPass.Text)
                        {
                            isEnter = true;
                            MessageBox.Show($"Вы успешно вошли в аккаунт!\nФИО: {user.name}\nВаш возраст: {user.age}");
                            tbLogin.Text = ""; tbPass.Text = "";
                        }
                    }

                    if (!isEnter)
                    {
                        MessageBox.Show("Неверный логин или пароль!");
                    }
                }
                else
                    MessageBox.Show("Вы не заполнили поля!");

                isEnter = false;
            }
        }

        private void tb_passChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string hidePass = "";

            for (int i = 0; i < tbPass.Text.Length; i++)
            {
                hidePass += "*";
            }

            VisiblePass.Text = hidePass;
        }

        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
    }
}
