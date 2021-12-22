using Bhaptics.Tact.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVest : MonoBehaviour
{
    float timeLaps;
    float timeLapsStart = 2f;
    float maxTimeLaps = 0.3f;
    
    float lastTimeTriggert = 0f;
    Bhaptics_Setup setup;

    public Transform goal;
    public bool playstate = true;
    public float timeLapsDamping = 2f;
    public HapticSource jumpscare;

    // Start is called before the first frame update
    void Start()
    {
        timeLaps = timeLapsStart;
    }
    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if (timeLaps > maxTimeLaps)
        {
            if ((pos.z < 0) && (pos.z > goal.position.z))
            {
                timeLaps = timeLapsStart * (1 - ((pos.z / goal.position.z) / timeLapsDamping));
            }
        }

        if (((Time.time - lastTimeTriggert) > timeLaps)&&(playstate))
        {
            GetComponent<HapticSource>().Play();
            lastTimeTriggert = Time.time;
        }
    }

    public void VestJumpscare()
    {
        jumpscare.Play();
    }
}
