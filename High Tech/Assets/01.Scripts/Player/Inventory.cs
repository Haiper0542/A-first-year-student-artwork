using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public Image[] Slot_Item = new Image[5];
    public Text[] Slot_Name = new Text[5];
    public Image[] Select = new Image[5];

    public Sprite[] Item = new Sprite[10];
    public int[] Item_codes = new int[5];
    public string[] Item_name = new string[5];

    public int current_select = 0;

    public static Inventory instance;

    void Awake()
    {
        if (Inventory.instance == null)
            Inventory.instance = this;
    }

    public void Start()
    {
    }

    public void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !Hold.instance.Reloading())
        {
            if (Hold.instance.zooming)
            {
                Hold.instance.Start_zoomout();
                Hold.instance.zooming = false;
            }
            select("--");
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !Hold.instance.Reloading())
        {
            if (Hold.instance.zooming)
            {
                Hold.instance.Start_zoomout();
                Hold.instance.zooming = false;
            }
            select("++");
        }
    }

    public void addItem(int Item_code)
    {
        for (int i = 0; i < 5; i++)
        {
            if (Item_codes[i] == 0) //해당 칸이 비어있을 때
            {
                Item_codes[i] = Item_code;
                Slot_Item[i].sprite = Item[Item_code];
                Slot_Name[i].text = Item_name[Item_code];
                break;
            }
        }
    }
    public void removeItem()
    {
        Item_codes[current_select] = 0;
        Slot_Item[current_select].sprite = Item[0];
        Slot_Name[current_select].text = Item_name[0];
    }
    public void RemoveItem(int c)
    {
        Item_codes[c] = 0;
        Slot_Item[c].sprite = Item[0];
        Slot_Name[c].text = Item_name[0];
    }

    public void select(string x)
    {
        if (x == "--")
        {
            Select[current_select].gameObject.SetActive(false);
            current_select--;
            if (current_select < 0)
            {
                current_select = 4;
            }
            Select[current_select].gameObject.SetActive(true);
            Hold.instance.select_Item(current_select);
        }
        else if (x == "++")
        {
            Select[current_select].gameObject.SetActive(false);
            current_select++;
            if (current_select > 4)
            {
                current_select = 0;
            }
            Select[current_select].gameObject.SetActive(true);
            Hold.instance.select_Item(current_select);
        }
        else
        {
            current_select = Hold.instance.k;
            if(current_select == -1)
            {
                current_select = 0;
            }
            for (int i = 0; i < 5; i++)
            {
                Select[i].gameObject.SetActive(false);
            }
            Select[current_select].gameObject.SetActive(true);
        }
    }
    public int current()
    {
        return current_select;
    }
    public int now_code()
    {
        return Item_codes[current_select];
    }
}
