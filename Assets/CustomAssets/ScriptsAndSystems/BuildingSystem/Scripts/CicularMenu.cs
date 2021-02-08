using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CicularMenu : MonoBehaviour
{
    public List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 Mouseposition;
    private Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    private Vector2 centercirlce = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;

    public int menuItem;
    public int CurMenuItem;
    private int OldMenuItem;

    public BuildingSystem buildsys;

    // Start is called before the first frame update
    void Start()
    {
        menuItem = buttons.Count;
        foreach(MenuButton button in buttons)
        {
            button.sceneimage.color = button.NormalColor;
        }
        CurMenuItem = 0;
        OldMenuItem = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentMenuItem();
        if (Input.GetButtonDown("Fire1"))
            ButtonAction();
    }

    public void GetCurrentMenuItem()
    {
        Mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        toVector2M = new Vector2(Mouseposition.x / Screen.width, Mouseposition.y / Screen.height);

        float angle = (Mathf.Atan2(fromVector2M.y - centercirlce.y, fromVector2M.x - centercirlce.x) - Mathf.Atan2(toVector2M.y - centercirlce.y, toVector2M.x - centercirlce.x)) * Mathf.Rad2Deg;

        if (angle < 0)
            angle += 360;

        CurMenuItem = (int) (angle / (360 / menuItem));

        if(CurMenuItem != OldMenuItem)
        {
            buttons[OldMenuItem].sceneimage.color = buttons[OldMenuItem].NormalColor;
            OldMenuItem = CurMenuItem;
            buttons[CurMenuItem].sceneimage.color = buttons[CurMenuItem].HighlightedColor;
        }
    }

    public void ButtonAction()
    {
        buttons[CurMenuItem].sceneimage.color = buttons[CurMenuItem].PressedColor;
    }
}

[System.Serializable]
public class MenuButton
{
    public string name;
    public Image sceneimage;
    public Color NormalColor = Color.white;
    public Color HighlightedColor = Color.gray;
    public Color PressedColor = Color.gray;
}