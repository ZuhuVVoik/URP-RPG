using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupDesc : MonoBehaviour
{
    public Camera cam;
    public Text text;

    public PlayerMotor player;

    Ray RayOrigin;
    RaycastHit HitInfo;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        RayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(RayOrigin, out HitInfo, 100f))
        {
            Debug.DrawRay(RayOrigin.direction, HitInfo.point, Color.yellow);
        }


        Interactable obj = HitInfo.transform.gameObject.GetComponent<Interactable>();

        if (obj != null)
        {
            OnInteractionReady(obj);
        }
        else
        {
            text.text = "";
        }
    }


    public void OnInteractionReady(Interactable interactable)
    {
        if (Vector3.Distance(player.transform.position, interactable.transform.position) < interactable.radius)
        {

            GenerateTooltip(interactable);
            

            if (Input.GetButtonDown("Interract"))
            {
                interactable.Interract();
            }
            if (Input.GetButtonDown("InterractAlt"))
            {
                interactable.InterractAlternatively();
            }
        }
        else
        {
            text.text = "";
        }
    }

    public void GenerateTooltip(Interactable interactable)
    {
        ItemPickUp pickup = interactable.GetComponent<ItemPickUp>();
        if (pickup != null)
        {
            GenerateTooltipForItem(pickup.item);
        }

        InteractableObject interactableObject = interactable.GetComponent<InteractableObject>();
        if (interactableObject != null)
        {
            Lootable lootable = interactableObject.GetComponent<Lootable>();

            if(lootable == null)
            {
                GenerateIOTooltip(interactableObject);
            }
            else
            {

            }
        }
    }

    public void GenerateIOTooltip(InteractableObject interactableObject)
    {
        string message = "Использовать ";
        message += interactableObject.name;

        if (interactableObject.canBeRemovedByPlayer)
        {
            message += "\n";
            message += "Разобрать " + interactableObject.name;
        }

        text.text = message;
    }

    public void GenerateTooltipForItem(ItemInstance item)
    {
           
        string message = "Поднять ";

        message += item.item.name;

        if (item.item.stackable)
        {
            message += "(" + item.Count + ")";
        }

        text.text = message;
    }
}
