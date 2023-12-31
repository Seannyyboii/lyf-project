using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class BossSpawn : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    public Vector3 originalPosition;

    // new variable to store the newly instantiated boss game object
    private GameObject Boss;
    
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    // new variables to store the distance between the camera and the placement indicator, and the minimum distance between the object and the camera
    private float distance;
    public float minDistance = 2f;

    public GameObject bossText;
    public GameObject bossBar;
    public GameObject healthBars;

    public TextMeshProUGUI hintText;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();  
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        CheckSpawn();
    }

    private void CheckDistance()
    {
        if (placementIndicator != null && placementPoseIsValid)
        {
            distance = Vector3.Distance(Camera.main.transform.position, placementIndicator.transform.position);

            hintText.text = distance.ToString();

            if (distance >= 3.75 && distance <= 4)
            {
                hintText.text = "Tap Anywhere to Spawn the Boss";
            }
            else if(distance < 3.75)
            {
                hintText.text = "Too Close";
            }
            else if(distance > 4)
            {
                hintText.text = "Too Far";
            }
            
        }
    }

    private void CheckSpawn()
    {
        if (distance >= 3.75 && distance <= 4)
        {
            // added extra line "Boss == null" to check if the boss object has been instantiated or not. If the Boss object has been instantiated, it wont instantiate another boss.
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && Boss == null)
            {
                PlaceObject();

                // Hides the hint text
                hintText.enabled = false;
            }
        }
    }

    private void PlaceObject()
    {
        Boss = Instantiate(objectToPlace, placementPose.position, objectToPlace.transform.rotation);
        originalPosition = placementPose.position;
    }

    private void UpdatePlacementIndicator()
    {
        // it will only update the location of the placement indicator if the distance between the cam and indicator is bigger than min distance to prevent any clipping between the object and the camera
        if (placementPoseIsValid && Boss == null)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}