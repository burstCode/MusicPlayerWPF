using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerWPF.Models;

public class Track
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string CoverBase64 { get; set; } = string.Empty;
}
