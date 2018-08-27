using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodController : MonoBehaviour {
    private float life;
    // Use this for initialization
    void Start () {
        life = 2f;
    }
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;

        if (life < 0f)
        {
            Destroy(gameObject);
        }
    }
}
