using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance;

    private int lifePoints;
    private int emeralds;
    private int puntos;
    private int llaves;

    private TextMeshProUGUI puntosGUI;
    private TextMeshProUGUI nLlavesGUI;
    private TextMeshProUGUI vidaGUI;
    private TextMeshProUGUI esmeraldasGUI;

    private GameObject heroKnightGO;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        lifePoints = 100;
        puntos = 0;
        llaves = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (puntosGUI == null) puntosGUI = GameObject.Find("Monedas").GetComponent<TextMeshProUGUI>();
            if (nLlavesGUI == null) nLlavesGUI = GameObject.Find("nKeys").GetComponent<TextMeshProUGUI>();
            if (vidaGUI == null) vidaGUI = GameObject.Find("Life").GetComponent<TextMeshProUGUI>();
            if (esmeraldasGUI == null) esmeraldasGUI = GameObject.Find("NumExp").GetComponent<TextMeshProUGUI>();
            if (heroKnightGO == null) heroKnightGO = GameObject.Find("HeroKnight");
            puntosGUI.text = puntos.ToString();
            nLlavesGUI.text = llaves.ToString() + " x";
            vidaGUI.text = Mathf.Max(0, lifePoints).ToString();
            esmeraldasGUI.text = emeralds.ToString();
        }
    }

    public void AddPoints(int points)
    {
        puntos += points;
    }

    public void SubPoints(int points)
    {
        puntos = Mathf.Max(0, puntos - points);
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

    public void ResetKeys()
    {
        llaves = 0;
    }

    public void UpdateLife(int l)
    {
        lifePoints -= l;
    }

    public int GetLifePoints()
    {
        return lifePoints;
    }
    public void ResetLife()
    {
        lifePoints = 100;
    }

    public void GetEmerald()
    {
        emeralds++;
    }

    public int GetEmeralds()
    {
        return emeralds;
    }
}
