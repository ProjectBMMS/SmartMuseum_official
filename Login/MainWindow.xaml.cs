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
using System.Xml.Linq;

namespace Login
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Login_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            string path = "registro.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlListaPersone = xmlDoc.Element("persone");
            var xmlPersona = xmlListaPersone.Elements("persona");
            foreach (var item in xmlPersona)
            {
                XElement xmlEmail = item.Element("email");
                XElement xmlUsername = item.Element("username");
                XElement xmlPassword = item.Element("password");

                if (xmlUsername.Value == Txt_Username.Text && xmlPassword.Value == Pwd_Password.Password || xmlEmail.Value == Txt_Username.Text && xmlPassword.Value == Pwd_Password.Password)
                {
                    Museo.MainWindow mainWindow = new Museo.MainWindow();
                    this.Close();
                    mainWindow.Show();
                    flag = false;
                    break;
                }
                else if (xmlUsername.Value == Txt_Username.Text && xmlPassword.Value != Pwd_Password.Password || xmlEmail.Value == Txt_Username.Text && xmlPassword.Value != Pwd_Password.Password)
                {
                    MessageBox.Show("Verifica la correttezza dei dati inseriti", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Error);
                    Txt_Username.Focus();
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                MessageBox.Show("Non sei registrato? Registrati ora!", "Avviso", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void Btn_Register_Click(object sender, RoutedEventArgs e)
        {
            Wnd_Register register = new Wnd_Register();
            register.Show();
            this.Close();
        }
    }
}
