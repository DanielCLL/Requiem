using UnityEngine;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GUIController GUIController;

    private bool m_shieldAviable, m_doubleJumpAviable, m_rollAviable, m_fireballAviable;

    // Guarda nombres de habilidades desbloqueadas
    //private HashSet<string> unlocked = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            /* Carga de PlayerPrefs las que ya tenía
            foreach (var key in new[] { "Escudo", "Fireball", "DobleSalto", "Deslizar" })
                if (PlayerPrefs.GetInt(key, 0) == 1)
                    unlocked.Add(key);
            */
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        m_shieldAviable = false;
        m_doubleJumpAviable = false;
        m_rollAviable = false;
        m_fireballAviable = false;
    }

    void Update()
    {
        
    }
    /*
    public bool IsUnlocked(string ability)
    {
        return unlocked.Contains(ability);
    }

    public void Unlock(string ability)
    {
        if (unlocked.Add(ability))
            PlayerPrefs.SetInt(ability, 1);
    }
    */
    public void UnlockShield()
    {
        m_shieldAviable = true;
        GUIController.GetEmerald();
    }

    public bool GetShieldAviable()
    {
        return m_shieldAviable;
    }

    public void UnlockDoubleJump()
    {
        m_doubleJumpAviable = true;
        GUIController.GetEmerald();
    }

    public bool GetDoubleJumpAviable()
    {
        return m_doubleJumpAviable;
    }

    public void UnlockRoll()
    {
        m_rollAviable = true;
        GUIController.GetEmerald();
    }

    public bool GetRollAviable()
    {
        return m_rollAviable;
    }

    public void UnlockFireball()
    {
        m_fireballAviable = true;
        GUIController.GetEmerald();
    }

    public bool GetFireballAviable()
    {
        return m_fireballAviable;
    }

    public bool GetIDAbility(string name)
    {
        if (name == "Escudo")
            return GetShieldAviable();
        else if (name == "DobleSalto")
            return GetDoubleJumpAviable();
        else if (name == "Roll")
            return GetRollAviable();
        else if (name == "Fireball")
            return GetFireballAviable();
        else
            return false;
    }
}
