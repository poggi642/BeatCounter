using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace BeatCounter.Model;
[Serializable]
public class Note : IXmlSerializable
{
    int m, s, ms, b;
    bool? sig;

    public Note()
    { }

    public Note(int minute, int second, int milli, int button, bool? significance)
    {
        m = minute;
        s = second;
        ms = milli;
        sig = significance;
        b = button;
    }

    public System.Xml.Schema.XmlSchema GetSchema()
    {
        //GetSchema should always return null
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        //move to the next node. If it's a note, get the data
        if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Note")
        {
            ms = int.Parse(reader.GetAttribute("milliseconds"));
            s = int.Parse(reader.GetAttribute("seconds"));
            m = int.Parse(reader.GetAttribute("minutes"));
            b = int.Parse(reader.GetAttribute("button"));
            string input = reader.GetAttribute("significance");
            bool sn;
            if (bool.TryParse(input, out sn))
            {
                sig = sn;
            }
            else
            {
                sig = null;
            }
            reader.Read();
        }
    }

    public void WriteXml(XmlWriter writer)
    {
        //write values to XML
        writer.WriteAttributeString("milliseconds", ms.ToString());
        writer.WriteAttributeString("seconds", s.ToString());
        writer.WriteAttributeString("minutes", m.ToString());
        writer.WriteAttributeString("significance", !sig.HasValue ? "null" : sig.ToString());
        writer.WriteAttributeString("button", b.ToString());
    }

    public static explicit operator Note(MidiNote n)
    {
        Note ret = new Note();
        ret.b = n.button;
        ret.sig = n.importance;
        long time = n.startTime;

        TimeSpan span = new TimeSpan(0, 0, 0, 0, (int)time);
        ret.m = span.Minutes;
        ret.s = span.Seconds;
        ret.ms = span.Milliseconds;

        return ret;
    }
}
