using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusUn : MonoBehaviour {
    private Camera cameraMain;
    private float life;
	// Use this for initialization
	void Start () {
        cameraMain = Camera.main;
        life = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;

        Vector3 targetPos = new Vector3(cameraMain.transform.localPosition.x - 6f, cameraMain.transform.localPosition.y + 4f, 0f);

        if (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, Time.deltaTime * 4f);
        }
        
        if(life < 0f)
        {
            if(GameObject.FindGameObjectWithTag("Player") != null)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().setScore(1);
            }
            Destroy(gameObject);
        }
    }
}
