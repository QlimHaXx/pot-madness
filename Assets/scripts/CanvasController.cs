using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {
    public GameObject text, text2;
    public GameObject bottle, bottle2;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (text.transform.position.x > -390f && text.transform.position.x < 990f) {
            text.transform.Translate(new Vector3(Time.deltaTime * 32f, 0f, 0f));
        }
        if (text2.transform.position.x > -390f && text2.transform.position.x < 990f)
        {
            text2.transform.Translate(new Vector3(-Time.deltaTime * 20f, 0f, 0f));
        }
        if (bottle.transform.position.y > -390f && bottle.transform.position.y < 990f)
        {
            bottle.transform.Translate(new Vector3(0f, -Time.deltaTime * 8f, 0f));
        }
        if (bottle2.transform.position.y > -390f && bottle2.transform.position.y < 990f)
        {
            bottle2.transform.Translate(new Vector3(0f, Time.deltaTime * 16f, 0f));
        }
    }
}
