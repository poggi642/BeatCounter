using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NAudio;
using NAudio.Midi;
using BeatCounter.Extensions;
using System.Xml;
using System.Xml.Serialization;
using BeatCounter.Model;

namespace BeatCounter.Services;
public class AudioAnalyzer
{
    static double[] midi = new double[127];

    public List<Note> Notes { get; set; } = new();
    public AudioAnalyzer()
    {

    }

    public void TrackBeats()
    {
        GetFrequencies();

        string fileName = GetFilename();

        MidiFile file = new MidiFile(fileName);
        Console.WriteLine("Here are some possible tracks for you to choose from:");
        for (int i = 0; i < file.Tracks; i++)
        {
            string instrument = "";
            var events = file.Events.GetTrackEvents(i);
            foreach (var x in events)
            {
                if (x is PatchChangeEvent)
                {
                    instrument = (x as PatchChangeEvent).ToString().Split(':')[1].Replace(" " + x.Channel + " ", "");
                    break;
                }
            }
            if (!string.IsNullOrWhiteSpace(instrument)) Console.WriteLine(i + ": " + instrument);
        }

        int trackN = GetTrackNumber();

        string sortType = GetSortType();

        string output = getOutputPath();

        var trackevents = file.Events.GetTrackEvents(trackN);
        //this is the track 0 will have tempo information
        var tempoGetter = file.Events.GetTrackEvents(0);
        List<MidiNote> notes = new List<MidiNote>();
        int tempo = 0;
        foreach (var e in tempoGetter)
        {
            //get the tempo and drop out of that track
            if (e is TempoEvent)
            {
                //tempo in milliseconds
                tempo = (e as TempoEvent).MicrosecondsPerQuarterNote / 1000;
                break;
            }
        }
        for (int i = 0; i < trackevents.Count; i++)
        {
            //for every note
            MidiEvent e = trackevents[i];
            //if it's a note turning ON
            if (e is NoteOnEvent)
            {
                //the note on event, contains the time, volume, and the length of note
                var On = e as NoteOnEvent;
                //the note event, contains the midi note number (pitch)
                var n = e as NoteEvent;
                //the absolute time (in delta ticks) over the delta ticks per quarter note times the number of milliseconds per quarter note = time in milliseconds
                notes.Add(new MidiNote(On.NoteLength, midi[n.NoteNumber], (long)(On.AbsoluteTime / (float)file.DeltaTicksPerQuarterNote * tempo), On.Velocity));
            }
        }
        //uses known values to get unknown values needed for a guitar hero clone
        //get the min and max frequency
        notes.Sort((n, n2) => n.frequency.CompareTo(n2.frequency));
        int minFreq = notes.First().frequency;
        int maxFreq = notes.Last().frequency;

        //get the min and max volume
        notes.Sort((n, n2) => n.volume.CompareTo(n2.volume));
        int minVol = notes.First().volume;
        int maxVol = notes.Last().volume;

        //get the min and max note duration
        notes.Sort((n, n2) => n.duration.CompareTo(n2.duration));
        int minLen = notes.First().duration;
        int maxLen = notes.Last().duration;

        //sort by time
        notes.Sort((n, n2) => n.startTime.CompareTo(n2.startTime));

        //outputs the song data to {output}.song
        //List<Note> Notes = new List<Note>();
        foreach (MidiNote n in notes)
        {
            MidiNote N = n;
            //gets unknown values for button and importance based off of known values
            N.ButtonSignificance(minFreq, maxFreq, minVol, maxVol, minLen, maxLen, sortType == "v");
            Notes.Add((Note)N);
        }
        //serialize to XML document
        XmlTextWriter w = new XmlTextWriter(output, null);
        XmlSerializer serializer = new XmlSerializer(typeof(List<Note>));
        serializer.Serialize(w, Notes);
        w.Close();
        Console.WriteLine("done");
        Console.ReadKey();
    }

    private string GetFilename()
    {
        Console.Write("Path to MIDI file (relative or absolute): ");
        string fileName = @".\Resources\Raw\Khiva_FeelItOut.mp3";
        if (!File.Exists(fileName))
        {
            Console.WriteLine("That file does not exist.");
            return GetFilename();
        }
        return fileName;
    }

    private int GetTrackNumber()
    {
        Console.Write("Melody Track #: ");
        string track = Console.ReadLine();
        int trackN;
        if (!int.TryParse(track, out trackN))
        {
            Console.WriteLine(track + " is not a valid number.");
            return GetTrackNumber();
        }
        return trackN;
    }

    private string GetSortType()
    {
        Console.Write("Sort importance by [(d)uration or (v)olume]: ");
        string sortType = Console.ReadLine();
        if (sortType != "d" && sortType != "v")
        {
            Console.WriteLine("Invalid sort type " + sortType);
            return GetSortType();
        }
        return sortType;
    }

    static string getOutputPath()
    {
        Console.Write("Path to output (*.song): ");
        string output = Console.ReadLine() + ".song";
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            if (output.Contains(c))
            {
                Console.WriteLine("The character '" + c + "' is not allowed in file names.");
                return getOutputPath();
            }
        }
        return output;
    }

    static void GetFrequencies()
    {
        //get frequencies for midi notes at 440 tuning (piano)
        int a = 440;
        for (int i = 0; i < 127; i++)
        {
            midi[i] = a / 32 * Math.Pow(2, (i - 9) / 12);
        }
    }
}
