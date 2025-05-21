using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EventSystemController : MonoBehaviour
{
    public static EventSystemController Instance;

    private GameObject heroKnightGO, go_title, go_menuPanel, go_creditsEsc, go_creditsText, pauseMenuUI;
    private GameObject gameManager;
    private bool creditsOn;
    private bool isPaused = false;
    private float deathTimer = 2f;
    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        /*
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        */

    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        creditsOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (go_title == null) go_title = GameObject.Find("Title");
            if (go_menuPanel == null) go_menuPanel = GameObject.Find("MenuPanel");
            if (go_creditsEsc == null)
            {
                go_creditsEsc = GameObject.Find("Credits_Esc");
                go_creditsEsc.SetActive(false);
            }
            if (go_creditsText == null)
            {
                go_creditsText = GameObject.Find("CreditsText");
                go_creditsText.SetActive(false);
            }

            if ((Input.GetKeyDown(KeyCode.Escape) && creditsOn))
                deactivateCredits();
            else if (creditsOn)
            {
                go_creditsText.transform.Translate(Vector2.up * 50f * Time.deltaTime);
            }
        } else if (SceneManager.GetActiveScene().name == "Game")
        {
            if (heroKnightGO == null) heroKnightGO = GameObject.Find("HeroKnight");
            if (pauseMenuUI == null)
            {
                pauseMenuUI = GameObject.Find("CanvasPausa");
                pauseMenuUI.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                    Resume();
                else
                    Pause();

                // SceneManager.LoadScene("PauseMenu");
                // //Destroy(this.gameObject);
            }

            if (heroKnightGO.GetComponent<HeroKnight>().GetIsDead())
            {
                if (deathTimer > 0)
                    deathTimer -= Time.deltaTime;
                else
                    SceneManager.LoadScene("DeathScene");
            }
        }
        else if (SceneManager.GetActiveScene().name == "DeathScene")
        {

        }
        else if (SceneManager.GetActiveScene().name == "Final")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }

    public void CargarEscenaHabilidades(){
        Debug.Log("Cambia de escena");
         SceneManager.LoadScene("HabilidadesScene");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void activateCredits()
    {
        go_creditsText.transform.position = new Vector2(go_creditsText.transform.position.x, -820f);
        creditsOn = true;
        go_title.SetActive(false);
        go_menuPanel.SetActive(false);
        go_creditsText.SetActive(true);
        go_creditsEsc.SetActive(true);
    }
    public void deactivateCredits()
    {
        //timer = 0;
        creditsOn = false;
        go_title.SetActive(true);
        go_menuPanel.SetActive(true);
        go_creditsText.SetActive(false);
        go_creditsEsc.SetActive(false);
    }
    public void Resume()
    {
        // Ocultamos menú
        pauseMenuUI.SetActive(false);
        // Reanudamos la simulación
        Time.timeScale = 1f;
        // Reactivamos audio (opcional)
        AudioListener.pause = false;
        isPaused = false;
    }

    public void Pause()
    {
        // Congelamos la simulación
        Time.timeScale = 0f;
        // Pausamos audio (opcional)
        AudioListener.pause = true;
        // Activamos menú
        pauseMenuUI.SetActive(true);

        isPaused = true;
    }
        // Funciones públicas que puedes enlazar a botones
    public void OnClickResume()    => Resume();
    public void OnClickQuitGame()  => Application.Quit();

    public void ActivateShield()
    {
        gameManager.GetComponent<GameManager>().UnlockShield();
    }
    public void ActivateDoubleJump()
    {
        gameManager.GetComponent<GameManager>().UnlockDoubleJump();
    }
    public void ActivateRoll()
    {
        gameManager.GetComponent<GameManager>().UnlockRoll();
    }
    public void ActivateFireball()
    {
        gameManager.GetComponent<GameManager>().UnlockFireball();
    }
}
