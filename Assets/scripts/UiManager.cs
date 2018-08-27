using DigitalRuby.SimpleLUT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour {
    public Slider healthBar, manaBar;
    public Text hpText, manaText;
    public GameObject player;
    public GameObject gameui, endgameui, scoreui, text, star1, star2, star3, time, score, timeUI, scoreUI;
    private float durationAnim;
    private Camera cameraMain;

    // Use this for initialization
    void Start () {
        durationAnim = 10f;
        cameraMain = Camera.main;

        healthBar.maxValue = player.GetComponent<PlayerController>().getMaxLife();
        healthBar.value = player.GetComponent<PlayerController>().getLife();
        int currentHpValue = (int)(healthBar.value / healthBar.maxValue * 100);
        hpText.text = currentHpValue + " %";

        manaBar.maxValue = player.GetComponent<PlayerController>().getMaxMana();
        manaBar.value = player.GetComponent<PlayerController>().getMana();
        int currentManaValue = (int)(manaBar.value / manaBar.maxValue * 100);
        manaText.text = currentManaValue + " %";
    }
	
	// Update is called once per frame
	void Update () {
        float playerTimeUI = Mathf.Round(player.GetComponent<PlayerController>().getTime());
        int playerScoreUI = player.GetComponent<PlayerController>().getScore();

        timeUI.GetComponent<TextMeshProUGUI>().text = "Time : " + playerTimeUI;
        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score : " + playerScoreUI;

        healthBar.maxValue = player.GetComponent<PlayerController>().getMaxLife();
        healthBar.value = player.GetComponent<PlayerController>().getLife();
        int currentHpValue = (int)(healthBar.value / healthBar.maxValue * 100);
        hpText.text = currentHpValue + " %";

        manaBar.maxValue = player.GetComponent<PlayerController>().getMaxMana();
        manaBar.value = player.GetComponent<PlayerController>().getMana();
        int currentManaValue = (int)(manaBar.value / manaBar.maxValue * 100);
        manaText.text = currentManaValue + " %";

        if (currentManaValue < 50)
        {
            manaText.color = new Color(1f, 1f, 1f);
        }
        else
        {
            manaText.color = new Color(0f, 0f, 0f);
        }
        if (currentHpValue < 46)
        {
            hpText.color = new Color(1f, 1f, 1f);
        }
        else
        {
            hpText.color = new Color(0f, 0f, 0f);
        }

        if (player.GetComponent<PlayerController>().getIsDead())
        {
            gameui.SetActive(false);
            endgameui.SetActive(true);
            cameraMain.GetComponent<SimpleLUT>().Amount = 0.9f;

            float playerTime = Mathf.Round(player.GetComponent<PlayerController>().getTime());
            int playerScore = player.GetComponent<PlayerController>().getScore();

            time.GetComponent<TextMeshProUGUI>().text = "Time : " + playerTime;
            score.GetComponent<TextMeshProUGUI>().text = "Score : " + playerScore;

            if (durationAnim > 0f)
            {
                durationAnim -= Time.deltaTime;
            }

            if (durationAnim <= 7.6f)
            {
                text.SetActive(true);
            }

            if(durationAnim < 4f) {
                endgameui.SetActive(false);
                cameraMain.GetComponent<SimpleLUT>().Amount = 0.9f;
                scoreui.SetActive(true);
            }
            
            if (playerScore > 3)
            {
                if (playerTime < 50f)
                {
                    if (durationAnim < 3f)
                    {
                        star1.SetActive(true);
                    }
                }
            }
            
            if (playerScore > 7)
            {
                if (playerTime < 70f)
                {
                    if (durationAnim < 3f)
                    {
                        star1.SetActive(true);
                    }
                    if (durationAnim < 2.5f)
                    {
                        star2.SetActive(true);
                    }
                }
            }
            
            if (playerScore > 15)
            {
                if (playerTime < 70f)
                {
                    if (durationAnim < 3f)
                    {
                        star1.SetActive(true);
                    }
                    if (durationAnim < 2.5f)
                    {
                        star2.SetActive(true);
                    }
                    if (durationAnim < 2f)
                    {
                        star3.SetActive(true);
                    }
                }
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
