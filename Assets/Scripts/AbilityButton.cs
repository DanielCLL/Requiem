using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AbilityButton : MonoBehaviour
{
    public string abilityName;         // "Escudo", "Fireball", etc.
    public string nextSceneName;       // nombre de la escena destino

    private Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void Start()
    {
        // Si ya estaba desbloqueada, desactivo el botón
        if (GameManager.Instance.IsUnlocked(abilityName))
            btn.interactable = false;
    }

    void OnClick()
    {
        // Desbloqueo la habilidad
        GameManager.Instance.Unlock(abilityName);

        // Pongo el botón inactivo para esta sesión
        btn.interactable = false;

        // Aquí darías la habilidad a tu personaje, p.e.:
        // Player.Instance.GiveAbility(abilityName);

        // Y cambio de escena
        SceneManager.LoadScene(nextSceneName);
    }
}
