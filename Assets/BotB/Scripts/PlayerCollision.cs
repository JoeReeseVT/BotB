using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;  // For ToDictionary

public class PlayerCollision : MonoBehaviour
{
    // The order in which you populate these arrays in the editor doesn't matter
    // because they're going to be converted to Dictionaries when the scene loads.
    //
    // The dictionaries are indexable using the collider's name as a string key.
    // For example, to set one of these colliders to disabled, you'd do:
    //     PlayerCollision.hurtboxes.Item["Name_Of_Collider"].enabled = false;
    public Collider[] hurtboxesArr;
    public Collider[] hitboxesArr;

    public Dictionary<string, Collider> hurtboxes;
    public Dictionary<string, Collider> hitboxes;

    // Start is called before the first frame update
    void Start()
    {
        hurtboxes = new Dictionary<string, Collider>();
        hitboxes = new Dictionary<string, Collider>();

        foreach (Collider c in hurtboxesArr) {
            hurtboxes.Add(c.name, c);
        }
        foreach (Collider c in hitboxesArr) {
            hitboxes.Add(c.name, c);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
