using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Notes;
using TMPro;

public class NoteDebug : MonoBehaviour
{
    private Conductor conductor;
    TextMeshProUGUI text;
    
    //Enum.GetName is wierdly expensive when you call it a bunch
    string[] noteNames;
    
    
    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor").GetComponent(typeof(Conductor)) as Conductor;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        noteNames = new string[10];
        for (int noteType = 0; noteType < noteNames.Length; noteType++) {
            noteNames[noteType] = Enum.GetName(typeof(Notes), noteType);
        }

    }

    // Update is called once per frame
    void Update()
    {
        string newText = "Note info for this frame:\n";
        for (int noteType = 0; noteType < noteNames.Length; noteType++)
        {

            if (conductor.song.noteChart[noteType].notes.Length != 0 && Math.Abs(conductor.nextNoteDist(noteType)) <= .05)
            {
                newText = newText + noteNames[noteType] + "\n";
            }
        }
        text.text = newText;
    }
}
