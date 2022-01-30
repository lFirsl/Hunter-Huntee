using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json;


public class StatUpdate
{
    public int EnemiesKilled { get; set; }
    public int Deaths { get; set; }
    public int DamageTaken { get; set; }
    public int LevelsFinished { get; set; }
    public int CharacterSwitches { get; set; }
}


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class winScript : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider bc;
    public bool winConditionAchieved = false;
    public GameObject gate;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        bc.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (winConditionAchieved)
        {
            gate.SetActive(false);
        }
    }

    void UploadStats()
    {
        StatUpdate stat_update = new StatUpdate {
            EnemiesKilled = 6,
            Deaths = 9,
            DamageTaken = 4,
            LevelsFinished = 2,
            CharacterSwitches = 0,
        };
        string data = JsonConvert.SerializeObject(stat_update);
        Debug.Log(data);
        byte[] bytes = Encoding.UTF8.GetBytes(data);



        using (UnityWebRequest www = new UnityWebRequest("http://127.0.0.1:9000/update", "POST"))
        {
            www.uploadHandler = (UploadHandler) new UploadHandlerRaw(bytes);
            www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<playScript>() /*&& winConditionAchieved*/)
        {
            Debug.Log("COLLIDED");
            UploadStats();
            if (GameObject.Find("LevelLoader"))
            {
                GameObject ob = GameObject.Find("LevelLoader");
                ob.GetComponent<LevelLoaderScript>().LoadNextLevel();
            }
        }
    }
}
