using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float moveSpeed;
    // private Rigidbody2D myRigidBody;
    private Animator anim;
    private bool playerMoving, playerAttack;
    private Vector2 lastMove;
    public GameObject followTarget, targetToKill, plusUn;
    // private GameObject[] targets;
    private Vector3 targetPos;
    private GameObject sFxManager;
    public float life;
    private CameraShake cameraMain;
    private float animDurationAttack;
    // Use this for initialization
    void Start () {
        animDurationAttack = 0.15f;
        anim = GetComponent<Animator>();
        // myRigidBody = GetComponent<Rigidbody2D>();
        // targets = GameObject.FindGameObjectsWithTag("Clone");
        playerAttack = false;
        playerMoving = false;
        cameraMain = Camera.main.GetComponent<CameraShake>();
        sFxManager = GameObject.Find("SFx Manager");
    }

    // Update is called once per frame
    void Update()
    {
        playerAttack = false;
        playerMoving = false;

        if (!followTarget.GetComponent<PlayerController>().getIsDead()) { 

            if (animDurationAttack > 0f)
            {
                animDurationAttack -= Time.deltaTime;
            }

            targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, followTarget.transform.position.z);

            if (transform.position.x + 4f > targetPos.x && transform.position.x - 4f < targetPos.x)
            {
                if (transform.position.y + 4f > targetPos.y && transform.position.y - 4f < targetPos.y)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
                    playerMoving = true;

                    float x = 0f;
                    float y = 0f;

                    if (transform.position.x - followTarget.transform.position.x < 0f)
                    {
                        x = transform.position.x / followTarget.transform.position.x;
                    }
                    else
                    {
                        x = -transform.position.x / followTarget.transform.position.x;
                    }

                    if (transform.position.y - followTarget.transform.position.y < 0f)
                    {
                        y = transform.position.y / followTarget.transform.position.y;
                    }
                    else
                    {
                        y = -transform.position.y / followTarget.transform.position.y;
                    }
                    //sFxManager.GetComponent<SFxManager>().player_walk.Play();
                    lastMove = new Vector2(x, y);
                }
                else
                {
                    //sFxManager.GetComponent<SFxManager>().player_walk.Stop();
                }
            }

            if (targetToKill != null)
            {
                playerAttack = true;
                cameraMain.setShouldShake(true);

                if (targetToKill.tag == "Player")
                {
                    targetToKill.GetComponent<PlayerController>().setLife(1f);
                }
                if (targetToKill.tag == "Clone")
                {
                    targetToKill.GetComponent<CloneController>().setLife(1f);
                }
                animDurationAttack = 0.15f;
            }

            if (animDurationAttack < 0f)
            {
                playerAttack = false;
            }

            if (life <= 0f)
            {
                //followTarget.GetComponent<PlayerController>().setScore(1);
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0f);
                Instantiate(plusUn, spawnPosition, gameObject.transform.rotation);
                Destroy(gameObject);
            }

            anim.SetFloat("MoveX", lastMove.x);
            anim.SetFloat("MoveY", lastMove.y);
            anim.SetBool("PlayerMoving", playerMoving);
            anim.SetBool("PlayerAttack", playerAttack);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
        }
        else
        {
            anim.SetBool("PlayerMoving", false);
            anim.SetBool("PlayerAttack", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Clone" || other.gameObject.tag == "Player")
        {
            sFxManager.GetComponent<SFxManager>().player_attack_loop.Play();
            targetToKill = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Clone" || other.gameObject.tag == "Player")
        {
            sFxManager.GetComponent<SFxManager>().player_attack_loop.Stop();
            targetToKill = null;
        }
    }

    public void setLife(float amount)
    {
        life -= amount;
    }
}
