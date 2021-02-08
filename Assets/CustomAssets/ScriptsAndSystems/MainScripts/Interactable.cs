using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    bool interactionInProcess = false;
    bool haveAlternativeInterraction = true;

    public virtual void Interract()
    {
        Debug.Log("Interract with " + transform.name);
    }
    public virtual void InterractAlternatively()
    {
        Debug.Log("Interract alternatively with " + transform.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
