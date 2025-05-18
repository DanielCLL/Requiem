using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    public Button soundOnButton;  // Referencia al botón SoundOn
    public Button soundOffButton; // Referencia al botón SoundOff
    public AudioSource audioSource;  // Referencia al componente AudioSource donde suena la música

    void Start()
    {
        //SoundOff esta desactivado al inicio
        soundOffButton.gameObject.SetActive(false);

        // Agregar el evento de clic para SoundOn
        soundOnButton.onClick.AddListener(OnSoundOnClicked);
    }

    void OnSoundOnClicked()
    {
        // Desactivar el botón SoundOn
        soundOnButton.gameObject.SetActive(false);

        // Activar el botón SoundOff
        soundOffButton.gameObject.SetActive(true);

        // Detener la música
        audioSource.Stop();

        // Agregar el evento para cuando se haga clic en SoundOff
        soundOffButton.onClick.AddListener(OnSoundOffClicked);
    }

    void OnSoundOffClicked()
    {
        // Activar el botón SoundOn
        soundOnButton.gameObject.SetActive(true);

        // Desactivar el botón SoundOff
        soundOffButton.gameObject.SetActive(false);

        // Reproducir la música nuevamente
        audioSource.Play();

        // Agregar el evento para cuando se haga clic en SoundOn nuevamente
        soundOnButton.onClick.AddListener(OnSoundOnClicked);
    }
}
