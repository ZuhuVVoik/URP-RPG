using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    public List<Collider> col = new List<Collider>();
    public objectsorts sort;
    public Material green;
    public Material red;
    public bool IsBuildable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
            col.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
            col.Remove(other);
    }

    private void Update()
    {
        Changecolor();
    }

    public void Changecolor()
    {
            if (col.Count == 0)
                IsBuildable = true;
            else
                IsBuildable = false;

        if (IsBuildable)
        {
            foreach (Transform child in this.transform)
            {
                child.GetComponent<Renderer>().material = green;
            }

        }
        else
        {
            foreach (Transform child in this.transform)
            {
                child.GetComponent<Renderer>().material = red;
            }
        }
    }
}

public enum objectsorts
{
    normal
}
