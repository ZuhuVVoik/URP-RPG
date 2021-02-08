using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingSystem : MonoBehaviour
{
    public List<buildObjects> objects = new List<buildObjects>();
    public buildObjects currentObject;
    private Vector3 currentpos;
    private Vector3 currentrot;
    public int rotationStepAngle = 30;
    public Transform currentpreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask layer;

    public float offset = 1.0f;
    public float gridSize = 1.0f;

    public bool IsBuilding = false;

    public GameObject objectMenuobj;

    private bool chooseobj;

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (IsBuilding)
        {
            if (IsBuilding && !chooseobj)
                startPreview();
            if (Input.GetButtonDown("Fire1") && !chooseobj)
                Build();
        }
    }


    public void startPreview()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, 10, layer))
        {
            if (hit.transform != this.transform)
                showPreview(hit);
        }
    }

    public void showPreview(RaycastHit hit2)
    {
        currentpos = hit2.point;
        currentpos -= Vector3.one * offset;
        currentpos /= gridSize;
        currentpos = new Vector3(Mathf.Round(currentpos.x), Mathf.Round(currentpos.y), Mathf.Round(currentpos.z));
        currentpos *= gridSize;
        currentpos += Vector3.one * offset;
        currentpreview.position = currentpos;
        if (Input.GetButtonDown("Fire2"))
            currentrot += new Vector3(0, rotationStepAngle, 0);
        currentpreview.localEulerAngles = currentrot;
    }

    public void Build()
    {
        PreviewObject PO = currentpreview.GetComponent<PreviewObject>();
        if (PO.IsBuildable)
        {
            Instantiate(currentObject.prefab, currentpos, Quaternion.Euler(currentrot));
        }
    }

    private void Instantiate()
    {
        throw new NotImplementedException();
    }
}

[System.Serializable]
public class buildObjects
{
    public string name;
    public GameObject prefab;
    public GameObject preview;
    public int gold;
}