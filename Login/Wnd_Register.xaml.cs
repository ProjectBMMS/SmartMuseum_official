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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Login
{
    /// <summary>
    /// Logica di interazione per Wnd_Register.xaml
    /// </summary>
    public partial class Wnd_Register : Window
    {
        public Wnd_Register()
        {
            InitializeComponent();
            Txt_Username.MaxLength = 16;
            Pwd_Password.MaxLength = 16;
            Pwd_ConfermaPW.MaxLength = 16;
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Btn_Aggiungi_Click(object sender, RoutedEventArgs e)
        {
            if (Txt_Nome.Text == "")
                MessageBox.Show("Si prega di ricontrollare il nome inserito");
            else if (Txt_Cognome.Text == "")
                MessageBox.Show("Si prega di ricontrollare il cognome inserito");
            else if (Txt_Email.Text == "")
                MessageBox.Show("Si prega di ricontrollare l'e-mail inserita");
            else if (Txt_Username.Text == "" || Txt_Username.Text.Length < 3)
                MessageBox.Show("Si prega di ricontrollare l'username inserito, ricorda deve contenere un minimo di 3 caratteri");
            else if (Pwd_Password.Password == "" || Pwd_Password.Password.Length < 8)
                MessageBox.Show("Si prega di ricontrollare la password inserita, ricorda deve contenere un minimo di 8 caratteri");
            else if (Pwd_ConfermaPW.Password != Pwd_Password.Password)
                MessageBox.Show("Si prega di ricontrollare la password inserita");
            else
            {
                string path = "registro.xml";
                XDocument xmlDoc = XDocument.Load(path);
                XElement xmlPersona = new XElement("persona");
                xmlPersona.Add(new XElement("email", Txt_Email.Text));
                xmlPersona.Add(new XElement("nome", Txt_Nome.Text + " " + Txt_Cognome.Text));
                xmlPersona.Add(new XElement("username", Txt_Username.Text));
                xmlPersona.Add(new XElement("password", Pwd_Password.Password));

                xmlDoc.Element("persone").Add(xmlPersona);
                xmlDoc.Save("registro.xml");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
