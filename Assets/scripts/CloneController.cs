using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour {
    private GameObject[] followTarget;
    private Vector3 targetPos;
    public float moveSpeed;
    private Animator anim;
    private bool playerMoving, playerAttack;
    private Vector2 lastMove;
    public float life;
    private float lifeAuto;
    private GameObject targetToKill, targetTofollow;
    private GameObject player;
    private CameraShake cameraMain;
    private GameObject sFxManager;
    private float animDurationAttack;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        animDurationAttack = 0.15f;
        lifeAuto = 4f;
        anim = GetComponent<Animator>();
        followTarget = GameObject.FindGameObjectsWithTag("Enemy");
        playerAttack = false;
        playerMoving = false;
        cameraMain = Camera.main.GetComponent<CameraShake>();
        sFxManager = GameObject.Find("SFx Manager");
    }

    // Update is called once per frame
    void Update() {
        followTarget = GameObject.FindGameObjectsWithTag("Enemy");
        lifeAuto -= Time.deltaTime;
        playerAttack = false;
        playerMoving = false;

        if (!player.GetComponent<PlayerController>().getIsDead())
        {
            if (animDurationAttack > 0f)
            {
                animDurationAttack -= Time.deltaTime;
            }

            if (lifeAuto < 3.7f)
            {
                if (followTarget != null)
                {
                    for (int i = 0; i < followTarget.Length; i++)
                    {
                        targetPos = new Vector3(followTarget[i].transform.position.x, followTarget[i].transform.position.y, followTarget[i].transform.position.z);

                        if (transform.position.x + 4f > targetPos.x && transform.position.x - 4f < targetPos.x)
                        {
                            if (transform.position.y + 4f > targetPos.y && transform.position.y - 4f < targetPos.y)
                            {
                                targetTofollow = followTarget[i];
                            }
                        }
                    }
                }

                if (targetTofollow != null)
                {
                    targetPos = new Vector3(targetTofollow.transform.position.x, targetTofollow.transform.position.y, targetTofollow.transform.position.z);
                    transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
                    playerMoving = true;

                    float x = 0f;
                    float y = 0f;

                    if (transform.position.x - targetTofollow.transform.position.x < 0f)
                    {
                        x = transform.position.x / targetTofollow.transform.position.x;
                    }
                    else
                    {
                        x = -transform.position.x / targetTofollow.transform.position.x;
                    }

                    if (transform.position.y - targetTofollow.transform.position.y < 0f)
                    {
                        y = transform.position.y / targetTofollow.transform.position.y;
                    }
                    else
                    {
                        y = -transform.position.y / targetTofollow.transform.position.y;
                    }

                    lastMove = new Vector2(x, y);
                }
            }

            if (lifeAuto < 0f || life <= 0f)
            {
                Destroy(gameObject);
            }

            if (targetToKill != null)
            {
                playerAttack = true;
                cameraMain.setShouldShake(true);
                targetToKill.GetComponent<EnemyController>().setLife(0.8f);
            }
            if (animDurationAttack < 0f)
            {
                playerAttack = false;
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
        if (other.gameObject.tag == "Enemy")
        {
            sFxManager.GetComponent<SFxManager>().player_attack_loop.Play();
            targetToKill = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
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
