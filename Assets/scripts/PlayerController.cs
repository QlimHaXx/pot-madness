using DigitalRuby.SimpleLUT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moverSpeed;
    private float moveSpeedDefault;
    //private Rigidbody2D myRigidBody;
    private Animator anim;
    private bool playerMoving, playerAttack;
    private Vector2 lastMove;
    public GameObject clone, wood;
    public GameObject[] shurikens;
    public GameObject rasen;
    private GameObject target;
    private GameObject sFxManager;
    private CameraShake cameraShake;
    public float life, mana;
    public VirtualJoystick joyStick;
    public VirtualControls controlsButtons;
    private float maxLife, maxMana;
    private float shooter;
    private float animDurationAttack, animShuriken;
    private bool isDead, throwRasen, jump;
    private int score;
    private float time;
    private Vector3 velocity;
    // Use this for initialization
    void Start () {
        jump = false;
        velocity = new Vector3(1f, 1f, 0f);
        score = 0;
        time = 0f;
        isDead = false;
        animDurationAttack = 0.15f;
        maxLife = life;
        maxMana = mana;
        anim = GetComponent<Animator>();
        //myRigidBody = GetComponent<Rigidbody2D>();
        moveSpeedDefault = moverSpeed;
        playerAttack = false;
        playerMoving = false;
        cameraShake = Camera.main.GetComponent<CameraShake>();
        sFxManager = GameObject.Find("SFx Manager");
        throwRasen = false;
    }
	
	// Update is called once per frame
	void Update () {
        playerMoving = false;

        if(animDurationAttack > 0f)
        {
            animDurationAttack -= Time.deltaTime;
        }

        if (shooter > 0f) // high
        {
            shooter -= Time.deltaTime;
            cameraShake.GetComponent<SimpleLUT>().Hue += Time.deltaTime * 20f;
            transform.Translate(new Vector3(Time.deltaTime * moverSpeed, Time.deltaTime * moverSpeed, 0f));
            transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * 10f));
            sFxManager.GetComponent<SFxManager>().player_walk.Stop();
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");

            for (int i = 0; i < clones.Length; i++)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), clones[i].GetComponent<BoxCollider2D>(), false);
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), enemies[i].GetComponent<BoxCollider2D>(), false);
            }

            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Map").GetComponent<PolygonCollider2D>(), false);

            sFxManager.GetComponent<SFxManager>().shooted.Stop();
            cameraShake.GetComponent<SimpleLUT>().Hue = 0f;
            //myRigidBody.mass = 10f;
            transform.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            moverSpeed = moveSpeedDefault;

            if (mana > 0f)
            {
                if (mana >= 19.5f) { // clone
                    if (controlsButtons.getA())
                    {
                        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0f);
                        Instantiate(clone, spawnPosition, gameObject.transform.rotation);
                        Instantiate(clone, spawnPosition, gameObject.transform.rotation);
                        Instantiate(clone, spawnPosition, gameObject.transform.rotation);
                        mana -= 20f;
                    }
                }
                if (mana >= 4.5f) // saut
                {
                    if (controlsButtons.getZ())
                    {
                        int nbTronc = 1;
                        //if (Input.GetButton("Horizontal"))
                        if (joyStick.InputDirection.x != 0)
                        {
                            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0f);
                            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moverSpeed, 0f, 0f));
                            transform.Translate(new Vector3(joyStick.InputDirection.x * moverSpeed, 0f, 0f));
                            if (nbTronc != 0)
                            {
                                Instantiate(wood, spawnPosition, gameObject.transform.rotation);
                                nbTronc--;
                            }
                            sFxManager.GetComponent<SFxManager>().fast_depl.Play();
                            jump = true;
                        }
                        if (joyStick.InputDirection.z != 0)
                        {
                            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0f);
                            //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moverSpeed, 0f));
                            transform.Translate(new Vector3(0f, joyStick.InputDirection.z * moverSpeed, 0f));
                            if (nbTronc != 0)
                            {
                                Instantiate(wood, spawnPosition, gameObject.transform.rotation);
                                nbTronc--;
                            }
                            sFxManager.GetComponent<SFxManager>().fast_depl.Play();
                            jump = true;
                        }
                        if (jump)
                        {
                            mana -= 5f;
                        }
                    }
                }
                if (mana >= 4.5f)
                {
                    if (controlsButtons.getE()) // shurikens
                    {
                        sFxManager.GetComponent<SFxManager>().player_attack_vide.Play();
                        Vector3 spawnPosition = new Vector3(transform.position.x + velocity.x, transform.position.y + velocity.y, 0f);
                        Instantiate(shurikens[Random.Range(0, shurikens.Length)], spawnPosition, gameObject.transform.rotation);
                        mana -= 5f;
                    }
                }
                if (mana >= 94.5f) // rasen shuriken
                {
                    if (controlsButtons.getR())
                    {
                        sFxManager.GetComponent<SFxManager>().rasen_shuriken.Play();
                        animShuriken = 2f;
                        throwRasen = true;
                    }
                }
            }

            if (throwRasen)
            {
                if (animShuriken < 0f)
                {
                    Vector3 spawnPosition = new Vector3(transform.position.x + velocity.x, transform.position.y + velocity.y, 0f);
                    Instantiate(rasen, spawnPosition, gameObject.transform.rotation);
                    throwRasen = false;
                }
                else
                {
                    animShuriken -= Time.deltaTime;
                    mana -= 0.78f;
                }
            }

            //if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            if (joyStick.InputDirection.x != 0)
            {
                //myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moverSpeed, myRigidBody.velocity.y);
                transform.Translate(new Vector3(joyStick.InputDirection.x * moverSpeed * Time.deltaTime, 0f, 0f));

                playerMoving = true;

                if (joyStick.InputDirection.x < 0)
                {
                    lastMove = new Vector2(-1f, 0f);
                }
                else
                {
                    lastMove = new Vector2(1f, 0f);
                }
                
            }
            //if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            if (joyStick.InputDirection.z != 0)
            {
                //myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * moverSpeed);
                //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moverSpeed * Time.deltaTime, 0f));
                transform.Translate(new Vector3(0f, joyStick.InputDirection.z * moverSpeed * Time.deltaTime, 0f));

                playerMoving = true;

                if(joyStick.InputDirection.z < 0)
                {
                    lastMove = new Vector2(0f, -1f);
                }
                else
                {
                    lastMove = new Vector2(0f, 1f);
                }
                
            }
            if (joyStick.InputDirection.x == 0)
            {
                //myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
                transform.Translate(new Vector3(0f, 0f, 0f));
            }
            if (joyStick.InputDirection.z == 0)
            {
                //myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0f);
                transform.Translate(new Vector3(0f, 0f, 0f));
            }
            if (joyStick.InputDirection.x != 0 || joyStick.InputDirection.z != 0)
            {
                //sFxManager.GetComponent<SFxManager>().player_walk.Play();
            }
            if (joyStick.InputDirection.x == 0 || joyStick.InputDirection.z == 0)
            {
                //sFxManager.GetComponent<SFxManager>().player_walk.Stop();
            }

            if (joyStick.InputDirection.x != 0 || joyStick.InputDirection.z != 0)
            {
                velocity = new Vector3(joyStick.InputDirection.x, joyStick.InputDirection.z, 0f);
            }
            else
            {
                velocity = new Vector3(lastMove.x, lastMove.y, 0f);
            }
            /*
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f &&
                Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
            {
                moverSpeed = moveSpeedDefault / 2f;
            }
            else
            {
                moverSpeed = moveSpeedDefault;
            }
            */

            if (controlsButtons.getS()) // attack
            {
                playerAttack = true;
                animDurationAttack = 0.15f;

                if (target != null)
                {
                    cameraShake.setShouldShake(true);
                    target.GetComponent<EnemyController>().setLife(4f);
                    sFxManager.GetComponent<SFxManager>().player_attack.Play();
                }
                else
                {
                    sFxManager.GetComponent<SFxManager>().player_attack_vide.Play();
                }
            }
        }

        if (animDurationAttack < 0f)
        {
            /*if(target != null)
            {
                sFxManager.GetComponent<SFxManager>().player_attack.Play();
            }
            else
            {
                sFxManager.GetComponent<SFxManager>().player_attack_vide.Play();
            }*/

            playerAttack = false;
        }

        if (life <= 5f)
        {
            sFxManager.GetComponent<SFxManager>().player_walk.Stop();
            sFxManager.GetComponent<SFxManager>().wasted.Play();
            isDead = true;
            gameObject.SetActive(false);
        }
        else
        {
            time += Time.deltaTime;
            anim.SetFloat("MoveX", joyStick.InputDirection.x);
            anim.SetFloat("MoveY", joyStick.InputDirection.z);
            anim.SetBool("PlayerMoving", playerMoving);
            anim.SetBool("PlayerAttack", playerAttack);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = other.gameObject;
        }
        if (other.gameObject.tag == "Mana")
        {
            mana = 100f;
            sFxManager.GetComponent<SFxManager>().mana_potion.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Canna")
        {
            shooter = 10f;
            moverSpeed = 2f;
            sFxManager.GetComponent<SFxManager>().shooted.Play();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");

            for (int i = 0; i < clones.Length; i++)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), clones[i].GetComponent<BoxCollider2D>());
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), enemies[i].GetComponent<BoxCollider2D>());
            }
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Map").GetComponent<PolygonCollider2D>());
            //myRigidBody.mass = 0.1f;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = null;
        }
    }

    public void setLife(float amount)
    {
        life -= amount;
    }

    public float getMaxLife()
    {
        return maxLife;
    }

    public float getLife()
    {
        return life;
    }

    public float getMaxMana()
    {
        return maxMana;
    }

    public float getMana()
    {
        return mana;
    }

    public bool getIsDead()
    {
        return isDead;
    }

    public void setScore(int scoreIn)
    {
        score += scoreIn;
    }

    public int getScore()
    {
        return score;
    }

    public float getTime()
    {
        return time;
    }

    public Vector3 getVelocity()
    {
        return velocity;
    }
}
