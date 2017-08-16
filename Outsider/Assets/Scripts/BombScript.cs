using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

    int TapCount;
    public float MaxDubbleTapTime;
    float NewTime;
    

    void Start()
    {
        TapCount = 0;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                TapCount += 1;
            }

            if (TapCount == 1)
            {

                NewTime = Time.time + MaxDubbleTapTime;
            }
            else if (TapCount == 2 && Time.time <= NewTime)
            {

                //Whatever you want after a dubble tap    
                Debug.Log("TAP");


                TapCount = 0;
            }

        }
        if (Time.time > NewTime)
        {
            TapCount = 0;
        }
    }
}