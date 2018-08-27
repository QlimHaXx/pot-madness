using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public float power;
    public float duration;
    private Transform cameraMain;
    public float slowDownAmount;
    public bool shouldShake = false;

    private Vector3 startPosition;
    private float initialDuration;
	// Use this for initialization
	void Start () {
        cameraMain = Camera.main.transform;
        startPosition = cameraMain.localPosition;
        startPosition.z = -10f;
        initialDuration = duration;
	}
	
	// Update is called once per frame
	void Update () {
        startPosition = cameraMain.localPosition;
        if (shouldShake)
        {
            if(duration > 0f)
            {
                Vector3 randomPos = startPosition + Random.insideUnitSphere * power;
                randomPos.z = -10f;
                cameraMain.localPosition = randomPos;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                cameraMain.localPosition = startPosition;
            }
        }
	}

    public void setShouldShake(bool b)
    {
        shouldShake = b;
    }

    public void setPower(float pow)
    {
        power = pow;
    }
}
