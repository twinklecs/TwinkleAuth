using System;
using System.Linq;
using System.Windows;

namespace AccountAuth.WIndow
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWin.xaml
    /// </summary>
    public partial class RegistrationWin : Window
    {       
        public RegistrationWin()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                var users = context.Users;
                if (tbFullname.Text != "" && tbAge.Text != "" && tbLogin.Text != "" && tbPass.Text != "")
                {
                    if (tbAge.Text.All(char.IsDigit))
                    {
                        User user = new User()
                        {
                            name = tbFullname.Text,
                            age = Convert.ToInt32(tbAge.Text),
                            login = tbLogin.Text,
                            pass = tbPass.Text
                        };

                        foreach (var i in users)
                        {
                            if (i.login == user.login)
                            {
                                MessageBox.Show("Такой пользователь уже существует!");
                                return;
                            }
                        }

                        context.Users.Add(user);
                        context.SaveChanges();

                        MessageBox.Show("Вы зарегистрировались!");
                        tbFullname.Text = ""; tbAge.Text = ""; tbLogin.Text = ""; tbPass.Text = "";

                        this.Close();
                    }
                    else
                        MessageBox.Show("В поле возраста указаны буквы!");
                }
                else
                    MessageBox.Show("Не все поля заполнены!");
            }
        }

        private void EventClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
