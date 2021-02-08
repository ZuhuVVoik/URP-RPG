using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int TreeHP = 3;
    public int TreeTier = 1;

    public GameObject TreeBase;
    public GameObject TreeRagdoll;

    private void Update()
    {
        CheckHP();
    }

    public void CheckHP()
    {
        if (TreeHP <= 0)
        {
            TreeRagdoll.SetActive(true);
            Destroy(TreeBase);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
