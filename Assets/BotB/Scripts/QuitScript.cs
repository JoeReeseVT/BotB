﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine;

public class QuitScript : MonoBehaviour
{
    public void doquit()
    {
        Application.Quit();
    }
    public void RollCredits() {
        Application.OpenURL("https://battleoftheband.wixsite.com/botb");
    }
}
