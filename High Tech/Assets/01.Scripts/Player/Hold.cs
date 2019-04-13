using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public bool reloading = false;
    public bool zooming = false;
    public bool pin = false;

    public Camera Cam;
    public GameObject[] Hand_Item = new GameObject[5];
    public int count = 0;
    public int Rifle_bullet = 0; //라이플 총알

    public bool isGun = false; //총을 들고 있는지
    public int focus = 0; //창에 포커스가 올라와있는지

    public Transform room;//메인 룸
    public GameObject Hand; //손

    public int k = 0;
    public int i = 0;

    public GameObject now_gun;
    public GameObject now_item;
    GameObject item;
    Rigidbody itemRb;
    Collider itemCol_p;
    Collider[] itemCol;
    

    public static Hold instance;

    void Awake()
    {
        if (Hold.instance == null)
            Hold.instance = this;
    }

    void Update()
    {
        if (focus > 0) //마우스 감추기
        {
            focus--;
            if (focus == 0)
            {
                //커서 고정시키기
                Cursor.lockState = CursorLockMode.Locked;
                //커서 감추기
                Cursor.visible = true;
            }
        }
        else
        {
            isGun = false;
            if (Inventory.instance.Item_codes[Inventory.instance.current()] == 4) //총을 들고 있으면
            {
                isGun = true;
                Gun.instance.Gun_pick(true);
            }
            else
            {
                Gun.instance.Gun_pick(false);
                Manager.instance.bullet_UI(false, 0, 0);
            }
            if (Input.GetMouseButton(0) && isGun && !reloading) //좌클릭 && 총을 들고 있음 && 재장전 중이 아님
            {
                now_gun.SendMessage("Shot");
            }

            if (Input.GetKeyDown(KeyCode.F)) //F키를 눌렀을때
            {
                int mask = 1 << 11;
                mask = ~mask;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);
                if (Physics.Raycast(ray, out hit, 12f, mask))
                {
                    float dist = Vector3.Distance(Cam.transform.position, hit.transform.position);
                    if ((hit.transform.tag == "Tools" || hit.transform.tag == "Gun" || hit.transform.tag == "Bullet") && !zooming) //줌상태가 아닐때
                    {
                        pick(hit); //아이템 줍기
                    }
                }
            }
            if (Input.GetMouseButtonDown(2) && Hand.transform.childCount > 0) //휠버튼 클릭 && 아이템을 들고 있을때
            {
                shoot(); //아이템 던지기
            }

            if (Input.GetKeyDown(KeyCode.R) && isGun && !reloading && Rifle_bullet > 0) //R키 클릭 && 총을 들고 있음 && 재장전 중이 아님
            {
                if(now_gun.GetComponent<Gun>().bullet_count < now_gun.GetComponent<Gun>().bullet_limit) //총에 들어 있는 총알이 최대보다 적을때
                {
                    count--; //아이템이 하나 줄어듬

                    if (zooming) //줌상태일때
                    {
                        StartCoroutine("ZoomOut"); //줌아웃
                    }
                    Reload(); //재장전

                    //총알 계산
                    if (30 - now_gun.GetComponent<Gun>().bullet_count > Rifle_bullet) //넣어야 할 총알이 가진 총알보다 많을때
                    {
                        now_gun.SendMessage("Reload", Rifle_bullet); //가진거 다 넣음
                    }
                    else
                    {
                        now_gun.SendMessage("Reload", 30 - Gun.instance.bullet_count); //넣어야 할 총알만 넣음
                    }
                }
            }

            if (zooming && Input.GetMouseButtonDown(1))
            {
                StartCoroutine("ZoomOut");
            }
            else if (isGun && Input.GetMouseButtonDown(1))
            {
                StartCoroutine("ZoomIn");
            }

            if (Inventory.instance.Item_codes[Inventory.instance.current()] == 9 && Input.GetMouseButtonDown(1))
            {
                now_item.transform.SendMessage("Bang");
            }
            if (Inventory.instance.Item_codes[Inventory.instance.current()] == 10 && Input.GetMouseButtonDown(1))
            {
                now_item.transform.SendMessage("Bang");
            }
        }
    }
    
    public void Start_zoomout()
    {
        StartCoroutine("ZoomOut_ez");
    }

    IEnumerator ZoomIn()
    {
        zooming = true;
        for(int i = 0; i < 5; i++)
        {
            now_gun.transform.Translate(0.02364f, 0.0065f, 0);
            now_gun.transform.Rotate(0, -0.265f, 0.341f);
            yield return new WaitForSeconds(0.0005f);
        }
        for (int i = 0; i < 20; i++)
        {
            if(i < 15)
            {
                now_gun.transform.Translate(0.02364f, 0.0065f, 0);
                now_gun.transform.Rotate(0, -0.265f, 0.341f);
            }
            Cam.fieldOfView -= 0.8f;
            yield return new WaitForSeconds(0.0005f);
        }
        //Hand.transform.localPosition = new Vector3(0, -0.13f,0.7f);
        //now_gun.transform.localEulerAngles = new Vector3(7, 180, 0);
    }

    IEnumerator ZoomOut_ez()
    {
        zooming = false;
        for (int i = 0; i < 5; i++)
        {
            now_gun.transform.Translate(-0.024f, -0.0065f, 0);
            now_gun.transform.Rotate(0, 0.265f, -0.341f);
            yield return new WaitForSeconds(0.0001f);
        }
        for (int i = 0; i < 20; i++)
        {
            if (i < 15)
            {
                now_gun.transform.Translate(-0.024f, -0.0065f, 0);
                now_gun.transform.Rotate(0, 0.265f, -0.341f);
            }
            Cam.fieldOfView += 0.8f;
            yield return new WaitForSeconds(0.0001f);
        }
        FollowCam.instance.Canmove = true;
    }

    IEnumerator ZoomOut()
    {
        zooming = false;
        for (int i = 0; i < 5; i++)
        {
            now_gun.transform.Translate(-0.024f, -0.0065f, 0);
            now_gun.transform.Rotate(0, 0.265f, -0.341f);
            yield return new WaitForSeconds(0.0005f);
        }
        for (int i = 0; i < 20; i++)
        {
            if (i < 15)
            {
                now_gun.transform.Translate(-0.024f, -0.0065f, 0);
                now_gun.transform.Rotate(0, 0.265f, -0.341f);
            }
            Cam.fieldOfView += 0.8f;
            yield return new WaitForSeconds(0.0005f);
        }
        FollowCam.instance.Canmove = true;
    }

    public void Reload()
    {
        for (int i = 0; i < Hand.transform.childCount; i++)
        {
            if (Hand.transform.GetChild(i).name == "Bullet 1")
            {
                Destroy(Hand.transform.GetChild(i).gameObject);
                break;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (Inventory.instance.Item_codes[i] == 2)
            {
                Inventory.instance.RemoveItem(i);
                Hand_Item[i] = null;
                break;
            }
        }
    }


    public void pick(RaycastHit hit)
    {
        if (count == 5)
        {
            return;
        }
        item = hit.transform.gameObject;
        for (k = 0; k < 5; k++)
        {
            if (Hand_Item[k] == null)
            {
                Hand_Item[k] = item;
                select_Item(k);
                Inventory.instance.select("re");
                break;
            }
        }
        count++;

        itemRb = item.GetComponent<Rigidbody>();
        if (item.GetComponent<Collider>() != null)
        {
            item.GetComponent<Collider>().enabled = false;
        }

        itemRb.isKinematic = true;

        for (int i = 0; i < item.transform.childCount; i++)
        {
            if (item.transform.GetChild(i).GetComponent<Collider>() != null)
            {
                item.transform.GetChild(i).GetComponent<Collider>().enabled = false;
            }
        }

        if (item.name == "Mug")
        {
            item.transform.SetParent(Hand.transform);
            Inventory.instance.addItem(1);
        }
        else if (item.name == "Bullet 1")
        {
            item.transform.SetParent(Hand.transform);
            Inventory.instance.addItem(2);
            Rifle_bullet += 30;
        }
        else if (item.name == "Book")
        {
            item.transform.SetParent(Hand.transform);
            Inventory.instance.addItem(3);
        }
        else if (item.name == "Gun")
        {
            item.transform.SetParent(Hand.transform);
            Inventory.instance.addItem(4);
            now_gun = item;
            now_gun.SendMessage("Gun_pick", true);
        }
        else if (item.name == "Cellphone")
        {
            item.transform.SetParent(Hand.transform);
            Inventory.instance.addItem(5);
        }
        else if (item.name == "Memo")
        {
            item.transform.SetParent(Hand.transform);
            Inventory.instance.addItem(6);
        }
        else if (item.name == "Basket_ball")
        {
            item.transform.SetParent(Hand.transform);
            Inventory.instance.addItem(7);
        }
        else if (item.name == "Map")
        {
            item.transform.SetParent(Hand.transform);
            Inventory.instance.addItem(8);
        }
        else if (item.name == "Grenade")
        {
            item.transform.SetParent(Hand.transform);
            Inventory.instance.addItem(9);
        }
        else if (item.name == "Smoke_Grenade")
        {
            item.transform.SetParent(Hand.transform);
            Inventory.instance.addItem(10);
        }

        item.transform.localPosition = new Vector3(0, 0, 0);
        item.transform.localRotation = Quaternion.Euler(0, 0, 0);

        item = null;
        itemRb = null;
        itemCol = null;
    }

    private void OnApplicationFocus(bool Focus)
    {
        if (Focus)
        {
            focus = 10;
        }
    }

    public void drop() //떨구기
    {
        if (zooming) //줌상태일때
        {
            StartCoroutine("ZoomOut"); //줌아웃
        }
        if (Inventory.instance.now_code() != 0)
        {
            count--;
            if (Inventory.instance.now_code() == 2)
            {
                Rifle_bullet -= 30;
            }
            Inventory.instance.removeItem();
            GameObject Hitem = null;
            if (Hand.transform.childCount > 0)
            {
                Hitem = Hand_Item[Inventory.instance.current_select].GetComponentInChildren<Rigidbody>().gameObject;
            }
            Rigidbody Hrb = Hitem.GetComponent<Rigidbody>();
            if (Hitem.GetComponent<Collider>() != null)
            {
                Hitem.GetComponent<Collider>().enabled = true;
            }

            for (int i = 0; i < Hitem.transform.childCount; i++)
            {
                if (Hitem.transform.GetChild(i).GetComponent<Collider>() != null)
                {
                    Hitem.transform.GetChild(i).GetComponent<Collider>().enabled = true;
                }
            }

            Hitem.transform.SetParent(room);
            Hrb.isKinematic = false;

            int Addx = Random.Range(1, 3);
            int Addy = Random.Range(1, 3);
            int Addz = Random.Range(1, 3);
            Hrb.AddTorque(new Vector3(Addx, Addy, Addz), ForceMode.Impulse);

            Hand_Item[Inventory.instance.current()] = null;
        }
        now_item = null;
    }

    public void shoot() //던지기
    {
        if (zooming) //줌상태일때
        {
            StartCoroutine("ZoomOut"); //줌아웃
            print(zooming);
        }
        count--;
        if (Inventory.instance.now_code() == 2)
        {
            Rifle_bullet -= 30;
        }
        GameObject Hitem = null;
        if (Hand.transform.childCount > 0)
        {
            Hitem = Hand_Item[Inventory.instance.current()].GetComponentInChildren<Rigidbody>().gameObject;
            Hand_Item[Inventory.instance.current()] = null;
        }
        Inventory.instance.removeItem();
        Rigidbody Hrb = Hitem.GetComponent<Rigidbody>();

        if (Hitem.GetComponent<Collider>() != null)
        {
            Hitem.GetComponent<Collider>().enabled = true;
        }

        for (int i = 0; i < Hitem.transform.childCount; i++)
        {
            if (Hitem.transform.GetChild(i).GetComponent<Collider>() != null)
            {
                Hitem.transform.GetChild(i).GetComponent<Collider>().enabled = true;
            }
        }
        Hitem.transform.SetParent(room);

        Hrb.isKinematic = false;

        Vector3 throwAngle = Cam.transform.forward * 10;
        Hrb.AddForce(throwAngle + Vector3.up, ForceMode.Impulse);
        int Addx = Random.Range(1, 5);
        int Addy = Random.Range(1, 5);
        int Addz = Random.Range(1, 5);
        Hrb.AddTorque(new Vector3(Addx, Addy, Addz), ForceMode.Impulse);
        now_item = null;
    }

    public void select_Item(int select)
    {
        for (int i = 0; i < 5; i++)
        {
            if (Hand_Item[i] != null)
            {
                Hand_Item[i].SetActive(false);
            }
        }
        if (Hand_Item[select] != null)
        {
            now_item = Hand_Item[select];
            Hand_Item[select].SetActive(true);
        }
    }

    public bool Reloading()
    {
        return reloading;
    }
}