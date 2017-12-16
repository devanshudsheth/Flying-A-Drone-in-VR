using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnmannedAerialVehicleTrainer.Drone;

public enum E_Indicator
{
    E_THROTTLE_UP = 0,
    E_THROTTLE_DOWN = 1,
    E_YAW_CW = 2,
    E_YAW_CCW = 3,
    E_PITCH_FORWARD = 4,
    E_PITCH_BACKWARD = 5,
    E_ROLL_LEFT = 6,
    E_ROLL_RIGHT = 7,
    E_LEFT_CURSOR = 8,
    E_RIGHT_CURSOR = 9,
    E_NONE = 10
}
[System.Serializable]
public struct TutorialEvent
{
    public AudioClip sound_file;        // Audio cue to play 
    public List<E_Indicator> indicators; // Indicators to flash
    public int times_to_flash;           // Number of times to flash the given indicator.
}
public class TutorialManager : MonoBehaviour {
    private Color[] indicator_diffuse = { Color.white, Color.green };
    private Color[] indicator_emissive = { Color.black, Color.green };
    private const float FLASH_TIME = .5f;   //Flash should be 2 times per second.
    private int event_index = 0;
    public DroneController drone;
    public Transform goal_pointer;  //Points towards next goal.
    public Indicator Throttle_Up;
    public Indicator Throttle_Down;
    public Indicator Rudder_CCW;
    public Indicator Rudder_CW;
    public Indicator Pitch_Forward;
    public Indicator Pitch_Backward;
    public Indicator Roll_Left;
    public Indicator Roll_Right;
    public Indicator Left_Cursor;
    public Indicator Right_Cursor;
    private IEnumerator coroutine;  //The coroutine.
    private AudioSource sound_source; 
    public List<TutorialEvent> events;
    private bool isPlaying = false;
    /* 0: Throttle up
     * 1: Throttle down
     * 2: Yaw CCW
     * 3: Yaw CW
     * 4: Pitch Forward 
     * 5: Pitch Backward
     * 6: Roll Left
     * 7: Roll Right
     * 8: Left Cursor
     * 9: Right Cursor
     */
    public Renderer[] indicator_renderers = new Renderer[10];
    public bool[] indicator_states = new bool[10];
    public void ToNextEvent()
    {
        this.event_index--;
        if(this.event_index < 0) { this.event_index = 0; }
    }
    public void ToPreviousEvent()
    {
        this.event_index++;
        if (this.event_index > this.events.Count-1) { this.event_index = this.events.Count-1; }
    }
    public void PlayNextEvent()
    {
        ToNextEvent();
        coroutine = PlayEvent(events[this.event_index]);
        if(!isPlaying) StartCoroutine(coroutine);
    }
    public void PlayPreviousEvent()
    {
        ToPreviousEvent();
        coroutine = PlayEvent(events[this.event_index]);
        if (!isPlaying) StartCoroutine(coroutine);
    }
    public void PlayCurrentEvent()
    {
        coroutine = PlayEvent(events[this.event_index]);
        if (!isPlaying) StartCoroutine(coroutine);
    }
    public void StopEvent()
    {
        isPlaying = false;
        sound_source.Stop();
        StopCoroutine(coroutine);
    }
    public void GoToEvent(int index)
    {
        event_index = index;
        coroutine = PlayEvent(events[this.event_index]);
        if (!isPlaying) StartCoroutine(coroutine);
    }
	// Use this for initialization
	void Start () {
        indicator_renderers[0] = Throttle_Up.GetComponent<Renderer>();
        indicator_renderers[1] = Throttle_Down.GetComponent<Renderer>();
        indicator_renderers[2] = Rudder_CCW.GetComponent<Renderer>();
        indicator_renderers[3] = Rudder_CW.GetComponent<Renderer>();
        indicator_renderers[4] = Pitch_Forward.GetComponent<Renderer>();
        indicator_renderers[5] = Pitch_Backward.GetComponent<Renderer>();
        indicator_renderers[6] = Roll_Left.GetComponent<Renderer>();
        indicator_renderers[7] = Roll_Right.GetComponent<Renderer>();
        indicator_renderers[8] = Left_Cursor.GetComponent<Renderer>();
        indicator_renderers[9] = Right_Cursor.GetComponent<Renderer>();
        this.sound_source = gameObject.AddComponent<AudioSource>();
        PlayCurrentEvent();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator PlayEvent(TutorialEvent evnt)
    {
        isPlaying = true;
        float audio_length = 0f;
        if(evnt.sound_file)
        {
            this.sound_source.clip = evnt.sound_file;
            audio_length = this.sound_source.clip.length;
            this.sound_source.Play();
        }
        int num_times_to_flash = evnt.times_to_flash;
        audio_length -= ((float)num_times_to_flash * FLASH_TIME);
        if (audio_length <= 0f)
        {
            audio_length = 5f;
        }
        for (int i = 0; i < num_times_to_flash; ++i)
        {
            foreach(E_Indicator cator in evnt.indicators)
            {
                Toggle_Indicator(cator);
            }
            yield return new WaitForSeconds(FLASH_TIME);
        }
        Reset_Indicators();
        isPlaying = false;
        yield return new WaitForSeconds(audio_length);
    }
    
    private void Activate_Indicator(E_Indicator indicator_code)
    {
        int indicator_index = (int)indicator_code;
        if (indicator_index >= 10) return;
        indicator_states[indicator_index] = true;
        indicator_renderers[indicator_index].material.SetColor("_Color", indicator_diffuse[1]);
        indicator_renderers[indicator_index].material.SetColor("_Emissive", indicator_emissive[1]);
    }
    private void Deactivate_Indicator(E_Indicator indicator_code)
    {
        int indicator_index = (int)indicator_code;
        if (indicator_index >= 10) return;
        indicator_states[indicator_index] = false;
        int i_state = indicator_states[indicator_index] ? 1 : 0;
        indicator_renderers[indicator_index].material.SetColor("_Color", indicator_diffuse[0]);
        indicator_renderers[indicator_index].material.SetColor("_Emissive", indicator_emissive[0]);
    }
    private void Toggle_Indicator(E_Indicator indicator_code)
    {
        int indicator_index = (int)indicator_code;
        if (indicator_index >= 10) return;
        indicator_states[indicator_index] = !indicator_states[indicator_index];
        int i_state = indicator_states[indicator_index] ? 1 : 0;
        indicator_renderers[indicator_index].material.SetColor("_Color", indicator_diffuse[i_state]);
        indicator_renderers[indicator_index].material.SetColor("_Emissive", indicator_emissive[i_state]);
    }
    private void Reset_Indicators()
    {
       for(int i = 0; i < 10; ++i)
        {
            indicator_states[i] = false;
            indicator_renderers[i].material.SetColor("_Color", indicator_diffuse[0]);
            indicator_renderers[i].material.SetColor("_Emissive", indicator_emissive[0]);
        }
    }


    public bool isAudioPlaying()
    {
        return sound_source.isPlaying;
    }
    
}
