using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public Vector2 normalisedMousePosition;
    public float currentAngle;
    public int selection;
    private int previousSelection;
    public float elementSelected;
    public bool imhere;

    public GameObject[] menuItems;

    public GameObject fire;
    public GameObject water;
    public GameObject plant;
    MenuItemScript fireScript;
    MenuItemScript waterScript;
    MenuItemScript plantScript;


    private MenuItemScript menuItemSc;
    private MenuItemScript previousMenuItemSc;

    // Start is called before the first frame update
    void Start()
    {
        fireScript = fire.GetComponentInChildren<MenuItemScript>();
        waterScript = water.GetComponentInChildren<MenuItemScript>();
        plantScript = plant.GetComponentInChildren<MenuItemScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (elementSelected == 1)
        {
            fireScript.Select();
            waterScript.Deselect();
            plantScript.Deselect();
        }
        if (elementSelected == 2)
        {
            fireScript.Deselect();
            waterScript.Select();
            plantScript.Deselect();
        }
        if (elementSelected == 3)
        {
            fireScript.Deselect();
            waterScript.Deselect();
            plantScript.Select();
        }
        /*
        normalisedMousePosition = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        currentAngle = Mathf.Atan2(normalisedMousePosition.y, normalisedMousePosition.x) * Mathf.Rad2Deg;

        currentAngle = (currentAngle + 360) % 360;

        selection = (int) currentAngle/120;

        if(selection != previousSelection)
        {
            previousMenuItemSc = menuItems[previousSelection].GetComponent<MenuItemScript>();
            previousMenuItemSc.Deselect();
            previousSelection = selection;

            menuItemSc = menuItems[selection].GetComponent<MenuItemScript>();
            menuItemSc.Select(); 
        }

        Debug.Log(currentAngle);
        */
    }
    public void SelectElement(float element)
    {
        elementSelected = element;
        
    }
}
