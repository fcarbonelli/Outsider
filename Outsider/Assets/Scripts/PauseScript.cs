using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public bool paused;
    GameObject chestSpawned;
    public GameObject SwipeOff;

    void Start()
    {
        paused = false;
    }


    public void Pause()
    {
       
        paused = !paused;
        
        if (paused)
        {
            Time.timeScale = 0;

            SwipeOff.SetActive(false);

           if(GameObject.FindWithTag("Chest") != null)
            {
                chestSpawned = GameObject.FindWithTag("Chest");
                 chestSpawned.SetActive(false);
            }      
               
            
        }


        else if (!paused)
        {
            Time.timeScale = 1;

            SwipeOff.SetActive(true);

            chestSpawned.SetActive(true);
            
        }
    }
}
