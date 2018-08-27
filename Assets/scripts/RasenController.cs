﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasenController : MonoBehaviour {
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
        life = 4f;
        Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        cameraMain.setShouldShake(true);

        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");

        for (int i = 0; i < clones.Length; i++)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), clones[i].GetComponent<BoxCollider2D>());
        }

        if (life > 0f)
        {
            life -= Time.deltaTime;
            transform.GetChild(0).Rotate(0f, 0f, Time.deltaTime * 1400f);
            transform.Translate(direction * Time.deltaTime * 14f);
            cameraMain.setPower(life / 3f);
        }
        else
        {
            cameraMain.setPower(0.3f);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            cameraMain.setShouldShake(true);
            other.gameObject.GetComponent<EnemyController>().setLife(200f);
            sFxManager.GetComponent<SFxManager>().player_attack.Play();
        }
    }
}