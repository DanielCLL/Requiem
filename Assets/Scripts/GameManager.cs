using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Guarda nombres de habilidades desbloqueadas
    private HashSet<string> unlocked = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Carga de PlayerPrefs las que ya tenía
            foreach (var key in new[] { "Escudo", "Fireball", "ExtraCorazon", "DobleSalto", "Deslizar" })
                if (PlayerPrefs.GetInt(key, 0) == 1)
                    unlocked.Add(key);
        }
        else Destroy(gameObject);
    }

    public bool IsUnlocked(string ability)
    {
        return unlocked.Contains(ability);
    }

    public void Unlock(string ability)
    {
        if (unlocked.Add(ability))
            PlayerPrefs.SetInt(ability, 1);
    }
}
