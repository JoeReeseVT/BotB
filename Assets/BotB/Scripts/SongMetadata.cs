using System;
using MidiParser;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Notes;

[CreateAssetMenu(fileName = "Data", menuName = "BotB/SongMetadata", order = 1)]
public class SongMetadata : ScriptableObject
{
    //Player facing strings for the name of the song
    public string songTitle;
    public string songSubtitle;

    //https://docs.unity3d.com/ScriptReference/AudioClip.html
    public AudioClip audioFile;
    public TextAsset songMidi;
    public float latencyComp;

    //Tracks all instances of a given note.
    //Not actually a List, but I couldn't think of a better name. Sorry if BuildNoteChart() is a bit confusing because of that.
    [System.Serializable]
    public struct NoteList
    {
        public int notesCount;
        public int noteID;
        public int ticksPerQuarterNote;
        public int[] notes;
        public int Length()
        {
            return notesCount;
        }
    }

    public NoteList[] noteChart;

    //In Beats Per Minute.
    public float[] Tempos;

    //TODO: Insert any song-specific art assets for song selection

    public static NoteList[] BuildNoteChart(TextAsset midiToRead) {

        //skip all this if we don't have the data
        if (!midiToRead) {
            return new NoteList[1];
        }

        var midiFile = new MidiFile(midiToRead.bytes);
        int ticksPerQuarterNote = midiFile.TicksPerQuarterNote;

        // Note IDs we know each chart has. This prevents things from breaking if, say, a track has a BPM change but no chord changes.
        // At the time of writing, 42 through 52 are empty, but reserving them anyway will prevent issues from arising if we add more 
        // notes, at the cost of a couple dozen bytes of memory.
        int[] hardcodedNoteIDs = new[] { 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 52 };

        List<int> newNoteIDs = new List<int>();
        newNoteIDs.AddRange(hardcodedNoteIDs);

        List<List<int>> newNotes = new List<List<int>>();
        for (int i = 0; i < newNoteIDs.Count; i++) {
            newNotes.Add(new List<int>());
        }

        foreach (var track in midiFile.Tracks)
        {
            foreach (var midiEvent in track.MidiEvents)
            {
                if (midiEvent.MidiEventType == MidiEventType.NoteOn) {
                    if (newNoteIDs.IndexOf(midiEvent.Note) == -1) {
                        newNoteIDs.Add(midiEvent.Note);
                        newNotes.Add(new List<int>());
                    }
                    newNotes[newNoteIDs.IndexOf(midiEvent.Note)].Add(midiEvent.Time);
                }
            }
        }

        NoteList[] newChart = new NoteList[newNoteIDs.Count];

        foreach (var i in newNoteIDs) {
            NoteList currNoteList = new NoteList();
            currNoteList.notesCount = newNotes[newNoteIDs.IndexOf(i)].Count;
            currNoteList.noteID = i - 33;
            currNoteList.ticksPerQuarterNote = ticksPerQuarterNote;
            currNoteList.notes = newNotes[newNoteIDs.IndexOf(i)].ToArray();
            // 33 is the note value of A0, our root note, in all my MIDI exports from Ableton.   -- Leo
            newChart[i - 33] = currNoteList;
        }

        return newChart;

    }
}

//Inserts editor UI element to build note chart from MIDI
//https://stackoverflow.com/questions/48223969/onvaluechanged-in-scriptable-object-unity
#if UNITY_EDITOR

    [CustomEditor(typeof(SongMetadata))]
    class SongMetadataEditor : Editor {
        public override void OnInspectorGUI() {
            SongMetadata songMetadata = (SongMetadata)target;
            
            base.OnInspectorGUI();

             if(GUILayout.Button("Build Note Chart From MIDI", GUILayout.Height(30)))
             {
                songMetadata.noteChart = SongMetadata.BuildNoteChart(songMetadata.songMidi);
                
                //save the asset we just changed
                EditorUtility.SetDirty(songMetadata);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
             }
        }
    }

#endif
