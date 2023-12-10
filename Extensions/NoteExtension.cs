using BeatCounter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatCounter.Extensions;
public static class NoteExtension
{
    public static void ButtonSignificance(this MidiNote note, int minFreq, int maxFreq, int minVel, int maxVel, int minLen, int maxLen, bool sortVol)
    {
        //divide the frequencies into five steps
        float btnStep = (maxFreq - minFreq) / 5.0f;

        float button1Min = minFreq;
        float button2Min = button1Min + btnStep;
        float button3Min = button2Min + btnStep;
        float button4Min = button3Min + btnStep;
        float button5Min = button4Min + btnStep;
        //based off of the note frequency, get the button it needs to be
        if (note.frequency >= button1Min && note.frequency < button2Min) note.button = 0;
        if (note.frequency >= button2Min && note.frequency < button3Min) note.button = 1;
        if (note.frequency >= button3Min && note.frequency < button4Min) note.button = 2;
        if (note.frequency >= button4Min && note.frequency < button5Min) note.button = 3;
        if (note.frequency >= button5Min && note.frequency <= maxFreq) note.button = 4;

        if (sortVol)
        {
            //if sorting by volume, split volume into three steps and get importance
            float vStep = (maxVel - minVel) / 3.0f;
            float v1 = minVel;
            float v2 = v1 + vStep;
            float v3 = v2 + vStep;
            if (note.volume >= v1 && note.volume < v2) note.importance = null;
            if (note.volume >= v2 && note.volume < v3) note.importance = false;
            if (note.volume >= v3 && note.volume <= maxVel) note.importance = true;
        }
        else
        {
            //if sorting by duration, split duration into three steps and get importance
            float lStep = (maxLen - minLen) / 3.0f;
            float l1 = minLen;
            float l2 = l1 + lStep;
            float l3 = l2 + lStep;
            if (note.duration >= l1 && note.duration < l2) note.importance = null;
            if (note.duration >= l2 && note.duration < l3) note.importance = false;
            if (note.duration >= l3 && note.duration <= maxLen) note.importance = true;
        }
    }
}
