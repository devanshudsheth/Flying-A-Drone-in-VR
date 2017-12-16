using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course2 : MonoBehaviour
{
    public TutorialManager tutorialManager;

    public AudioClip outofBoundsAudioClip;
    public AudioClip stage1AudioClip;
    public AudioClip stage2AudioClip;
    public AudioClip stage3AudioClip;
    public AudioClip stage4AudioClip;

    AudioSource audioSource;

    //Affect for path colliders
    public Affect path1;
    public Affect path2;
    public Affect path3;
    public Affect path4;

    public float throttleVal = 0.5f;
    public float rollVal = 0.3f;
    public float yawVal = 0.3f;
    public float pitchVal = 0.3f;

    public CommonAxis Throttle_Yaw;
    public CommonAxis Pitch_Roll;

    //list of colliders colliding with the path stages
    List<Collider> currentTriggers1;
    List<Collider> currentTriggers2;
    List<Collider> currentTriggers3;
    List<Collider> currentTriggers4;

    //List of GameObjects
    List<GameObject> gs = new List<GameObject>();

    //Collider on drone
    public Collider drone;

    //Materials to set
    public Material transparent;
    public Material defaultMaterial;

    //int to control blinking
    int blinkCheck;

    //boolean to flip the direction
    bool flipDirection;

    bool errPlay;
    bool notPlayed;

    bool p1;
    bool p2;
    bool p3;
    bool p4;

    // Use this for initialization
    void Start()
    {
        //get transform array of all children for current GameObject
        Transform[] ts = this.GetComponentsInChildren<Transform>();

        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.01f;
        audioSource.playOnAwake = false;
        if (audioSource.clip != outofBoundsAudioClip)
        {
            audioSource.clip = outofBoundsAudioClip;
        }

        //if not null
        if (ts == null)
        {
        }
        foreach (Transform t in ts)
        {
            //transfer into list of GameObjects, includes the parent and children of this
            if (t != null && t.gameObject != null)
            {
                gs.Add(t.gameObject);
            }

        }

        //intialize direction to false
        flipDirection = false;

        //remove test stage and this, so it only consists of the children stages
        gs.Remove(this.gameObject);
        gs.Remove(GameObject.Find("stagetest"));

        //initialize blinkCheck to 70
        blinkCheck = 70;
        errPlay = true;
        notPlayed = true;

        p1 = p2 = p3 = p4 = true;
    }

    // Update is called once per frame
    void Update()
    {

        //if blinkCheck reaches 0, reset to 70
        if (blinkCheck == 0)
        {
            blinkCheck = 70;
        }
        //otherwise decrement
        blinkCheck--;

        //updates collider lists
        currentTriggers1 = path1.ongoingTriggers;
        currentTriggers2 = path2.ongoingTriggers;
        currentTriggers3 = path3.ongoingTriggers;
        currentTriggers4 = path4.ongoingTriggers;

        //if drone is colliding with path stage 4
        if (path4.triggerOngoing && checkDrone(currentTriggers4))
        {
            if (!tutorialManager.isAudioPlaying() && p4)
            {
                tutorialManager.GoToEvent(21);
                p4 = false;
                p3 = true;
            }
            enforceControllerInput("st1_4");
            errPlay = true;
            audioSource.Stop();
            audioSource.clip = stage4AudioClip;
            audioSource.PlayDelayed(0.0f);
            //flip the directions
            flipDirection = false;
            foreach (GameObject gobj in gs)
            {
                //make all other stages transparent, and current stage green
                if (!gobj.name.Equals("st1_4"))
                {
                    gobj.GetComponent<Renderer>().material = transparent;
                }
                else
                {
                    gobj.GetComponent<Renderer>().material = defaultMaterial;
                    gobj.GetComponent<Renderer>().material.SetColor("_TintColor", Color.green);
                }
            }
            
        }
        //if drone is colliding with path stage 3
        else if (path3.triggerOngoing && checkDrone(currentTriggers3))
        {
            if(!tutorialManager.isAudioPlaying() && p3)
            {
                tutorialManager.GoToEvent(21);
                p3 = false;
                p4 = true;
            }
            enforceControllerInput("st1_3");
            errPlay = true;
            audioSource.Stop();
            audioSource.clip = stage3AudioClip;
            audioSource.PlayDelayed(0.0f);
            //flip the directions
            flipDirection = true;
            foreach (GameObject gobj in gs)
            {
                //make all other stages transparent, and current stage green
                if (!gobj.name.Equals("st1_3"))
                {
                    gobj.GetComponent<Renderer>().material = transparent;
                }
                else
                {
                    gobj.GetComponent<Renderer>().material = defaultMaterial;
                    gobj.GetComponent<Renderer>().material.SetColor("_TintColor", Color.green);
                }
            }
           
        }
        //if drone is colliding with path stage 2
        else if (path2.triggerOngoing && checkDrone(currentTriggers2))
        {
            if (!tutorialManager.isAudioPlaying() && p2)
            {
                tutorialManager.GoToEvent(20);
                p2 = false;
            }
            errPlay = true;
            foreach (GameObject gobj in gs)
            {
                enforceControllerInput("st1_2");
                audioSource.Stop();
                audioSource.clip = stage2AudioClip;
                audioSource.PlayDelayed(0.0f);
                //if current stage, make it green
                //make next stage to travel to, blink yellow
                //makes rest of the stages transparent
                if (!gobj.name.Equals("st1_2"))
                {
                    if (gobj.name.Equals("st1_3") && blinkCheck > 35 && blinkCheck <= 70 && !flipDirection)
                    {

                        gobj.GetComponent<Renderer>().material = defaultMaterial;
                        gobj.GetComponent<Renderer>().material.SetColor("_TintColor", Color.yellow);



                    }
                    else if (gobj.name.Equals("st1_4") && blinkCheck > 35 && blinkCheck <= 70 && flipDirection)
                    {

                        gobj.GetComponent<Renderer>().material = defaultMaterial;
                        gobj.GetComponent<Renderer>().material.SetColor("_TintColor", Color.yellow);



                    }
                    else
                    {

                        gobj.GetComponent<Renderer>().material = transparent;
                    }

                }
                else
                {
                    gobj.GetComponent<Renderer>().material = defaultMaterial;
                    gobj.GetComponent<Renderer>().material.SetColor("_TintColor", Color.green);
                }
            }
            
        }

        //if drone is colliding with path stage 1
        else if (path1.triggerOngoing && checkDrone(currentTriggers1))
        {
            if (!tutorialManager.isAudioPlaying() && p1)
            {
                if(notPlayed)
                {
                    tutorialManager.GoToEvent(18);
                    notPlayed = false;
                } else
                {
                    p1 = false;
                    tutorialManager.GoToEvent(19);
                }
                
            }
            enforceControllerInput("st1_1");
            errPlay = true;
            audioSource.Stop();
            audioSource.clip = stage1AudioClip;
            audioSource.PlayDelayed(0.0f);
            foreach (GameObject gobj in gs)
            {
                //make all other stages transparent, and current stage green
                if (!gobj.name.Equals("st1_1"))
                {
                    gobj.GetComponent<Renderer>().material = transparent;
                }
                else
                {
                    gobj.GetComponent<Renderer>().material = defaultMaterial;
                    gobj.GetComponent<Renderer>().material.SetColor("_TintColor", Color.green);
                }
            }
            
        }

        //if out of bounds, make all stages red
        else
        {
            if (!audioSource.isPlaying && errPlay) {
                errPlay = false;
                audioSource.clip = outofBoundsAudioClip;
                audioSource.PlayDelayed(0.0f);
            }
           
            foreach (GameObject gobj in gs)
            {
                gobj.GetComponent<Renderer>().material = defaultMaterial;
                gobj.GetComponent<Renderer>().material.SetColor("_TintColor", Color.red);
            }

        }
    }

    //st1_1 throttle
    //st1_2 roll min throttle
    //st1_3 yaw min throttle
    //st1_4 yaw min throttle

    void enforceControllerInput(string stageName)
    {

        // float pitch_input = Input.GetAxis("Elevators"); // Up down
        // float roll_input = Input.GetAxis("Ailerons"); // left right
        // float throttle_input = Input.GetAxis("Throttle"); // w s
        //  float yaw_input = Input.GetAxis("Rudder"); // a d

        Vector2 pitch_roll = Vector2.zero;
        Vector2 throttle_yaw = Vector2.zero;

        throttle_yaw = Throttle_Yaw.GetAxis();
        pitch_roll = Pitch_Roll.GetAxis();

        float pitch_input = pitch_roll.y;
        float roll_input = pitch_roll.x;
        float throttle_input = throttle_yaw.y;
        float yaw_input = throttle_yaw.x;

      //  print("pitch = " + pitch_input);
      //  print("roll = " + roll_input);
      //  print("yaw = " + yaw_input);
      //  print("throttle = " + throttle_input);

        if (stageName.Equals("st1_1"))
        {
            if (Mathf.Abs(pitch_input) > pitchVal || Mathf.Abs(roll_input) > rollVal || Mathf.Abs(yaw_input) > yawVal)
            {
                print("Incorrect Input1");

            }
            else
            {
                print("ok");
            }


        }
        else if (stageName.Equals("st1_2"))
        {
            if (Mathf.Abs(pitch_input) > pitchVal || Mathf.Abs(throttle_input) > throttleVal || Mathf.Abs(yaw_input) > yawVal)
            {
                print("Incorrect Input2");

            }
            else
            {
                print("ok");
            }


        }
        else if (stageName.Equals("st1_3") || stageName.Equals("st1_4"))
        {
            if (Mathf.Abs(pitch_input) > pitchVal || Mathf.Abs(roll_input) > rollVal || Mathf.Abs(throttle_input) > throttleVal)
            {
                print("Incorrect Input3");

            }
            else
            {
                print("ok");
            }


        }

    }

    bool checkDrone(List<Collider> currentTriggers)
    {
        return currentTriggers.Contains(drone);
    }

}
