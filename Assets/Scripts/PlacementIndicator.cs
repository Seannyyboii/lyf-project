using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject visual;

    // Start is called before the first frame update
    void Start()
    {
        // raycastManager is set to the new ARRaycastManager Object to be used to check raycasts in the Augmented reality environment.
        raycastManager = FindObjectOfType<ARRaycastManager>();

        // visual (called "Quad" in the hierarchy) is deactivated to hide it if the raycast does not hit anything.
        visual = transform.GetChild(0).gameObject;

        visual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // A raycast is projected from the center of the screen in the forwards direction to check if it hits any surface in the real world.
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // If a hit is detected, the visual/indicator is then activated to show where the objects would be placed if the screen is tapped.
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);
            }
        }
    }
}
