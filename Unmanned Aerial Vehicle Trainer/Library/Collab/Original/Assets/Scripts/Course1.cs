using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course1 : MonoBehaviour {

    // public Rigidbody path;
    GameObject[] cObjs;
    // Use this for initialization
    void Start ()
    {
        NewMethod();
    }

    private void NewMethod()
    {
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
            print(transform.GetChild(i));
    }

    // Update is called once per frame
    void Update () {



    }
    void OnTriggerStay(Collider col)
    {
       // Debug.Log(col + " Stay ");
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.name.Equals("DronePrefab"))
            Debug.Log( col + " Enter ");
    }
    void OnTriggerExit(Collider col)
    {
        if (col.name.Equals("DronePrefab"))
            Debug.Log(col + " STOP " );
    }
}
