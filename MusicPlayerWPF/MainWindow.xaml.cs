using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MusicPlayerWPF.Models;

namespace MusicPlayerWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //private readonly string _imagePath;
    //_imagePath = "Resources\\default-music-image.png";
    //List<Track> tracks = new List<Track>
    //{
    //    new Track
    //    {
    //        Id = 1,
    //        Title = "Заголовок",
    //        Description = "Описание",
    //        Author = "Автор",
    //    },

    //};

    /*
     * Пауза - &#xE769;
     * Играть - &#xE768;
     * 
     * Не повторять - F5E7;
     * Повторять трек - &#xE8ED;
     * Повторять все - &#xE8EE;
     */

    public MainWindow()
    {
        InitializeComponent();

        
        tracksList.ItemsSource = new string[]{ "Заголовок 1", "Заголовок 2" };
    }

}
