using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {

    enum gameState{
        tutorial,
        freeroam,
        course

    }
    //reset position and orientation on drone
    private Vector3 drone_start_position;
    private Quaternion drone_start_rotation;

    public GameObject drone;



    public CommonButton leftGrip;
    public CommonButton rightGrip;

    public GameObject course1;
    public GameObject course2;

    public TutorialManager tutorialManager;

    bool playFirst = true;

    gameState currentState;
	// Use this for initialization
	void Start () {

        currentState = gameState.tutorial;
        //initialize beginning and end positions
        drone_start_position = drone.transform.position;
        drone_start_rotation = drone.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {

       
        if(currentState == gameState.tutorial)
        {
            hideCourses(true);

            if (!tutorialManager.isAudioPlaying() && tutorialManager.getCurrentEvent() < 14)
            {
                if (playFirst)
                {
                    tutorialManager.GoToEvent(13);
                    playFirst = false;
                }
                else
                {
                    tutorialManager.PlayNextEvent();
                }
            } 
            
            if (leftGrip.GetPress() && rightGrip.GetPress())
            {
                droneReset(drone);
                currentState = gameState.course;
                print(currentState.ToString());
            }
        }

        else if (currentState == gameState.course)
        {
            hideCourses(false);
            if (leftGrip.GetPress() && rightGrip.GetPress())
            {
                droneReset(drone);
                currentState = gameState.freeroam;
                print(currentState.ToString());
            }
        }
        else
        {
            playFirst = true;
            hideCourses(true);
            if (leftGrip.GetPress() && rightGrip.GetPress())
            {
                droneReset(drone);
                currentState = gameState.tutorial;
                print(currentState.ToString());
            }


        }

    }

    private void droneReset(GameObject droneGameObj)
    {
        droneGameObj.transform.position = drone_start_position;
        droneGameObj.transform.rotation = drone_start_rotation;
        droneGameObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        droneGameObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    void hideCourses(bool arg)
    {
           course1.SetActive(!arg);
           course2.SetActive(!arg);
        
    }
}
