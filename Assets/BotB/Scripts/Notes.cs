using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Notes
{
    MeasureStart,
    Beat,
    DrumKick,
    DrumSnare,
    DrumHat,
    DrumCrash,
    ChordChange,
    LightingChange,
    BPMChange

    // If the length of this gets super long (>20) , there are some hardcoded values that will need to be changed in SongMetadata.BuildNoteChart
}