using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MusicPlayerWPF.Models;

namespace MusicPlayerWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //private readonly string _imagePath;
    //_imagePath = "Resources\\default-music-image.png";

    /*
     * Пауза - &#xE769;
     * Играть - &#xE768;
     * 
     * Не повторять - F5E7;
     * Повторять трек - &#xE8ED;
     * Повторять все - &#xE8EE;
     */

    const string PLAY_BUTTON_SYMBOL = "\uE768";
    const string PAUSE_BUTTON_SYMBOL = "\uE769";
    const string PREVIOUS_BUTTON_SYMBOL = "\uE76B";
    const string NEXT_BUTTON_SYMBOL = "\uE76C";

    private readonly static string PATH_TO_MUSIC_FOLDER =
        Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

    private MediaPlayer mediaPlayer = new();

    private readonly List<string> _musicList;

    public Track CurrentTrack { get; set; } = new();
    

    public MainWindow()
    {
        InitializeComponent();

        DataContext = this;

        // Получение списка имен треков в пользовательской папке "Музыка"
        _musicList = MusicLoader.GetMusicList();

        // Установка списка имен треков в качестве источника данных для бокового списка
        UITracksList.ItemsSource = _musicList;
    }

    private void ButtonPressed_Click(object sender, RoutedEventArgs e)
    {
        var pressedButtonContent = (sender as Button).Content.ToString();

        switch (pressedButtonContent)
        {
            // Воспроизвести
            case PLAY_BUTTON_SYMBOL: PlayMusic(); break;

            // Пауза
            case PAUSE_BUTTON_SYMBOL: PauseMusic(); break;

            // Предыдущий
            case PREVIOUS_BUTTON_SYMBOL: PreviousTrack(); break;

            // Следующий
            case NEXT_BUTTON_SYMBOL: NextTrack(); break;
        }
    }

    private void PlayMusic()
    {
        // Получение индекса текущего выбранного трека в списке слева
        int selectedTrackIndex = UITracksList.SelectedIndex;
        
        // Проверка на то, что трек не выбран
        if ( selectedTrackIndex == -1 ) { return; }

        // Получение выбранного имени трека
        string selectedTrackName = _musicList[ selectedTrackIndex ];

        // Составление полного пути к треку в пользовательской папке "Музыка"
        var currentTrackUri = new Uri( Path.Combine(PATH_TO_MUSIC_FOLDER, selectedTrackName) );

        if ( mediaPlayer.Source == currentTrackUri ) { return; }
        if ( mediaPlayer.Source != null ) { mediaPlayer.Close(); }
        
        mediaPlayer.Open(currentTrackUri);
        mediaPlayer.Play();
    }

    private void PauseMusic()
    {
    }

    private void PreviousTrack()
    {
    }

    private void NextTrack()
    {
    }

    private void UITracksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int selectedTrackIndex = (sender as ListBox).SelectedIndex;
        string selectedTrackName = _musicList[ selectedTrackIndex ];

        if ( selectedTrackName == null ) { return; };

        CurrentTrack.Title = selectedTrackName;

        PlayMusic();
    }
}
