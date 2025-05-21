using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;           // para Graphic
using TMPro;                    // para TextMeshProUGUI

public class AbilityButton : MonoBehaviour
{
    public string abilityName;

    private GameObject gameManager;
    private Button btn;
    private float disabledDarkFactor = 0.5f;
    private Graphic[] allGraphics;     // Image, TextMeshProUGUI, etc.

    void Awake()
    {
        
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        btn = GetComponent<Button>();

        // Cacheamos todos los componentes gráficos hijos para colorearlos luego
        allGraphics = GetComponentsInChildren<Graphic>(true);



        // Si ya estaba desbloqueada, desactivo el botón y lo oscurezco
        if (gameManager.GetComponent<GameManager>().GetIDAbility(abilityName))
        {
            btn.interactable = false;
            SetDisabledVisuals();
        }
    }

    private void SetDisabledVisuals()
    {
        foreach (var g in allGraphics)
        {
            // Multiplica cada canal de color (RGBA) por el factor
            g.color *= disabledDarkFactor;
        }
    }
}