using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePseudoBody : MonoBehaviour
{
    private GameObject pseudoBody;
    private bool isPseudoBodyActive;

    private void Start()
    {
        // Find the game object with the "PseudoBody" tag at the start.
        pseudoBody = GameObject.FindGameObjectWithTag("PseudoBody");
        isPseudoBodyActive = pseudoBody != null && pseudoBody.activeSelf;
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
