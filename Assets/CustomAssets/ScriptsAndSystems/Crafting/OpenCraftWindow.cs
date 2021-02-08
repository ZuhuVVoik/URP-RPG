using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCraftWindow : MonoBehaviour
{
    public GameObject CraftWindow;

    public void OpenCraft()
    {
        CraftWindow.gameObject.SetActive(true);
    }
}
