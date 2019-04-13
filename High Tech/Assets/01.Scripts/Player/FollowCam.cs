using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    [Header("추적 대상")]
    public Transform target;
    public Transform player;

    public Transform hand;
    public Transform smallhand;
    Vector3 Pos_target;
    public float dist = 0;
    public bool on = true;

    [Header("회전 속도")]
    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;

    public float mouseSpeed = 1;

    public float x = 0.0f;
    public float y = 0.0f;

    public float yMin = -80f;
    public float yMax = 80f;
    Quaternion rotation;

    public bool Canmove = true;

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public static FollowCam instance;

    void Awake()
    {
        if (FollowCam.instance == null)
            FollowCam.instance = this;
    }

    void Start()
    {
        //커서 고정시키기
        Cursor.lockState = CursorLockMode.Locked;
        //커서 감추기
        Cursor.visible = false;

        //플레이어를 시작시 타겟으로 잡음
        target = player.transform;

        //기본설정
        Vector3 angles = transform.eulerAngles;
    }

    void LateUpdate()
    {
        if (on)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.03f * mouseSpeed;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.03f * mouseSpeed;

            y = ClampAngle(y, yMin, yMax);
            rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 0.0f, -dist) + Pos_target;

            //transform.rotation = Quaternion.Euler(new Vector3(y, x, 0));
            rotation = Quaternion.Euler(0, x, 0);
            //transform.position = position;

            position = new Vector3(position.x, position.y, position.z);
            if (Inventory.instance.now_code() == 3)
            {
                smallhand.localPosition = new Vector3(0, 0f, 0.65f);
            }
            else if (Inventory.instance.now_code() == 2)
            {
                smallhand.localPosition = new Vector3(0.33f, 0f, 0.5f);
            }
            else if (Inventory.instance.now_code() == 4 && Canmove)
            {
                smallhand.localPosition = new Vector3(0.48f, -0.24f, 0.7f);
            }
            else if (!Canmove)
            {

            }
            else if (Inventory.instance.now_code() == 5)
            {
                smallhand.localPosition = new Vector3(0.5f, 0, 0.7f);
            }
            else if (Inventory.instance.now_code() == 6)
            {
                smallhand.localPosition = new Vector3(0, 0.15f, 0.6f);
            }
            else if (Inventory.instance.now_code() == 7)
            {
                smallhand.localPosition = new Vector3(0, 0, 0.6f);
            }
            else if (Inventory.instance.now_code() == 8)
            {
                smallhand.localPosition = new Vector3(0.5f, 0, 0.7f);
            }
            else if (Inventory.instance.now_code() == 9 || Inventory.instance.now_code() == 10)
            {
                smallhand.localPosition = new Vector3(0.5f, -0.1f, 0.8f);
            }
            else
            {
                smallhand.localPosition = new Vector3(0, 0, 0.5f);
            }

            if (Inventory.instance.now_code() == 2 || Inventory.instance.now_code() == 4 || Inventory.instance.now_code() == 5
                 || Inventory.instance.now_code() == 8 || Inventory.instance.now_code() == 9 || Inventory.instance.now_code() == 10)
            {
                hand.position = Camera.main.transform.position;
            }
            else
            {
                hand.position = position;
            }

            if (Inventory.instance.now_code() == 5)
            {
                smallhand.localRotation = Quaternion.Euler(-100, 0, 30);
            }
            else if (Inventory.instance.now_code() == 2)
            {
                smallhand.localRotation = Quaternion.Euler(78, 31, 0);
            }
            else if (Inventory.instance.now_code() == 4 && Canmove)
            {
                smallhand.localRotation = Quaternion.Euler(7.29f, 185.31f, -6.82f);
            }
            else if (!Canmove)
            {

            }
            else if (Inventory.instance.now_code() == 8)
            {
                smallhand.localRotation = Quaternion.Euler(0, 120, -10);
            }
            else if (Inventory.instance.now_code() == 9 || Inventory.instance.now_code() == 10)
            {
                smallhand.localRotation = Quaternion.Euler(0, 130, -10);
            }
            else
            {
                smallhand.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (Inventory.instance.now_code() == 2 || Inventory.instance.now_code() == 4 || Inventory.instance.now_code() == 5
                || Inventory.instance.now_code() == 8 || Inventory.instance.now_code() == 9 || Inventory.instance.now_code() == 10)
            {
                hand.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles);
            }
            else
            {
                hand.rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + 25, rotation.eulerAngles.z);
            }
            Pos_target = new Vector3(target.position.x, target.position.y + 0.5f, target.position.z);
            //player.transform.rotation = rotation;
        }
    }

    public void Targeting(GameObject hit)
    {
        target = hit.transform;
        if(target.name == "CCTV")
        {
            Manager.instance.screen_UI(true);
            player.transform.position = new Vector3(target.transform.position.x, target.transform.position.y - 0.5f, target.transform.position.z);
        }
        if(target.name == "Player")
        {
            Manager.instance.screen_UI(false);
            player.transform.position = new Vector3(target.transform.position.x, target.transform.position.y - 0.5f, target.transform.position.z);
        }
    }
    public void On(bool on_)
    {
        on = on_;
    }
    public void MoveCam(float addX, float addY)
    {
        x += addX;
        y += addY;
    }
}