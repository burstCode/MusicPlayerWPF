﻿using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MusicPlayerWPF.Models;

namespace MusicPlayerWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
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

    enum PlayerState
    {
        AtPause = 0,
        Playing = 1
    }

    const string PLAY_BUTTON_SYMBOL = "\uE768";
    const string PAUSE_BUTTON_SYMBOL = "\uE769";
    const string PREVIOUS_BUTTON_SYMBOL = "\uE76B";
    const string NEXT_BUTTON_SYMBOL = "\uE76C";

    private readonly static string PATH_TO_MUSIC_FOLDER =
        Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

    private MediaPlayer _mediaPlayer = new();
    private DispatcherTimer _timer = new();

    private readonly List<string> _musicList;
    private string _playButtonCurrentSymbol = PLAY_BUTTON_SYMBOL;
    private bool _isMusicInstalled = false;
    private bool _isSliderDragging = false;

    public Track CurrentTrack { get; set; } = new();

    private PlayerState _playerState = PlayerState.AtPause;

    public string PlayButtonCurrentSymbol
    {
        get => _playButtonCurrentSymbol;
        set
        {
            _playButtonCurrentSymbol = value;
            OnPropertyChanged("PlayButtonCurrentSymbol");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MainWindow()
    {
        InitializeComponent();

        DataContext = this;

        // Получение списка имен треков в пользовательской папке "Музыка"
        _musicList = MusicLoader.GetMusicList();

        // Установка списка имен треков в качестве источника данных для бокового списка
        UITracksList.ItemsSource = _musicList;

        // Настройка таймера
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += Timer_Tick;
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
        if ( !_isMusicInstalled )
        {
            // Получение индекса текущего выбранного трека в списке слева
            int selectedTrackIndex = UITracksList.SelectedIndex;

            // Проверка на то, что трек не выбран
            if (selectedTrackIndex == -1) { return; }

            // Получение выбранного имени трека
            string selectedTrackName = _musicList[selectedTrackIndex];

            // Составление полного пути к треку в пользовательской папке "Музыка"
            var currentTrackPath = Path.Combine(PATH_TO_MUSIC_FOLDER, selectedTrackName);

            if (_mediaPlayer.Source == new Uri(currentTrackPath)) { return; }
            if (_mediaPlayer.Source != null) { _mediaPlayer.Close(); }

            _mediaPlayer.Open(new Uri(currentTrackPath));

            // Получаем все метаданные трека
            CurrentTrack.Equals(MusicLoader.GetTrackMetadata(currentTrackPath));

            _isMusicInstalled = true;
        }

        _mediaPlayer.Play();
        _timer.Start();

        _playerState = PlayerState.Playing;
        PlayButtonCurrentSymbol = PAUSE_BUTTON_SYMBOL;
    }

    private void PauseMusic()
    {
        _mediaPlayer.Pause();
        _timer.Stop();

        _playerState = PlayerState.AtPause;
        PlayButtonCurrentSymbol = PLAY_BUTTON_SYMBOL;
    }

    private void PreviousTrack()
    {
        if (UITracksList.SelectedIndex == 0) UITracksList.SelectedIndex = _musicList.Count - 1;
        else UITracksList.SelectedIndex--;
    }

    private void NextTrack()
    {
        if (UITracksList.SelectedIndex == _musicList.Count - 1) UITracksList.SelectedIndex = 0;
        else UITracksList.SelectedIndex++;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        // Если трек открыт и не находится в процессе ручного перемещения слайдера:
        if (_mediaPlayer.Source != null && !_isSliderDragging)
        {
            CurrentTrack.PositionInSeconds = _mediaPlayer.Position.TotalSeconds;
            CurrentTrack.Position = _mediaPlayer.Position.ToString("mm\\:ss");

            if (_mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                CurrentTrack.DurationInSeconds = _mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            }
        }
    }

    private void UITracksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int selectedTrackIndex = (sender as ListBox).SelectedIndex;
        string selectedTrackName = _musicList[ selectedTrackIndex ];

        if ( selectedTrackName == null ) { return; };

        _isMusicInstalled = false;

        PlayMusic();
    }

    private void Slider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _isSliderDragging = true;
    }

    private void Slider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        var slider = sender as Slider;
        if (slider == null)
            return;
        _isSliderDragging = false;

        TimeSpan newPosition = TimeSpan.FromSeconds(slider.Value);
        _mediaPlayer.Position = newPosition;
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (_isSliderDragging)
        {
            CurrentTrack.Position = TimeSpan.FromSeconds(((Slider)sender).Value).ToString("mm\\:ss");
        }
    }

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
