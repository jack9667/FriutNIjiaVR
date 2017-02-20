using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {


    public static Hand Instance;

    public bool isNotShake;
    SteamVR_TrackedObject hand;
    SteamVR_Controller.Device device;

    // Use this for initialization

    void Awake()
    {
        Instance = this;
    }
    void Start () {
        hand = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)hand.index);
        isNotShake = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetHandShaking()
    {
        Debug.Log("GetHandShaking");
        StartCoroutine("Shake", 1.5f);

    }

    IEnumerator Shake(float time)
    {
        Invoke("StopShake", time);
        while (!isNotShake)
        {
            device.TriggerHapticPulse(500);
            yield return new WaitForEndOfFrame();
        }

    }

    private void StopShake()
    {
        isNotShake = true;
    }

    public void StopCoroutine()
    {
        //StopAllCoroutines();

        //Invoke("GetFalse", 0.5f);
        isNotShake = false;
    }

    private void GetFalse()
    {
        isNotShake = false;
    }
}
