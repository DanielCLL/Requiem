using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvenSystemController : MonoBehaviour
{
    public GameObject go_title, go_menuPanel, go_credtisEsc, go_creditsText;

    private bool creditsOn;
    private float timer;
    void Awake()
    {
       DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        creditsOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu")
        {
            SceneManager.LoadScene("MainMenu");
            //Destroy(this.gameObject);
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && creditsOn))
            deactivateCredits();
        else if (creditsOn)
        {
            go_creditsText.transform.Translate(Vector2.left * 50f * Time.deltaTime);
            timer = Time.deltaTime;
            if (timer > 500f) creditsOn = false;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level0");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void activateCredits()
    {
        go_creditsText.transform.position = new Vector2(800.5f, 167.0f);
        creditsOn = true;
        go_title.SetActive(false);
        go_menuPanel.SetActive(false);
        go_creditsText.SetActive(true);
        go_credtisEsc.SetActive(true);
    }
    public void deactivateCredits()
    {
        timer = 0;
        creditsOn = false;
        go_title.SetActive(true);
        go_menuPanel.SetActive(true);
        go_creditsText.SetActive(false);
        go_credtisEsc.SetActive(false);
    }
}
