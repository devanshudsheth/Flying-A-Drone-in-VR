using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course1 : MonoBehaviour
{
    public AudioClip outOfZoneAudio;
    AudioSource audioSource;
    
    Entry entryobj;

    public Affect path1;
    public Affect path2;
    public Affect path3;
    public Affect path4;
    public Affect path5;
    public Affect path6;

    List<Collider> currentTriggers1;
    List<Collider> currentTriggers2;
    List<Collider> currentTriggers3;
    List<Collider> currentTriggers4;
    List<Collider> currentTriggers5;
    List<Collider> currentTriggers6;

    public GameObject Hoop1;
    public GameObject Hoop2;
    public GameObject Hoop3;

    public CommonAxis Throttle_Yaw;
    public CommonAxis Pitch_Roll;

    //reset position and orientation on drone
    private Vector3 drone_start_position;
    private Quaternion drone_start_rotation;

    bool test1;
    bool test2;
    bool test3;
    bool test4;
    bool test5;
    bool test6;

    bool test; 

    public TutorialManager tutorialManagerinstance;

    

    List<GameObject> gs = new List<GameObject>();

    public Collider drone;

    public Material transparent;
    public Material defaultMaterial;


    public Indicator throttle_up_indicator;
    public Indicator throttle_down_indicator;
    public Indicator yaw_CCW_indicator;
    public Indicator yaw_CW_indicator;
    public Indicator pitch_forward_indicator;
    public Indicator pitch_back_indicator;
    public Indicator roll_left_indicator;
    public Indicator roll_right_indicator;

    bool errPlay;
    bool startupPlay;

    public float throttleVal = 0.5f;
    public float rollVal = 0.2f;
    public float yawVal = 0.2f;
    public float pitchVal = 0.2f;



    // Use this for initialization
    void Start()
    {
        

        Transform[] ts = this.GetComponentsInChildren<Transform>();
        entryobj = new Entry();
        audioSource = this.gameObject.AddComponent<AudioSource>();

        //initialize beginning and end positions
        drone_start_position = drone.gameObject.transform.position;
        drone_start_rotation = drone.gameObject.transform.rotation;

        errPlay = true;
        startupPlay = true;
        //audioSource.playOnAwake = false;

        test1 = test6 = test2 = test3 = test4 = test5 =  true;
        test = true;

        if (ts == null)
        {
        }
        foreach (Transform t in ts)
        {
            if (t != null && t.gameObject != null)
            {
                gs.Add(t.gameObject);
            }

        }
        Hoop1.SetActive(true);
        Hoop2.SetActive(false);
        Hoop3.SetActive(false);
        gs.Remove(this.gameObject);

       

    }

    // Update is called once per frame
    void Update()
    {

       
        
        

        currentTriggers1 = path1.ongoingTriggers;
        currentTriggers2 = path2.ongoingTriggers;
        currentTriggers3 = path3.ongoingTriggers;
        currentTriggers4 = path4.ongoingTriggers;
        currentTriggers5 = path5.ongoingTriggers;
        currentTriggers6 = path6.ongoingTriggers;

        if (path6.triggerOngoing && checkDrone(currentTriggers6))
        {
            if(test6)
            {
                tutorialManagerinstance.StopEvent();
                if (!tutorialManagerinstance.isAudioPlaying())
                {
                    tutorialManagerinstance.GoToEvent(17);
                    test6 = false;

                }
            }
            
            errPlay = true;
            // Debug.Log("Press forward and throttle");
          //  audioSource.Stop();
            setPathSettings("st_6", hoop1: false, hoop2: false, hoop3: true);
            enforceControllerInput("st_6", new List<string> { "Throttle", "Elevators" });
        }
        else if (path5.triggerOngoing && checkDrone(currentTriggers5))
        {
            if(test5)
            {
                tutorialManagerinstance.StopEvent();
                if (!tutorialManagerinstance.isAudioPlaying())
                {
                   tutorialManagerinstance.GoToEvent(16);
                    test5 = false;

                }
            }
           

            errPlay = true;
            // Debug.Log("Press throttle upward");
          //  audioSource.Stop();
            setPathSettings("st_5", hoop1: false, hoop2: false, hoop3: true);
            enforceControllerInput("st_5", new List<string> { "Throttle" });
        }
        else if (path4.triggerOngoing && checkDrone(currentTriggers4))
        {
            if(test4)
            {
                tutorialManagerinstance.StopEvent();
                if (!tutorialManagerinstance.isAudioPlaying())
                {
                    tutorialManagerinstance.GoToEvent(17);
                    test4 = false;
                    
                }
            }
            errPlay = true;
            // Debug.Log("Press forward and throttle");
          //  audioSource.Stop();
            setPathSettings("st_4", hoop1: false, hoop3: false, hoop2: true);
            enforceControllerInput("st_4", new List<string> { "Throttle", "Elevators" });
        }
        else if (path3.triggerOngoing && checkDrone(currentTriggers3))
        {
            if (test3)
            {
                tutorialManagerinstance.StopEvent();
                if (!tutorialManagerinstance.isAudioPlaying())
                {
                    tutorialManagerinstance.GoToEvent(16);
                    test3 = false;
                   
                }
            }
            
            errPlay = true;
            // Debug.Log("Press throttle upward");
         //   audioSource.Stop();
            setPathSettings("st_3", hoop1: false, hoop3: false, hoop2: true);
            enforceControllerInput("st_3", new List<string> { "Throttle" });
        }
        else if (path2.triggerOngoing && checkDrone(currentTriggers2))
        {
            if(test2)
            {
                tutorialManagerinstance.StopEvent();
                if (!tutorialManagerinstance.isAudioPlaying())
                {
                    tutorialManagerinstance.GoToEvent(17);
                    test2 = false;
                   
                }
            }
          

            errPlay = true;
         
            setPathSettings("st_2", hoop2: false, hoop3: false, hoop1: true);
            enforceControllerInput("st_4", new List<string> { "Throttle", "Elevators" });
        }
        else if (path1.triggerOngoing && checkDrone(currentTriggers1))
        {
            if (!tutorialManagerinstance.isAudioPlaying() && test)
                {
                    tutorialManagerinstance.GoToEvent(15);
                    test = false;
            }

                if (!tutorialManagerinstance.isAudioPlaying() && test1)
                {
                    tutorialManagerinstance.GoToEvent(16);
                    test1 = false;
                }


            //audioSource.clip = startupAudioClip;
            //audioSource.PlayDelayed(0.0f);


            errPlay = true;
            // Debug.Log("Press throttle upward");
            //audioSource.Stop();
            setPathSettings("st_1", hoop2: false, hoop3: false, hoop1: true);
            enforceControllerInput("st_3", new List<string> { "Throttle" });
        }


        else //out of the path
        {
            if (audioSource.clip != outOfZoneAudio)
            {
                audioSource.volume = 0.01f;
                
                audioSource.clip = outOfZoneAudio;
            }

            if (!audioSource.isPlaying && errPlay)
            {
                audioSource.PlayDelayed(0.0f);
                errPlay = false;
            }
            if (entryobj.enteredHoop1)
            {
                Hoop1.SetActive(false);
                Hoop2.SetActive(true);
                Hoop3.SetActive(false);
            }
            else if (entryobj.enteredHoop2)
            {
                Hoop1.SetActive(false);
                Hoop2.SetActive(false);
                Hoop3.SetActive(true);
            }


            foreach (GameObject gobj in gs)
            {
                gobj.GetComponent<Renderer>().material = defaultMaterial;
                gobj.GetComponent<Renderer>().material.SetColor("_TintColor", Color.red);
            }

        }
    }

    void setPathSettings(string stageName, bool hoop1 = false, bool hoop2 = false, bool hoop3 = false )
    {
        Hoop1.SetActive(hoop1);
        Hoop2.SetActive(hoop2);
        Hoop3.SetActive(hoop3);
        foreach (GameObject gobj in gs)
        {

            if (!gobj.name.Equals(stageName))
                gobj.GetComponent<Renderer>().material = transparent;
            else
            {
                gobj.GetComponent<Renderer>().material = defaultMaterial;
                gobj.GetComponent<Renderer>().material.SetColor("_TintColor", Color.green);
            }
        }
    }

    void enforceControllerInput(string stageName, List<string> inputs)
    {

        // float pitch_input = Input.GetAxis("Elevators"); // Up down
        // float roll_input = Input.GetAxis("Ailerons"); // left right
        // float throttle_input = Input.GetAxis("Throttle"); // w s
        // float yaw_input = Input.GetAxis("Rudder"); // a d
        Vector2 pitch_roll = Vector2.zero;
        Vector2 throttle_yaw = Vector2.zero;

        throttle_yaw = Throttle_Yaw.GetAxis();
        pitch_roll = Pitch_Roll.GetAxis();

        float pitch_input = pitch_roll.y;
        float roll_input = pitch_roll.x;
        float throttle_input = throttle_yaw.y;
        float yaw_input = throttle_yaw.x;

     //   print("pitch = " + pitch_input);
     //   print("roll = " + roll_input);
     //   print("yaw = " + yaw_input);
     //   print("throttle = " + throttle_input);

        if (stageName.Equals("st_1") || stageName.Equals("st_3") || stageName.Equals("st_5") )
            {
                if(Mathf.Abs(pitch_input) > pitchVal || Mathf.Abs(roll_input) > rollVal|| Mathf.Abs(yaw_input) > pitchVal)
                {
               

             //   print("Incorrect Input");

                }
                else
                {
             //       print("ok");
                  
                }


            }
            else if(stageName.Equals("st_2") || stageName.Equals("st_4") ||stageName.Equals("st_6") )
            {
                if(Mathf.Abs(throttle_input) > throttleVal)
                {
             //       print("too much throttle");
                }
                else if (Mathf.Abs(yaw_input) > yawVal || Mathf.Abs(pitch_input) > pitchVal )
                {
                    print("Incorrect Input");

                }
                else
                {
             //       print("ok");
                }
            }
                   
    }

    bool checkDrone(List<Collider> currentTriggers)
    {
        return currentTriggers.Contains(drone);
    }

    private void droneReset(GameObject droneGameObj)
    {
        droneGameObj.transform.position = drone_start_position;
        droneGameObj.transform.rotation = drone_start_rotation;
        droneGameObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        droneGameObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }


   
}
