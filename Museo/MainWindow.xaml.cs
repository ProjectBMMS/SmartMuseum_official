using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Museo
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Opt_OperaCrescente.IsChecked = true;
            Txt_Ricerca.IsReadOnly = true;
        }

        //caricamento dati all'interno della listbox
        private void Btn_Carica_Click(object sender, RoutedEventArgs e)
        {
            Lst_ListaOpere.Items.Clear();
            Task.Factory.StartNew(Mtd_CaricaDati);
        }

        List<string> elements = new List<string>();

        //metodo caricamento dati
        private void Mtd_CaricaDati()
        {
            elements.Clear();
            string path = @"lista_opere.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlListaOpere = xmlDoc.Element("opere");
            var xmlOpera = xmlListaOpere.Elements("opera");
            foreach (var item in xmlOpera)
            {
                XElement xmlNome = item.Element("nome");
                XElement xmlAutore = item.Element("autore");
                XElement xmlAnno = item.Element("anno");
                Opera o = new Opera();
                o.Nome = xmlNome.Value;
                o.Autore = xmlAutore.Value;
                o.Anno = xmlAnno.Value;

                //controllo scelta dell'utente sull'ordine della lista
                if (Dispatcher.Invoke(() => Opt_OperaCrescente.IsChecked == true || Opt_OperaDecrescente.IsChecked == true))
                    elements.Add(Convert.ToString($"{o.Nome}; {o.Autore}; {o.Anno}"));
                else
                    elements.Add(Convert.ToString($"{o.Autore}; {o.Nome}; {o.Anno}")); 
            }

            //ordinamento lista
            if (Dispatcher.Invoke(() => Opt_OperaCrescente.IsChecked == true))
            {
                elements.Sort();
            }
            else if (Dispatcher.Invoke(() => Opt_OperaDecrescente.IsChecked == true))
            {
                elements.Sort();
                elements.Reverse();
            }
            else if (Dispatcher.Invoke(() => Opt_AutoreCrescente.IsChecked == true))
            {
                elements.Sort();
            }
            else
            {
                elements.Sort();
                elements.Reverse();
            }

            //aggiunge gli elementi all'interno della lista
            for (int i = 0; i < elements.Count; i++)
                Dispatcher.Invoke(() => Lst_ListaOpere.Items.Add(elements[i]));

            //possibilità di ricerca di un'opera sbloccata
            Dispatcher.Invoke(() => Txt_Ricerca.IsReadOnly = false);
        }

        //apri nel browser click
        private void Btn_ApriBrowser_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(Mtd_ApriBrowserLink);
        }

        //metodo apri nel browser
        private void Mtd_ApriBrowserLink()
        {
            if (Dispatcher.Invoke(() => Convert.ToString(Lst_ListaOpere.SelectedItem) != ""))
            {
                string path = @"lista_opere.xml";
                XDocument xmlDoc = XDocument.Load(path);
                XElement xmlListaOpere = xmlDoc.Element("opere");
                var xmlOpera = xmlListaOpere.Elements("opera");
                foreach (var item in xmlOpera)
                {
                    XElement xmlNome = item.Element("nome");
                    XElement xmlLink = item.Element("link");
                    Opera o = new Opera();
                    o.Nome = xmlNome.Value;
                    o.Link = xmlLink.Value;

                    if (Dispatcher.Invoke(() => Convert.ToString(Lst_ListaOpere.SelectedItem).Contains(o.Nome)))
                        System.Diagnostics.Process.Start(o.Link);
                }

            }
            else
            {
                //messaggio di avviso
                MessageBox.Show("Selezionare un elemento", "ATTENZIONE", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //ricerca opera in tempo reale
        private void Txt_Ricerca_TextChanged(object sender, TextChangedEventArgs e)
        {
            Lst_ListaOpere.Items.Clear();
            foreach (string item in elements)
            {
                if (item.ToUpper().Contains(Txt_Ricerca.Text.ToUpper()))
                    Lst_ListaOpere.Items.Add(item);
            }
        }

        private void Txt_Ricerca_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!Txt_Ricerca.IsReadOnly)
                Txt_Ricerca.Text = null;
        }
    }
}
