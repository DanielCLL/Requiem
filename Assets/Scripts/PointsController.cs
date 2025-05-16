using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    public static PointsController Instance;

    public int puntos;
    public TextMeshPro puntosGUI;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        puntos = 0;
        ActualizarPuntos();
    }

    // Update is called once per frame
    void Update()
    {
        puntosGUI.text = "" + puntos;
        Debug.Log(puntos);
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
            puntosGUI.text = "" + puntos;
    }
}
