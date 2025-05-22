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
        Clock = "00:00";
        Duration = "00:00";
    }

    private int _id;
    private string _title;
    private string _description;
    private string _author;
    private Image _album;
    private string _clock;
    private string _duration;

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

    public string Clock
    {
        get { return _clock; }
        set
        {
            _clock = value;
            OnPropertyChanged("Clock");
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
