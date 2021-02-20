using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject Player;

    public void KillPlayer()
    {
        Debug.Log("Player killed");
    }
}
