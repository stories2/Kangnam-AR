using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class csTrackingEvent : MonoBehaviour, ITrackableEventHandler
{
    public GameObject quadObj;
    private TrackableBehaviour trackableBehaviour;

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        switch(newStatus) {
            case TrackableBehaviour.Status.DETECTED:
                Debug.Log("DETECTED " + gameObject.name);
            break;
            case TrackableBehaviour.Status.TRACKED:
                Debug.Log("TRACKED " + gameObject.name);
            break;
            case TrackableBehaviour.Status.EXTENDED_TRACKED:
                Debug.Log("EXTENDED_TRACKED " + gameObject.name);
            break;
            default:
                Debug.Log("Lost " + gameObject.name);
            break;
        }

        if (gameObject.name == "ImageTarget_Untitled" && quadObj != null) {
            if (newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED ||
                newStatus == TrackableBehaviour.Status.DETECTED) {
                    quadObj.GetComponent<VideoPlayer>().Play();
                } else {
                    quadObj.GetComponent<VideoPlayer>().Pause();
                }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (trackableBehaviour) {
            trackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
