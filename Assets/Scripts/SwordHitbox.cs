using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public GameObject swordHitbox;
    // Start is called before the first frame update
    void Start()
    {
        swordHitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHitbox()
    {
        swordHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        swordHitbox.SetActive(false);
    }
}
