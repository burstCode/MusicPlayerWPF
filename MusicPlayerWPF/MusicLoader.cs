using MusicPlayerWPF.Models;
using System.Drawing;
using System.IO;

namespace MusicPlayerWPF;

public static class MusicLoader
{
    static string[] supportedExtensions = { ".mp3", ".wav" };

    private readonly static string PATH_TO_MUSIC_FOLDER = 
        Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

    public static List<string> GetMusicList()
    {
        List<string> musicList = new();

        var files = Directory.GetFiles( PATH_TO_MUSIC_FOLDER );

        foreach ( var file in files )
        {
            var fileExtension = Path.GetExtension(file);
            
            if ( IsSupportedExtension(fileExtension) )
            {
                musicList.Add( Path.GetFileName(file) );
            }
        }

        return musicList;
    }

    private static bool IsSupportedExtension(string extension)
    {
        foreach ( var supportedExtension in supportedExtensions )
        {
            if ( extension.ToLower() == supportedExtension )
            {
                return true;
            }
        }

        return false;
    }

    public static Track GetTrackMetadata(string path)
    {
        if ( !Path.Exists(path) ) { return new(); }

        var filename = Path.GetFileName(path);

        Track track = new();

        try
        {
            TagLib.File tagFile = TagLib.File.Create(path);

            // Название трека
            if (!string.IsNullOrEmpty(tagFile.Tag.Title))
                track.Title = tagFile.Tag.Title;
            else
                track.Title = Path.GetFileNameWithoutExtension(filename);

            // Описание
            if (!string.IsNullOrEmpty(tagFile.Tag.Comment))
                track.Description = tagFile.Tag.Comment;
            else
                track.Description = "Описание отсутствует";

            // Исполнитель
            if (tagFile.Tag.Performers.Any())
                track.Author = tagFile.Tag.FirstPerformer;
            else
                track.Author = "Неизвестен";
            
            // Обложка
            if (tagFile.Tag.Pictures.Any())
            {
                MemoryStream ms = new MemoryStream(tagFile.Tag.Pictures[0].Data.Data);
                track.Album = Image.FromStream(ms);
            }

            track.Duration = tagFile.Properties.Duration.ToString("mm\\:ss");
        }
        catch { }

        return track;
    }
}
