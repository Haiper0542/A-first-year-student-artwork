using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public int fishCount = 0;
    public int rebuildPercent = 0;

    public Text fishCountText;
    public Text rebuildPercentText;

    public GameObject MainScreen;
    public GameObject TrashSortScreen;
    public GameObject PipeGameScreen;

    public GameObject MainUI01;
    public GameObject MainUI02;
    public GameObject MainUI03;
    public GameObject TrashSortUI;
    public GameObject PipeGameUI;

    public GameObject PipeGameBtn;

    public Text speechBubble;

    string[] basicScript = new string[5];
    string[] oldScript = new string[4];
    string[] newScript = new string[2];

    public Transform catParent;
    public CatCtrl[] cats = new CatCtrl[3];

    public AudioClip nyanSound;
    public AudioSource myAudio;

    public GameObject Cloud;

    public SpriteRenderer mainBackImg;
    public Sprite[] mainBackImgs = new Sprite[4];

    public GameObject[] Ruined = new GameObject[2];


    public GameObject RuinTree;
    public GameObject Tree;

    public GameObject Chri;

    public int nowTarget = 0;

    public GameObject StarParticle;

    public static MainManager instance;

    private void Awake()
    {
        instance = this;
        basicScript[0] = "냥!";
        basicScript[1] = "연어 먹고 싶다";
        basicScript[2] = "이 마을엔 고양이들이 정말 많아";
        basicScript[3] = "고양이는 귀엽다.. 물론 나도 귀엽다";
        basicScript[4] = "냥냥펀치!";

        oldScript[0] = "정말 더러운 곳이었어...";
        oldScript[1] = "넓어졌냥!";
        oldScript[2] = "이젠 깨끗한 물을 마실 수 있겠지?";
        oldScript[3] = "이제 길에서 실직자 냥이들을 덜 보게 되겠군..";

        newScript[0] = "이제야 고급 고양이 집 답군!";
        newScript[1] = "으음! 이제야 내 명성에 걸맞는 집이야";
    }

    private void Start()
    {
        StartCoroutine("ScriptChange");
    }

    public void Update()
    {
        fishCountText.text = "x " + fishCount.ToString() + "마리";
        rebuildPercentText.text = (rebuildPercent * 25).ToString() + "%";
        if(rebuildPercent >= 1)
            mainBackImg.sprite = mainBackImgs[rebuildPercent - 1];

        if (fishCount >= 200 && nowTarget == 3)
        {
            AddCat();
            AddCat();
            AddCat();
            AddCat();
            GameObject newStar = Instantiate(StarParticle, new Vector3(2.5f,3,0), Quaternion.identity);
            Destroy(newStar, 3);
            nowTarget = 4;
            rebuildPercent++;
        }
        else if (fishCount >= 150 && nowTarget == 2)
        {
            AddCat();
            AddCat();
            AddCat();
            AddCat();
            GameObject newStar = Instantiate(StarParticle, new Vector3(2.5f, 3, 0), Quaternion.identity);
            Destroy(newStar, 3);
            nowTarget = 3;
            rebuildPercent++;
        }
        else if (fishCount >= 100 && nowTarget == 1)
        {
            AddCat();
            AddCat();
            AddCat();
            GameObject newStar = Instantiate(StarParticle, new Vector3(2.5f, 3, 0), Quaternion.identity);
            Destroy(newStar, 3);
            nowTarget = 2;
            Ruined[1].SetActive(false);
            rebuildPercent++;

            RuinTree.SetActive(false);
            Tree.SetActive(true);
            Chri.SetActive(true);
        }
        else if(fishCount >= 50 && nowTarget == 0)
        {
            AddCat();
            AddCat();
            AddCat();
            GameObject newStar = Instantiate(StarParticle, new Vector3(2.5f, 3, 0), Quaternion.identity);
            Destroy(newStar, 3);
            nowTarget = 1;
            Ruined[0].SetActive(false);
            PipeGameBtn.SetActive(true);
            rebuildPercent++;
        }


        if (Random.Range(0,2000) < 5)
        {
            Instantiate(Cloud, new Vector3(-4, Random.Range(-3.7f, 4.3f), -10), Quaternion.identity);
        }
    }

    public void GoMain()
    {
        MainScreen.SetActive(true);
        MainUI01.SetActive(true);
        MainUI02.SetActive(true);
        MainUI03.SetActive(true);

        TrashSortScreen.SetActive(false);
        TrashSortUI.SetActive(false);
        PipeGameScreen.SetActive(false);
        PipeGameUI.SetActive(false);
    }
    public void GoTrashSort()
    {
        TrashSortScreen.SetActive(true);
        TrashSortUI.SetActive(true);

        MainScreen.SetActive(false);
        MainUI01.SetActive(false);
        MainUI02.SetActive(false);
        MainUI03.SetActive(false);
        PipeGameScreen.SetActive(false);
        PipeGameUI.SetActive(false);
    }
    public void GoPipeGame()
    {
        PipeGameScreen.SetActive(true);
        PipeGameUI.SetActive(true);

        MainScreen.SetActive(false);
        MainUI01.SetActive(false);
        MainUI02.SetActive(false);
        MainUI03.SetActive(false);
        TrashSortScreen.SetActive(false);
        TrashSortUI.SetActive(false);
    }

    IEnumerator ScriptChange()
    {
        if (rebuildPercent < 2)
            speechBubble.text = basicScript[Random.Range(0, 5)];
        else
            speechBubble.text = newScript[Random.Range(0, 2)];

        yield return new WaitForSeconds(Random.Range(10, 13));
        yield return StartCoroutine("ScriptChange");
    }

    public void AddCat()
    {
        CatCtrl newCat = Instantiate(cats[Random.Range(0, 3)], Vector2.zero, Quaternion.identity);
        newCat.transform.SetParent(catParent);
        myAudio.PlayOneShot(nyanSound);
    }

    public void CheatKey()
    {
        fishCount += 50;
    }
}
