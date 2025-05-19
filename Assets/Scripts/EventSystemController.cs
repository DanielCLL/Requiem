using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvenSystemController : MonoBehaviour
{
    public GameObject go_title, go_menuPanel, go_credtisEsc, go_creditsText, pauseMenuUI;

    private GameObject heroKnightGO;
    private bool creditsOn;
    private bool isPaused = false;
    private float timer = 0f;
    private float deathTimer = 2f;
    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
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
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if ((Input.GetKeyDown(KeyCode.Escape) && creditsOn))
                deactivateCredits();
            else if (creditsOn)
            {
                go_creditsText.transform.Translate(Vector2.left * 50f * Time.deltaTime);
                timer -= Time.deltaTime;
                if (timer > 500f) creditsOn = false;
            }
        } else if (SceneManager.GetActiveScene().name == "Game")
        {
            if (heroKnightGO == null)
                heroKnightGO = GameObject.Find("HeroKnight");

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

        void Pause()
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
}
