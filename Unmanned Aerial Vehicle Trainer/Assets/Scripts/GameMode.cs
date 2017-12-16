﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {

    enum gameState{
        tutorial,
        freeroam,
        course

    }

    public CommonButton leftGrip;
    public CommonButton rightGrip;

    public GameObject course1;
    public GameObject course2;
    public GameObject obstacles;

    public TutorialManager tutorialManager;

    bool playFirst = true;

    gameState currentState;
	// Use this for initialization
	void Start () {

        currentState = gameState.tutorial;
        
    }
	
	// Update is called once per frame
	void Update () {

       
        if(currentState == gameState.tutorial)
        {
            hideCourses(true);
            obstacles.SetActive(false);

            if (!tutorialManager.isAudioPlaying() && tutorialManager.getCurrentEvent() < 14)
            {
                if (playFirst)
                {
                    tutorialManager.GoToEvent(0);
                    playFirst = false;
                }
                else
                {
                    tutorialManager.PlayNextEvent();
                }
            } 
            
            if (leftGrip.GetPress() && rightGrip.GetPress())
            {
                currentState = gameState.course;
                print(currentState.ToString());
            }
        }

        else if (currentState == gameState.course)
        {
            hideCourses(false);
            obstacles.SetActive(false);

            if (leftGrip.GetPress() && rightGrip.GetPress())
            {
                currentState = gameState.freeroam;
                print(currentState.ToString());
            }
        }
        else
        {

            hideCourses(true);
            obstacles.SetActive(true);

            if (leftGrip.GetPress() && rightGrip.GetPress())
            {
                currentState = gameState.tutorial;
                print(currentState.ToString());
            }


        }

    }

    void hideCourses(bool arg)
    {
           course1.SetActive(!arg);
           course2.SetActive(!arg);
        
    }
}
