using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using static Notes;

public class Conductor : MonoBehaviour
{
    bool isPaused;

    public float currentBeatsPerMinute;
    double currentSecondsPerBeat;

    // What our actual position is in the audio track
    double positionInSeconds;
    double positionInMeasures;
    double positionInBeats;

    float[] timeOfLast;
    float[] timeOfNext;

    //the song we are playing
    public SongMetadata song;

    //Tells us which song we are going to play
    //static int songint;

    //component that will play the music
    private AudioSource musicSource;

    //how many BPM switches we've been through, and how many seconds we were at on the last.
    private int BPMSwitchCount;
    private double timeAtLastBPMSwitch;
    
    //keeps track of where we are in each NoteList, by the last note of that type played's index in song.NoteList[noteType]
    private int[] noteListPos;

    private double dspSongTime;

    public void Start()
    {
        isPaused = true;
        musicSource = gameObject.AddComponent<AudioSource>();
        song = ChooseSong.choosesong;
        loadSong(song);
    }

    public void loadSong(SongMetadata newSong)
    {
        song = newSong;
        noteListPos = new int[song.noteChart.Length];  // noteChart is array of noteList, which is a struct
        timeOfLast = new float[song.noteChart.Length]; // These 'new's all init to 0
        timeOfNext = new float[song.noteChart.Length];
        BPMSwitchCount = 0;
        currentBeatsPerMinute = song.Tempos[BPMSwitchCount];
        currentSecondsPerBeat = 60d/currentBeatsPerMinute;
        
        musicSource.clip = song.audioFile;
        musicSource.loop = true;

        musicSource.Play();
        timeAtLastBPMSwitch = CurrentTime();
        isPaused = false;

        dspSongTime = (float)AudioSettings.dspTime;
    }

    private void updatePositions() {
        //works
        positionInSeconds = CurrentTime();

        for ( int noteType = 0; noteType < noteListPos.Length; noteType++ )
        {

            // If our positionInSeconds has passed the time of the current note we're on, we need to fetch the next note time
            double notePosSec = notePosInSeconds(noteType, noteListPos[noteType]);
            if (song.noteChart[noteType].notes.Length!=0 && nextNoteDist(noteType) <= 0) {
                // Move our "cursor" to the next note in the sequence for this noteType
                noteListPos[noteType] = nextNote(noteType);
                //Debug.Log("Passed " + Enum.GetName(typeof(Notes), noteType));
                //Debug.Log("Going to next " + Enum.GetName(typeof(Notes), noteType) + " at time " + positionInSeconds + " sec (notePos " + notePosSec + " sec");
            }
        }
        //positionInMeasures = 
        //positionInBeats = 
    }

    int nextNote(int noteType) {
        // Make sure we don't go out of bounds
        if (noteListPos[noteType] + 1 < song.noteChart[noteType].notes.Length)
        {
            //Debug.Log("New note pos is " + (noteListPos[noteType] + 1));
            return noteListPos[noteType] + 1;
        }
        else {
            //Debug.Log("New note pos is 0 (RESET)");
            return 0;
        }
    }

    public float nearestNoteDist(int noteType) {
        float last = lastNoteDist(noteType);
        float next = nextNoteDist(noteType);
        if (Mathf.Abs(last) < next)
        {
            return last;
        }
        else
        {
            return next;
        }
    }

    public float lastNoteDist(int noteType) {
        return distanceToNote(noteType, noteListPos[noteType]-1);
    }

    public float nextNoteDist(int noteType) {
        return distanceToNote(noteType, noteListPos[noteType]);
    }

    public float distanceToNote(int noteType, int noteCount) {
        if (song.noteChart[noteType].notes.Length == 0 || song.noteChart[noteType].notes.Length - 1 < noteCount || noteCount <= -1)
        {
            return -1f;
        }
        else {
            return (float)(notePosInSeconds(noteType, noteCount) - positionInSeconds);
        }
    }

    public double notePosInSeconds(int noteType, int noteCount)
    {
        //TODO: rewrite this so it plays nice with BPM changes
        if (noteType < song.noteChart.Length && noteCount < song.noteChart[noteType].notes.Length)
        {
            double outValue = ((double)song.noteChart[noteType].notes[noteCount] * currentSecondsPerBeat) / ((double)song.noteChart[noteType].ticksPerQuarterNote );
            return outValue;
        }
        else {
            return -1;
        }
    }

    public void pauseSong() {
        musicSource.Pause();
        //TODO: verify this works, send out an event for pausing, update the whole dspTime thing
    }

    void Update()
    {
        if (!isPaused) {
            updatePositions();
        }        
    }

    private void BPMChange() {
        BPMSwitchCount++;
        currentBeatsPerMinute = song.Tempos[BPMSwitchCount];
        currentSecondsPerBeat = 60f/currentBeatsPerMinute;
        timeAtLastBPMSwitch = CurrentTime();
    }

    private double CurrentTime() {
        return AudioSettings.dspTime - dspSongTime - song.latencyComp;
    }
}
