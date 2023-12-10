using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AudioProvider
{
    private const string fileLocation = @".\MusicData";
    public IEnumerable<string> GetAudioFiles()
    {
        return Directory.EnumerateFiles(fileLocation, "*.mp3");
    }
}
