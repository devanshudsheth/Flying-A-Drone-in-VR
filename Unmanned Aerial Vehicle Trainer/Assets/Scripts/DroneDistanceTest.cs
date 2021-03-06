using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * This script is used for checking how far the user holds and two VIVE trackers.
 * Also checks whether they are of a similar orientation.
 * 
 * This is done to check if user is holding the VIVE controllers similar to how a user would hold a drone remote.
 * The script, at this stage, only sends a message on Logger as to whether it is close enough and has the right orientation or not.
 * 
 * This script is written with the view that we will pause the game if the user does not hold the controller the right way.
 * It is beyond the scope of the current Preliminary prototype.
 * 
 * ***/


public class DroneDistanceTest : MonoBehaviour
{



    //The maximum distance the two VIVE trackers can be held at to simulate the holding of a game Controller
    [Tooltip("The maximum distance the two VIVE trackers can be held at")]
    public float threshold = 0.25f;

    //The angle between the two VIVE trackers be at to simulate the holding of a game Controller
    [Tooltip("The maximum angle allowable between the two VIVE trackers' rotation vectors")]
    public float angle = 45.0f;

    // Inspector parameters
    [Tooltip("The Left Controller")]
	public CommonTracker leftTracker;

    // Inspector parameters
    [Tooltip("The Right Controller")]
	public CommonTracker rightTracker;


    // Use this for initialization
    void Start()
    {
        
    }

	void FixedUpdate()
	{
        //the global positions of the two trackers
		Vector3 leftPosition = leftTracker.transform.position;
		Vector3 rightPosition = rightTracker.transform.position;

        //Debug.Log (leftPosition.ToString ());
        //Debug.Log (rightPosition.ToString ());

        //the angle between the rotation vectors of the two remotes
        angle = Quaternion.Angle (leftTracker.transform.rotation, rightTracker.transform.rotation);

		//Debug.Log (angle);
        //if angle > 45 we display the controllers are held in opposite directions
		if (angle >= 45.0f) {
			Debug.Log ("Opposite directions");
		}
        //if orientation is right, look for distance
        else {
			//if distance is greater than set distance display too far
			if (Vector3.Distance (leftPosition, rightPosition) >= threshold) {
				//Debug.Log ("Too far");
            } else {
                //right distance and orientation
				//Debug.Log ("Close enough");
            }
		}
	}

   
    
}
