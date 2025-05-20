using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MusicPlayerWPF.Models;

public class Track : INotifyPropertyChanged
{
    public Track()
    {
        Id = 0;
        Title = "Имя трека";
        Description = "Описание трека";
        Author = "Автор трека";
        Album = "";
        Duration = new( new TimeSpan(0, 0, 0) );
    }

    private int _id;
    private string _title;
    private string _description;
    private string _author;
    private string _album;
    private Duration _duration;

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

    public string Album
    {
        get { return _album; }
        set
        {
            _album = value;
            OnPropertyChanged("Album");
        }
    }

    public Duration Duration
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
}
