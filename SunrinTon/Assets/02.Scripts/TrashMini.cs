using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashMini : MonoBehaviour
{
    public GameObject TouchToStartText;
    float now = 0;
    public float twinkleTerm = 0.7f;
    bool twinkle = true;

    public GameObject[] trashObj;
    int trashSortCount = 6;

    int[] ObjIds = new int[10];
    public GameObject[] Objs = new GameObject[10];
    public SpriteRenderer[] Sprites = new SpriteRenderer[10];

    public Transform timeFillImg;
    public Text scoreText;

    float maxTime = 5;
    public float nowTime = 5;

    public int fishCount = 0;
    public int trashCount = 0;

    public bool gameStart = false;
    public bool isStop = false;
    public GameObject inGamebuttons;
    public GameObject HelpImg;

    [Header("GameOver")]
    public GameObject gameOverScreen;
    public GameObject buttons;
    public Text overScoreText;
    public Text fishText;

    public void GameStart()
    {
        for (int i = 0; i < 4; i++)
        {
            int r = Random.Range(0, trashSortCount);
            ObjIds[i] = r / 2;
            GameObject newTrash = Instantiate(trashObj[r], new Vector3(0, -1 + i * 0.9f, i), Quaternion.identity);
            newTrash.transform.localScale = Vector2.one * (0.8f - i * 0.1f);
            Objs[i] = newTrash;
            Sprites[i] = newTrash.GetComponent<SpriteRenderer>();
            Sprites[i].color = new Color(255, 255, 255, (40 - i) * 0.025f);
        }
        gameStart = true;

        now = 0;
        twinkle = true;
        TouchToStartText.SetActive(false);
        HelpImg.SetActive(false);
        inGamebuttons.SetActive(true);
    }

    public void Update()
    {
        if (gameStart)
        {
            if (!isStop)
            {
                if (nowTime > 0)
                {
                    nowTime -= Time.deltaTime;
                    timeFillImg.localPosition = new Vector3((nowTime / maxTime) * 260 - 290, 592, 0);
                }
                else
                    StartCoroutine("GameOver");
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
                GameStart();

            if((now += Time.deltaTime) > twinkleTerm)
            {
                TouchToStartText.SetActive(twinkle);
                twinkle = !twinkle;
                now = 0;
            }
        }
    }

    public void TrashSort(int num)
    {
        if (num == ObjIds[0])
        {
            trashCount++;
            scoreText.text = trashCount.ToString();

            if (maxTime > 0.5f)
                maxTime *= 0.985f;
            nowTime = maxTime;

            Objs[0].GetComponent<ShapeCtrl>().isStop = false;
            Objs[0].transform.position += Vector3.back;
            switch (num)
            {
                case 0:
                    Objs[0].GetComponent<ShapeCtrl>().targetPos = new Vector2(-1.7f, -3.75f);
                    break;
                case 1:
                    Objs[0].GetComponent<ShapeCtrl>().targetPos = new Vector2(0, -3.75f);
                    break;
                case 2:
                    Objs[0].GetComponent<ShapeCtrl>().targetPos = new Vector2(1.7f, -3.75f);
                    break;
            }

            for (int i = 0; i < 3; i++)
            {
                ObjIds[i] = ObjIds[i + 1];
                Sprites[i] = Sprites[i + 1];
                Sprites[i].color = new Color(255, 255, 255, (40 - i) * 0.025f);
                Objs[i] = Objs[i + 1];
                Objs[i].transform.position = new Vector3(0, -1 + i * 0.9f, i + 1);
                Objs[i].transform.localScale = Vector2.one * (0.8f - (i + 1) * 0.1f);
            }
            int r = Random.Range(0, trashSortCount);
            ObjIds[3] = r / 2;
            GameObject newTrash = Instantiate(trashObj[r], new Vector3(0, 1.7f, 9), Quaternion.identity);
            newTrash.transform.localScale = Vector2.one * (0.3f);
            newTrash.transform.SetParent(transform);
            Objs[3] = newTrash;
            Sprites[3] = newTrash.GetComponent<SpriteRenderer>();
            Sprites[3].color = new Color(255, 255, 255, 0.51f);
        }
        else
            StartCoroutine("GameOver");
    }

    IEnumerator GameOver()
    {
        isStop = true;
        gameOverScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < trashCount; i++)
        {
            overScoreText.text = trashCount.ToString() + "점";
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i <= trashCount / 10; i++)
        {
            fishText.text = i.ToString() + "마리";
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);
        buttons.SetActive(true);

        MainManager.instance.fishCount += trashCount / 10;
    }

    public void RestartBtn()
    {
        ResetAll();
    }

    public void BackBtn()
    {
        ResetAll();
        MainManager.instance.GoMain();
    }

    public void ResetAll()
    {
        timeFillImg.transform.localPosition = new Vector3(-2, 191, 0);
        scoreText.text = "0";

        maxTime = 5;
        nowTime = 5;

        fishCount = 0;
        trashCount = 0;

        gameStart = false;
        isStop = false;
        gameOverScreen.SetActive(false);
        buttons.SetActive(false);
        inGamebuttons.SetActive(false);
        overScoreText.text = "0점";
        fishText.text = "0마리";

        for(int i = 0; i < 4; i++)
        {
            Destroy(Objs[i]);
            Objs[i] = null;
            Sprites[i] = null;
        }
    }
}
