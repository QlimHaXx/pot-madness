using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFxManager : MonoBehaviour {
    public AudioSource fast_depl;
    public AudioSource player_walk;
    public AudioSource player_attack;
    public AudioSource player_attack_loop;
    public AudioSource player_attack_vide;
    public AudioSource rasen_shuriken;
    public AudioSource mana_potion;
    public AudioSource wasted;
    public AudioSource shooted;
    private bool sdxManagerExists;
    // Use this for initialization
    void Start () {
        if (!sdxManagerExists)
        {
            sdxManagerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
