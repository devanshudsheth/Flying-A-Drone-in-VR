using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

    
    AudioSource audioSource;

    public AudioClip audioClipIntro;
    public AudioClip a1;
    public AudioClip a2;
    public AudioClip a3;
    public AudioClip a4;
    public AudioClip a5;
    public AudioClip a6;
    public AudioClip a7;
    public AudioClip a8;
    public AudioClip a9;
    public AudioClip a10;
    public AudioClip a11;
    public AudioClip a12;
    public AudioClip a13;
    public AudioClip a14;
    public AudioClip a15;

    public Indicator throttle_up_indicator;
    public Indicator throttle_down_indicator;
    public Indicator yaw_CCW_indicator;
    public Indicator yaw_CW_indicator;
    public Indicator pitch_forward_indicator;
    public Indicator pitch_back_indicator;
    public Indicator roll_left_indicator;
    public Indicator roll_right_indicator;
    
    Color[] indicator_diffuse = { Color.white, Color.green };
    Color[] indicator_emissive = { Color.black, Color.green };

    public CommonButton leftGrip;
    public CommonButton rightGrip;


    // Use this for initialization
    void Start () {
        audioSource = this.gameObject.AddComponent<AudioSource>();

        // StartCoroutine(waitingFunction("playIntro"));
        // StartCoroutine(waitingFunction());

        playSounds(a3);

        waitingFunction();

        /*
        if (leftGrip.GetPress() && rightGrip.GetPress())
        {
            Debug.Log("Starting Tutorial");

            waitingFunction();
        }
        */

    }
	
	// Update is called once per frame
	void Update () {
        // enforceControllerInput("st_6", new List<string> { "Throttle", "Elevators" });
    }

    // IEnumerator waitingFunction(string methodName, List<object> args = null)
    // IEnumerator waitingFunction()
    void waitingFunction()
    {

        playIntro();

        // GetType().GetMethod(methodName).Invoke(this, args.ToArray());

        // yield return new WaitForSeconds(3.0f);

        a();
        
        b();
        
        c();

        d();

        e();

        f();

    }

    void playIntro()
    {
        audioSource.clip = audioClipIntro;
        audioSource.PlayDelayed(0.0f);
        Debug.Log("Welcome to the VR UAV Trainer!");

        audioSource.clip = a1;
        audioSource.PlayDelayed(0.0f);
        Debug.Log("Hold the left controller out in front of you. First, take note of the little circle in the center of the touchpad.");
    }

    void a()
    {
        audioSource.clip = a2;
        audioSource.PlayDelayed(0.0f);
        Debug.Log("This is your cursor, and it’s a little visual aid to help you find your thumb on the touchpad. Pressing down with your thumb on the touchpad allows it to accept input, and moving around on the touchpad pushes the virtual joystick, represented by the cursor, in that direction. We’ll refer to this as “tilting” the cursor in that direction.");
    }
    
    void b()
    {
        audioSource.clip = a3;
        audioSource.PlayDelayed(0.0f);
        Debug.Log("Now, take note of the arrows on the left touchpad");

        /*
        throttle_up_indicator.GetComponent<Renderer>().material.SetColor("_Color", indicator_diffuse[1]);
        throttle_up_indicator.GetComponent<Renderer>().material.SetColor("_Emissive", indicator_emissive[1]);

        throttle_down_indicator.GetComponent<Renderer>().material.SetColor("_Color", indicator_diffuse[1]);
        throttle_down_indicator.GetComponent<Renderer>().material.SetColor("_Emissive", indicator_emissive[1]);

        yaw_CCW_indicator.GetComponent<Renderer>().material.SetColor("_Color", indicator_diffuse[1]);
        yaw_CCW_indicator.GetComponent<Renderer>().material.SetColor("_Emissive", indicator_emissive[1]);

        yaw_CW_indicator.GetComponent<Renderer>().material.SetColor("_Color", indicator_diffuse[1]);
        yaw_CW_indicator.GetComponent<Renderer>().material.SetColor("_Emissive", indicator_emissive[1]);
        */

        audioSource.clip = a4;
        audioSource.PlayDelayed(0.0f);

        if (audioSource.isPlaying)
        {
            setRenderer(throttle_up_indicator, 1);
            setRenderer(throttle_down_indicator, 1);
            setRenderer(yaw_CCW_indicator, 1);
            setRenderer(yaw_CW_indicator, 1);
        }

        Debug.Log("These arrows indicate the axes of the left joystick on most remote controllers.");

        
        setRenderer(throttle_up_indicator, 0);
        setRenderer(throttle_down_indicator, 0);
        setRenderer(yaw_CCW_indicator, 0);
        setRenderer(yaw_CW_indicator, 0);


        audioSource.clip = a5;
        audioSource.PlayDelayed(0.0f);

        if (audioSource.isPlaying)
        {
            setRenderer(throttle_up_indicator, 1);
            setRenderer(throttle_down_indicator, 1);
        }

        Debug.Log("See the flashing arrows? These represent the axis that adjusts the drone’s throttle.Tilting towards the arrow facing away from you increases the throttle, while tilting it towards you decreases the throttle.What you are going to do is THROTTLE UP to the flashing cube, then throttle back down.");

        setRenderer(throttle_up_indicator, 0);
        setRenderer(throttle_down_indicator, 0);
    }

    void c()
    {
        audioSource.clip = a6;
        audioSource.PlayDelayed(0.0f);
        Debug.Log("The next axis we’ll let you control is your throttle.");

        audioSource.clip = a7;
        audioSource.PlayDelayed(0.0f);

        if (audioSource.isPlaying)
        {
            setRenderer(yaw_CCW_indicator, 1);
            setRenderer(yaw_CW_indicator, 1);
        }

        Debug.Log("See the flashing arrows? Tilting the cursor to the left and right sides of the touchpad rotates the drone counter-clockwise and clockwise, respectively. Feel free to rotate around in circles to get used to this.");

        setRenderer(yaw_CCW_indicator, 0);
        setRenderer(yaw_CW_indicator, 0);
    }

    void d()
    {
        audioSource.clip = a8;
        audioSource.PlayDelayed(0.0f);
        Debug.Log("Now, take note of the blue arrow on the front of the drone. This is not a feature on real world drones, but for our purposes, we’ll include it. It indicates the drone’s heading, that is, it’s forward direction. Remember that your inputs are applied relative to the drone, not to you. Since you’ve got the drone all turned around, we’d better get you acquainted with the reset button. To reset the drone, hit both triggers at the same time. Try it now!");
    }

    void e()
    {
        audioSource.clip = a9;
        audioSource.PlayDelayed(0.0f);
        Debug.Log("Hold your right controller out in front of you.");

        audioSource.clip = a10;
        audioSource.PlayDelayed(0.0f);

        if (audioSource.isPlaying)
        {
            setRenderer(pitch_forward_indicator, 1);
            setRenderer(pitch_back_indicator, 1);
            setRenderer(roll_left_indicator, 1);
            setRenderer(roll_right_indicator, 1);
        }

        Debug.Log("Note the cursor, then the arrows. As you can see, they’re quite different than the left controller.");

        setRenderer(pitch_forward_indicator, 0);
        setRenderer(pitch_back_indicator, 0);
        setRenderer(roll_left_indicator, 0);
        setRenderer(roll_right_indicator, 0);


        audioSource.clip = a11;
        audioSource.PlayDelayed(0.0f);

        if (audioSource.isPlaying)
        {
            setRenderer(pitch_forward_indicator, 1);
            setRenderer(pitch_back_indicator, 1);
        }

        Debug.Log("Tilting the cursor towards the flashing arrows controls the drone’s pitch, which affects the drone’s forward and backwards movement. Tilting away from you pitches the drone forward, while tilting it towards you tilts it backwards. Tilt forward to the flashing cube, then tilt back to the start position.");

        setRenderer(pitch_forward_indicator, 0);
        setRenderer(pitch_back_indicator, 0);
    }

    void f()
    {
        audioSource.clip = a12;
        audioSource.PlayDelayed(0.0f);

        if (audioSource.isPlaying)
        {
            setRenderer(roll_left_indicator, 1);
            setRenderer(roll_right_indicator, 1);
        }

        Debug.Log("The flashing arrows control the drone’s roll, that is, how far to the left and right the drone is leaning.This translates to sideways movement. Now, roll the drone to the left to the flashing cube, then to the right.");

        setRenderer(roll_left_indicator, 0);
        setRenderer(roll_right_indicator, 0);
    }

    IEnumerator playSounds(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
    }

    void setRenderer(Indicator indicator, int index)
    {
        indicator.GetComponent<Renderer>().material.SetColor("_Color", indicator_diffuse[index]);
        indicator.GetComponent<Renderer>().material.SetColor("_Emissive", indicator_emissive[index]);
    }

    void enforceControllerInput(string stageName, List<string> inputs)
    {
        /*
        float pitch_input = Input.GetAxis("Elevators"); // Up down
        float roll_input = Input.GetAxis("Ailerons"); // left right
        float throttle_input = Input.GetAxis("Throttle"); // w s
        float yaw_input = Input.GetAxis("Rudder"); // a d
        */

        foreach (string input in inputs)
        {
            if (Input.GetAxis(input) == 0)
            {
                Debug.Log("FAILED! Get back in the course.");
            }
        }
    }

}
