using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("[상태]")]
    State state = State.idle;
    enum State { idle, walking, shoting, reloading, zoomin }

    [Header("[탄퍼짐]")]
    public float outx = 0;
    public float outy = 0;
    public float outz = 0;
    public float aimout = 1; //퍼지는 정도
    public int c = 0; //연속으로 쏜 총알
    public int start = 2; //퍼지기 시작하는 횟수

    [Header("[총 정보]")]
    public float Shot_delay = 0.4f; //발사 속도
    public int bullet_count = 0; //총에 들어있는 총알 갯수
    public int bullet_limit = 30; //최대 들어갈 수 있는 총알
    public float time = 0;

    [Header("[총]")]
    public GameObject Current_Magazine; //현재 탄창
    public GameObject Magazine; //탄창
    public GameObject Ejection; //장전할때 움직이는거
    public GameObject Cartridge_Case; //탄피
    public GameObject Cartridge_Case_pos; //탄피 위치
    public GameObject bullet;
    public GameObject muzzle;
    public GameObject bullet_mark;
    public GameObject shot_hole;

    public RaycastHit hit;

    [Header("[사운드]")]
    public AudioClip reload_sound;
    public AudioClip fire_sound;
    public AudioClip outOfAmmo;
    public AudioClip getHit;
    AudioSource audioSource;

    public static Gun instance;
    
    void Awake()
    {
        if (Gun.instance == null)
            Gun.instance = this;
    }

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!Input.GetMouseButton(0)) //총을 쏘고 있지 않을때
        {
            aimout = 1; //탄퍼짐 초기화
            c = 0; //연속으로 쏜 총알 갯수 초기화
        }
        
        if (time <= 0.8f)
        {
            time += 0.1f;
        }
        if (time >= 0.8f)
        {
            bullet_mark.SetActive(false);
        }

        if (state != State.reloading && state != State.zoomin) //재장전중이 아니고 줌인하는중이 아닌 경우
        {
            if (Input.GetMouseButton(0))
            {
                state = State.shoting;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                state = State.walking;
            }
            else
            {
                state = State.idle;
            }
        }
    }

    public void Shot()
    {
        if (bullet_count > 0)
        {
            if (time > Shot_delay)
            {
                //이펙트
                c++;

                outx = Random.Range(-1.01f + aimout, 1.01f - aimout);
                outy = Random.Range(-1.01f + aimout, 1.01f - aimout);
                outz = Random.Range(-1.01f + aimout, 1.01f - aimout);

                if (Hold.instance.zooming)
                {
                    outx *= 0.8f;
                    outy *= 0.8f;
                    outz *= 0.8f;
                }
                if (!Hold.instance.zooming)
                {
                    if (c > (start * 0.8f) && aimout < 1.06f)
                    {
                        aimout += 0.01f;
                    }
                    FollowCam.instance.MoveCam(Random.Range(-0.2f, 0.2f), -aimout * 0.45f);
                }
                else
                {
                    if (c > start && aimout < 1.04f)
                    {
                        aimout += 0.01f;
                    }
                    FollowCam.instance.MoveCam(0, -aimout * 0.1f);
                }
                Ejection.transform.localPosition = new Vector3(0, 0, 0.13f);
                bullet_mark.SetActive(true);
                StartCoroutine("Shaking");
                Play_Fire();
                //계산
                time = 0;
                bullet_count--;
                Manager.instance.bulletset(bullet_count);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + new Vector3(outx, outy, outz), out hit, Mathf.Infinity))
                {
                    print(hit.transform.name);
                    if (hit.transform.tag == "Enemy")
                    {
                        hit.transform.SendMessage("dmg", 1);
                    }
                    if (hit.transform.gameObject.GetComponent<Breakable>() != null)
                        hit.transform.SendMessage("damage");
                    GameObject newhole = Instantiate(shot_hole, hit.point, hit.transform.rotation);
                    newhole.SetActive(true);
                }
                GameObject newcase = Instantiate(Cartridge_Case, Cartridge_Case_pos.transform.position, Cartridge_Case_pos.transform.rotation);
                newcase.transform.SetParent(Camera.main.transform);
                Rigidbody caseRb = newcase.GetComponent<Rigidbody>();
                caseRb.AddForce(Vector3.up * Random.Range(0.5f, 2) + -transform.right * Random.Range(1, 3), ForceMode.Impulse);
            }
        }
        else
        {
        }
    }

    IEnumerator Shaking()
    {
        yield return new WaitForSeconds(0.01f);
        transform.Translate(0, Random.Range(-0.003f, 0.003f), Random.Range(0.04f, 0.025f));
        if (c > 2 && !Hold.instance.zooming)
        {
            transform.Rotate(Random.Range(0, Mathf.Clamp(c,0,4)), Random.Range(0,-Mathf.Clamp(c, 0, 4)), 0);
        }
        yield return new WaitForSeconds(0.05f);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void Gun_pick(bool on)
    {
        Manager.instance.bullet_UI(on, bullet_count, Hold.instance.Rifle_bullet);
    }

    public void Reload(int add_bullet)
    {
        if (state != State.reloading)
        {
            state = State.reloading;
            StartCoroutine("reload_ani", add_bullet);
        }
    }

    IEnumerator reload_ani(int add_bullet)
    {
        Hold.instance.Rifle_bullet -= add_bullet;
        Hold.instance.reloading = true;

        for (int i = 0; i < 20; i++) //84
        {
            transform.Rotate(4.2f, 0, 0);
            Ejection.transform.Translate(0, 0, 0.0045f);
            yield return new WaitForSeconds(0.001f);
            if(i == 10)
            {
                Play_Reload();
            }
        }

        Current_Magazine.SetActive(false);
        GameObject newMagazine = Instantiate(Magazine, Current_Magazine.transform.position, Current_Magazine.transform.rotation);
        newMagazine.transform.SetParent(Camera.main.transform);
        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < 20; i++) //100
        {
            transform.Rotate(-5f, 0, 0);
            Ejection.transform.Translate(0, 0, -0.0095f);
            yield return new WaitForSeconds(0.001f);
        }
        Current_Magazine.SetActive(true);
        //16
        for (int i = 0; i < 10; i++)
        {
            transform.Rotate(1.6f, 0, 0);
            yield return new WaitForSeconds(0.001f);
        }
        Hold.instance.reloading = false;

        bullet_count += add_bullet;

        Manager.instance.bulletset(bullet_count);
        state = State.idle;
    }

    public void Play_Fire()
    {
        audioSource.PlayOneShot(fire_sound);
    }
    public void Play_GetHit()
    {
        audioSource.PlayOneShot(getHit);
    }
    public void Play_Reload()
    {
        audioSource.PlayOneShot(reload_sound);
    }
    public void Play_OutOfAmmo()
    {
        audioSource.PlayOneShot(outOfAmmo);
    }
}
