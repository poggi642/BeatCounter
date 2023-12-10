using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatCounter.Model;
public struct MidiNote
{
    public int duration;
    public int frequency;
    public long startTime;
    public int volume;
    public int button;
    public bool? importance;//null = low,false=mid,true=high
    public MidiNote(int duration, double frequency, long startTime, int volume)
    {
        this.duration = duration;
        this.frequency = (int)frequency;
        this.startTime = startTime;
        this.volume = volume;
        button = 0;
        importance = null;
    }
    public override string ToString()
    {
        return "@" + startTime + "-" + (startTime + duration) + ":" + frequency;
    }
}
