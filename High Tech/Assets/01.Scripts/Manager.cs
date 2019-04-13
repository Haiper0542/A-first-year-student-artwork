using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Text Hand_name;
    public Image Hand_Img;

    public Text Bullet_Count;
    public Image Bullet_Img;

    public Image Screen;
    public Sprite[] UI_Img = new Sprite[3];
    public Sprite now;
    public int count;

    public Image[] CCTV_screen = new Image[6];

    public Image Hp;

    public static Manager instance;

    void Awake()
    {
        if (Manager.instance == null)
            Manager.instance = this;
    }

    public void start()
    {
        Change_img(now);
    }

    public void Change_img(Sprite change)
    {
        Hand_Img.sprite  = change;
    }

    public void bulletset(int bullet_count)
    {
        if (bullet_count == 0)
        {
            Bullet_Img.sprite = UI_Img[1];
            Bullet_Count.text = bullet_count.ToString();
        }
        else
        {
            Bullet_Img.sprite = UI_Img[1];
            Bullet_Count.text = bullet_count.ToString();
        }
    }

    public void bullet_UI(bool on,int bullet_count,int total_bullet)
    {
        if (on)
        {
            Bullet_Img.sprite = UI_Img[1];
            Bullet_Count.text = bullet_count.ToString() + "/" + total_bullet.ToString();
        }
        else
        {
            Bullet_Img.sprite = UI_Img[0];
            Bullet_Count.text = "";
        }
    }
    public void screen_UI(bool on)
    {
        StartCoroutine("cctv_screen_anime");
        //if (on)
        //{
        //    Screen.sprite = UI_Img[2];
        //}
        //else
        //{
        //    Screen.sprite = UI_Img[0];
        //}
    }

    IEnumerator cctv_screen_anime()
    {
        float delay = 0.05f;
        int c = 0;
        CCTV_screen[c].gameObject.SetActive(true);
        c++;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < 6; i++)
        {
            if (i == c)
            {
                continue;
            }
            CCTV_screen[i].gameObject.SetActive(false);
        }
        CCTV_screen[c].gameObject.SetActive(true);
        c++;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < 6; i++)
        {
            if (i == c)
            {
                continue;
            }
            CCTV_screen[i].gameObject.SetActive(false);
        }
        CCTV_screen[c].gameObject.SetActive(true);
        c++;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < 6; i++)
        {
            if (i == c)
            {
                continue;
            }
            CCTV_screen[i].gameObject.SetActive(false);
        }
        CCTV_screen[c].gameObject.SetActive(true);
        c++;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < 6; i++)
        {
            if (i == c)
            {
                continue;
            }
            CCTV_screen[i].gameObject.SetActive(false);
        }
        CCTV_screen[c].gameObject.SetActive(true);
        c++;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < 6; i++)
        {
            if (i == c)
            {
                continue;
            }
            CCTV_screen[i].gameObject.SetActive(false);
        }
        CCTV_screen[c].gameObject.SetActive(true);
        c++;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < 6; i++)
        {
            CCTV_screen[i].gameObject.SetActive(false);
        }
    }
}
