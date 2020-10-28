using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    //BPM of the song
    public float songBpm;

    //seconds for each song beat
    public float secPerBeat;

    //Current position in the song (seconds)
    public float songPosition;

    //current position in the song (beats)
    public float songPosInBeats;

    //Number of seconds passed since the song started
    public float dspSongTime;

    //array of the different notes of the song. Unsure if this
    //will need to be imported manually or thru a MIDI file
    public float[] notes;

    //object that will play the music
    public AudioSource musicSource;

    //keeps track of where we are in the notes array
    private int nextIndex;


    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();

        nextIndex = 0;
    }


    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPosInBeats = songPosition / secPerBeat;
    }
}
