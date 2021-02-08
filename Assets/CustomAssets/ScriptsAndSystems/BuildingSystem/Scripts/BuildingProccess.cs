using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProccess : MonoBehaviour
{
    public GameObject BuildingPrefab;
    public GameObject BuildingPreview;
    public Building inventoryItem;
    public ItemInstance itemInstanceInfo;

    public BuildingProccess player;

    public GameObject flyingBuilding;
    private Vector3 currentpos;
    private Vector3 currentrot;
    public int rotationStepAngle = 30;
    public GameObject currentpreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask layer;

    float offset = 1.0f;
    float gridSize = 1.0f;

    public bool IsBuilding;

    public void OnBuildingProccessStart(Building inventoryItem, ItemInstance itemInstanceInfo)
    {
        this.inventoryItem = inventoryItem;
        this.itemInstanceInfo = itemInstanceInfo;
        this.BuildingPrefab = this.inventoryItem.BuildingPrefab;
        this.BuildingPreview = this.inventoryItem.BuildingPreview;
        this.IsBuilding = true;
        this.currentpreview = this.BuildingPreview;
    }
    public void OnBulidingProcessEnded()
    {
        IsBuilding = false;
        Destroy(flyingBuilding);
        currentpreview = null;
        BuildingPrefab = null;
        BuildingPreview = null;
    }

    void Update()
    {
        if (IsBuilding)
        {
            if (IsBuilding)
                startPreview();


            if (Input.GetButtonDown("Fire1"))
            {
                Build();

                OnBulidingProcessEnded();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnBulidingProcessEnded();
            }
        }
    }


    public void startPreview()
    {
        if (Physics.Raycast(new Vector3(cam.position.x + 1, cam.position.y, cam.position.z), cam.forward, out hit, 10, layer))
        {
            if (hit.transform != this.transform)
            {
                if(flyingBuilding == null)
                {
                    flyingBuilding = Instantiate(currentpreview);
                }
                showPreview(hit);
            }
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
        currentpreview.transform.position = currentpos;
        if (Input.GetButtonDown("Fire2"))
            currentrot += new Vector3(0, rotationStepAngle, 0);
        currentpreview.transform.localEulerAngles = currentrot;

        flyingBuilding.transform.position = currentpreview.transform.position;
        flyingBuilding.transform.localEulerAngles = currentrot;
    }

    public void Build()
    {
        PreviewObject PO = flyingBuilding.GetComponent<PreviewObject>();
        if (PO.IsBuildable)
        {
            Debug.Log("Successfully placed a building");
            GameObject obj = Instantiate(BuildingPrefab, currentpos, Quaternion.Euler(currentrot));

            InteractableObject interactableObject = obj.GetComponent<InteractableObject>();
            if(interactableObject != null)
            {
                interactableObject.OnCreation();
            }

            itemInstanceInfo.Count -= 1;
            if(itemInstanceInfo.Count <= 0)
            {
                OnBulidingProcessEnded();
            }
        }
    }
}
