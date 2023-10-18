using System.Collections;
using System.Collections.Generic;
using Tilia.Trackers.PseudoBody;
using UnityEngine;

public class TogglePseudoBody : MonoBehaviour
{
    private GameObject pseudoBody;
    private bool isPseudoBodyActive;
    
    public void Initialize(GameObject pseudoBodyReference)
    {
        pseudoBody = pseudoBodyReference;
    }
    
    // Function to enable the game object with the "PseudoBody" tag.
    public void EnablePseudoBody()
    {
        if (pseudoBody != null)
        {
            pseudoBody.SetActive(true);
            isPseudoBodyActive = true;
        }
    }

    // Function to disable the game object with the "PseudoBody" tag.
    public void DisablePseudoBody()
    {
        if (pseudoBody != null)
        {
            pseudoBody.SetActive(false);
            isPseudoBodyActive = false;
        }
    }
    
    // Function to get the state of the "PseudoBody."
    public bool IsPseudoBodyActive()
    {
        return isPseudoBodyActive;
    }
}
