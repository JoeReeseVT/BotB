using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Here, we use a dictionary which stores a string for each key, and the keycode for
//the button.
public class KeyBindManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private GameObject currentKey;

    //"k" and "c" refer to keyboard and controller
    public Text kkick, kdash, kl_punch, kh_punch;
    public Text ckick, cdash, cl_punch, ch_punch;

    
    void Start()
    {
        //initialize the default keys for keyboard
        keys.Add("kkick", KeyCode.L);
        keys.Add("kdash", KeyCode.K);
        keys.Add("kl_punch", KeyCode.I);
        keys.Add("kh_punch", KeyCode.J);

        //Pretty sure I can't use a keyCode here,
        //currently looking for a solution
        /*
        keys.Add("ckick", KeyCode.L);
        keys.Add("cdash", KeyCode.K);
        keys.Add("cl_punch", KeyCode.I);
        keys.Add("ch_punch", KeyCode.J);
        */

        //Setting the default text for the button
        kkick.text = keys["kkick"].ToString();
        kdash.text = keys["kdash"].ToString();
        kl_punch.text = keys["kl_punch"].ToString();
        kh_punch.text = keys["kh_punch"].ToString();
    }

    void Update()
    {

    }

    //Checks to see if a button is pressed. If it is, we wait for a keystroke
    //and change the keyCode and the text for the button
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    //will let OnGUI know when a button is pressed, this is the funciton
    //that is connected to onClick in the inspector
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}
