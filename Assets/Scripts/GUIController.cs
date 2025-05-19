using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance;

    public int puntos;
    public int llaves;
    public TextMeshProUGUI puntosGUI;
    public TextMeshProUGUI nLlavesGUI;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        puntosGUI = GameObject.Find("Puntuacion").GetComponent<TextMeshProUGUI>();
        nLlavesGUI = GameObject.Find("nKeys").GetComponent<TextMeshProUGUI>();
        puntos = 0;
        llaves = 0;
        ActualizarPuntos();
    }

    // Update is called once per frame
    void Update()
    {
        puntosGUI.text = "" + puntos;
        nLlavesGUI.text = "x " + llaves.ToString();
    }

    public void AddPoints(int points)
    {
        puntos += points;
        ActualizarPuntos();
    }

    public void SubPoints(int points)
    {
        puntos = Mathf.Max(0, puntos - points);
        ActualizarPuntos();
    }

    void ActualizarPuntos()
    {
        if (puntosGUI != null)
            puntosGUI.text = puntos.ToString();
    }

    public void GetKey()
    {
        llaves++;
    }

    public int GetKeys()
    {
        return llaves;
    }

    public void UseKey()
    {
        llaves--;
    }
}
