using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUIButton : MonoBehaviour
{
    public GameObject obj;

    public void Close()
    {
        obj.gameObject.SetActive(false);
    }

}
