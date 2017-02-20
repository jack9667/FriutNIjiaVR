using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FruitSpawn : MonoBehaviour {

    public static FruitSpawn Instance;

    public GameObject[] FruitPrefabs;

    public int TotalScore;
    private float timeRemain;
    private float timeCount;
    private float timeStart;
    public Text TextScore;
    public Text TextTime;


    public GameObject RestartUI;
    public Transform GameOverPosition;
    private bool isOver;

    //public SteamVR_TrackedObject hand;
    //SteamVR_Controller.Device device;
    //private bool isNotShake;

    

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void FixedUpdate()
    {
        if (isOver)
        {
            return;
        }
        timeRemain = timeCount - (Time.time - timeStart);
        TextTime.text= AddTime(timeRemain);
        if (timeRemain <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        GameObject obj = Instantiate(RestartUI);
        obj.transform.position = GameOverPosition.position;
        obj.transform.localScale = Vector3.zero;
        obj.transform.DOScale(Vector3.one * 25f, 1.0f);
        isOver = true;
        StopAllCoroutines();
    }

    private string AddTime(float time)
    {
        string strTime;
        int timeBuff = (int)time;
        strTime = time > 10 ? "00:" + timeBuff.ToString() : "00:0" + timeBuff.ToString();
        return strTime;
    }

    //水果的生成
    private void StartSpawn()
    {
        StartCoroutine(Spawn());
    }

     IEnumerator Spawn()
    {
        while (true)
        {
            for(int i = 0; i < Random.Range(1, 5); i++)
            {
                GameObject Fruit = Instantiate(FruitPrefabs[Random.Range(0, FruitPrefabs.Length)]);
                Rigidbody rigibody = Fruit.GetComponent<Rigidbody>();
                rigibody.velocity = new Vector3(0, 5f, 0);
                rigibody.angularVelocity = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                rigibody.useGravity = true;
                Vector3 Position = transform.position + transform.right * Random.Range(-1f, 1f);
                Fruit.transform.position = Position;
            }
            

            yield return new WaitForSeconds(2.0f);
        }   
    }

    public void AddScore()
    {
        TotalScore += 10;
        TextScore.text = TotalScore.ToString();
    }

    public void RestartGame()
    {
        Invoke("Init", 2);
    }

    public void Init()
    {
        TotalScore = 0;
        StartSpawn();
        timeCount = 60f;
        timeStart = Time.time;
        isOver = false;
        TextScore.text = "0";
        TextTime.text = "00:00";

        //isNotShake = false;
        //hand = GetComponent<SteamVR_TrackedObject>();
        //device = SteamVR_Controller.Input((int)hand.index);
    }

    //public void GetHandShaking()
    //{
    //    StartCoroutine("Shake", 1.0f);
        
    //}

    //IEnumerator Shake(float time)
    //{
    //    Invoke("StopShake", time);
    //    while (!isNotShake)
    //    {
    //        device.TriggerHapticPulse(500);
    //        yield return new WaitForEndOfFrame();
    //    }
        
    //}

    //private void StopShake()
    //{
    //    isNotShake = true;
    //}
}
