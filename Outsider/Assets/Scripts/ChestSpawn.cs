using UnityEngine;
using System.Collections;

public class ChestSpawn : MonoBehaviour {

    public Transform[] Spawn;
    public GameObject ChestPrefab;

    public float spawnTimer = 8f;
    float timer = 0f;
    int random, random2;

    public bool Spawned;

    SoundManager SoMa;

    public void Awake()
    {
        spawnTimer = spawnTimer - SaveManager.Instance.RetornarMejoraCantChest();
    }
    // Use this for initialization
    void Start () {
        Spawned = false;
        SoMa = GameObject.FindObjectOfType(typeof(SoundManager)) as SoundManager;

    }

    // Update is called once per frame
    void Update () {

        timer += Time.deltaTime;
        if (timer >= spawnTimer)
        {
            
                timer = 0f;
            
            if (Spawned == false)
            { 
                random = Random.Range(0, 4);
                  if (random == 0) //SPAWN CHEST
                  {

                          random2 = Random.Range(0, 4);
                  
                          Instantiate(ChestPrefab, Spawn[random2].transform.position, Quaternion.identity);
                          SoMa.PlayChestSpawn();


                          Spawned = true;
                  }
             }
            
            
        }

    }
}
