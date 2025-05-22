using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MusicPlayerWPF.Models;

public class Track : INotifyPropertyChanged
{
    public Track()
    {
        Id = 0;
        Title = "Имя трека";
        Description = "Описание трека";
        Author = "Автор трека";
        Album = Image.FromFile("Resources/default-music-image.png");
        Position = "00:00";
        PositionInSeconds = 0;
        Duration = "00:00";
        DurationInSeconds = 0;
    }

    private int _id;
    private string _title;
    private string _description;
    private string _author;
    private Image _album;
    private string _position;
    private double _positionInSeconds;
    private string _duration;
    private double _durationInSeconds;

    public int Id
    {
        get { return _id; }
        set
        {
            _id = value;
            OnPropertyChanged("Id");
        }
    }

    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
            OnPropertyChanged("Title");
        }
    }

    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged("Description");
        }
    }

    public string Author
    {
        get { return _author; }
        set
        {
            _author = value;
            OnPropertyChanged("Author");
        }
    }

    public Image Album
    {
        get { return _album; }
        set
        {
            _album = value;
            OnPropertyChanged("Album");
        }
    }

    public string Position
    {
        get { return _position; }
        set
        {
            _position = value;
            OnPropertyChanged("Position");
        }
    }

    public double PositionInSeconds
    {
        get { return _positionInSeconds; }
        set
        {
            if (Math.Abs(_positionInSeconds - value) > 0.1)
            {
                _positionInSeconds = value;
                OnPropertyChanged(nameof(PositionInSeconds));
            }
            OnPropertyChanged("PositionInSeconds");
        }
    }

    public string Duration
    {
        get { return _duration; }
        set
        {
            _duration = value;
            OnPropertyChanged("Duration");
        }
    }

    public double DurationInSeconds
    {
        get { return _durationInSeconds; }
        set
        {
            if (Math.Abs(_durationInSeconds - value) > 0.1)
            {
                _durationInSeconds = value;
                OnPropertyChanged(nameof(Duration));
            }
            OnPropertyChanged("DurationInSeconds");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

    public void Equals(Track other)
    {
        Title = other.Title;
        Description = other.Description;
        Author = other.Author;
        Album = other.Album;
        Duration = other.Duration;
    }
}
