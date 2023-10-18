using System.Collections;
using System.Collections.Generic;
using Tilia.Interactions.Interactables.Interactables;
using Tilia.Interactions.Interactables.Interactors;
using Tilia.Trackers.PseudoBody;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{

    public GameObject arrowPrefab;
    public GameObject pseudoBody;
    private bool isPseudoBodyActive;

    private void Start()
    {
        StartCoroutine(InitializeAfterSceneLoaded());
    }

    private IEnumerator InitializeAfterSceneLoaded()
    {
        yield return null; // Wait for the end of the frame (scene fully loaded).
        pseudoBody = FindObjectOfType<PseudoBodyFacade>()?.gameObject;
        isPseudoBodyActive = pseudoBody != null && pseudoBody.activeSelf;
        
        Debug.Log("Pseudo Body:"+pseudoBody?.gameObject.tag);
    }
    
    public void OnTriggerEnter(Collider other) {
        CreateArrow(other);
    }

    public void OnTriggerStay(Collider other) {
        CreateArrow(other);
        //this might be spwaning too many arrows...
    }

    void CreateArrow(Collider other) {
        InteractorFacade interactor = other.gameObject.GetComponentInParent<InteractorFacade>();

        if (interactor != null && interactor.GrabAction.IsActivated && interactor.GrabbedObjects.Count == 0) {
            var arrow = Instantiate(arrowPrefab);
            arrow.GetComponent<TogglePseudoBody>()?.Initialize(pseudoBody);
            
            var interactable = arrow.GetComponentInChildren<InteractableFacade>();
            
            interactable.GrabAtEndOfFrame(interactor); //for some reason if we dont wait till end of frame, it sets KinmeaticWhenInactive
        }
    }
    
    // Function to get the state of the "PseudoBody."
    public bool IsPseudoBodyActive()
    {
        return isPseudoBodyActive;
    }
}
