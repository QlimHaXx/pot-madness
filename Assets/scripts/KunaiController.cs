﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour {
    private GameObject player;
    private Vector3 direction;
    private float life;
    private CameraShake cameraMain;
    private GameObject sFxManager;
    // Use this for initialization
    void Start()
    {
        sFxManager = GameObject.Find("SFx Manager");
        cameraMain = Camera.main.GetComponent<CameraShake>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.GetComponent<PlayerController>().getVelocity();
        //direction = new Vector3(1f, 0f, 0f);
        life = 3f;
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());

        if(direction.x != 0f && direction.y != 0f)
        {
            if(direction.y < 0f)
            {
                transform.GetChild(0).Rotate(0f, 0f, direction.x * direction.y * -225f);
            }
            else
            {
                transform.GetChild(0).Rotate(0f, 0f, direction.x * direction.y * -45f);
            }
        }
        else
        {
            if(direction.x != 0f && direction.y == 0f)
            {
                transform.GetChild(0).Rotate(0f, 0f, direction.x * -90f);
            }
            if(direction.x == 0f)
            {
                if (direction.y < 0f)
                {
                    transform.GetChild(0).Rotate(0f, 0f, 180f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");

        for(int i = 0; i < clones.Length; i++)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), clones[i].GetComponent<BoxCollider2D>());
        }

        if (life > 0f)
        {
            life -= Time.deltaTime;
            transform.Translate(direction * Time.deltaTime * 12f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            cameraMain.setShouldShake(true);
            other.gameObject.GetComponent<EnemyController>().setLife(40f);
            sFxManager.GetComponent<SFxManager>().player_attack.Play();
            Destroy(gameObject);
        }
    }
}
