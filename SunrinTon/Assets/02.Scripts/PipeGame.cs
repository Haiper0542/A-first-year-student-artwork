using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeGame : MonoBehaviour
{
    public PipeTile firstPipe;
    public PipeTile lastPipe;
    public Text scoreText;

    public GameObject firstWater;
    public GameObject lastWater;

    public float maxTime = 10;
    float nowTime = 10;

    public Transform timeFillImg;
    public GameObject HelpImg;
    public GameObject TouchToStartText;
    float now = 0;
    public float twinkleTerm = 0.7f;
    bool twinkle = true;

    public bool isStarted = false;
    public bool isStop = false;

    int pipeScore = 0;

    [Header("GameOver")]
    public GameObject gameOverScreen;
    public GameObject buttons;
    public Text overScoreText;
    public Text fishText;
    
    public GameObject[] Stage = new GameObject[3];

    public void GameStart()
    {
        isStarted = true;

        now = 0;
        twinkle = true;
        TouchToStartText.SetActive(false);
        HelpImg.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < Stage[i].transform.childCount; j++)
            {
                for (int k = 0; k < Random.Range(1, 3); k++)
                    Stage[i].transform.GetChild(j).GetComponent<PipeTile>().RotatePipe();
            }
        }
        Stage[Random.Range(0, 3)].SetActive(true);
    }

    private void Update()
    {
        if (isStarted)
        {
            if (!isStop)
            {
                if (nowTime > 0)
                {
                    nowTime -= Time.deltaTime;
                    timeFillImg.localPosition = new Vector3((nowTime / maxTime) * 720 - 750, 0, 0);
                }
                else
                {
                    WaterDown();
                    isStop = true;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
                    if (hit.collider != null && !hit.transform.GetComponent<PipeTile>().isFixed)
                    {
                        hit.transform.GetComponent<PipeTile>().RotatePipe();
                    }
                }

            }
        }
        else
        {
            if (Input.GetMouseButton(0))
                GameStart();

            if ((now += Time.deltaTime) > twinkleTerm)
            {
                TouchToStartText.SetActive(twinkle);
                twinkle = !twinkle;
                now = 0;
            }
        }
    }

    public void WaterDown()
    {
        firstPipe.isWater = true;
        firstPipe.WaterUpdate(0);

        firstWater.SetActive(true);

        Invoke("WaterCheck", 1f);
    }

    public void WaterCheck()
    {
        if (lastPipe.isWater)
        {
            maxTime *= 0.97f;
            nowTime = maxTime;
            isStop = false;
            pipeScore++;
            scoreText.text = pipeScore.ToString();
            lastWater.SetActive(true);
            NextStage();
        }
        else
            StartCoroutine("GameOver");
    }

    public void NextStage()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < Stage[i].transform.childCount; j++)
            {
                Stage[i].transform.GetChild(j).GetComponent<PipeTile>().isWater = false;
                firstPipe.isWater = false;
                lastPipe.isWater = false;
                for (int k = 0; k < Random.Range(1, 3); k++)
                {
                    Stage[i].transform.GetChild(j).GetComponent<PipeTile>().RotatePipe();
                }
            }
        }

        Stage[0].SetActive(false);
        Stage[1].SetActive(false);
        Stage[2].SetActive(false);

        Stage[Random.Range(0, 3)].SetActive(true);

        firstWater.SetActive(false);
        lastWater.SetActive(false);
    }

    IEnumerator GameOver()
    {
        isStop = true;
        gameOverScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < pipeScore; i++)
        {
            overScoreText.text = pipeScore.ToString() + "점";
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i <= pipeScore; i++)
        {
            fishText.text = i.ToString() + "마리";
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);
        buttons.SetActive(true);

        MainManager.instance.fishCount += pipeScore;
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
        firstWater.SetActive(false);
        lastWater.SetActive(false);

        maxTime = 10;
        nowTime = 10;

        timeFillImg.transform.localPosition = new Vector3(-2, 191, 0);
        TouchToStartText.SetActive(true);

        now = 0;

        twinkle = true;

        isStarted = false;
        isStop = false;

        pipeScore = 0;

        gameOverScreen.SetActive(false);
        buttons.SetActive(false);
        overScoreText.text = "0점";
        scoreText.text = "0";
        fishText.text = "0마리";
    }
}

