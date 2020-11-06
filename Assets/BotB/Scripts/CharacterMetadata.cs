using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "BotB/CharMetadata", order = 1)]
public class CharacterMetadata : ScriptableObject
{
    //Player facing strings for the character select screen
    public string charName;
    public string charDescription;

    public Mesh charMesh;
    public Material charMat;

    //If we have character specific audio barks/exertion noises, we'll need to reference whatever data container we do for that here:
    
    //If there are character specific animations, we'll also reference that here:
}

//Inserts editor UI element to build note chart from MIDI
//https://stackoverflow.com/questions/48223969/onvaluechanged-in-scriptable-object-unity
/*
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
*/
