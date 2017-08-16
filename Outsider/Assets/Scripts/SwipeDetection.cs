using UnityEngine;
using System.Collections;

public class SwipeDetection : MonoBehaviour {

    public float maxTime;
    public float minSwipeDist;

    float startTime;
    float endTime;

    Vector3 startPos;
    Vector3 endPos;
    float swipeDistance;
    float swipeTime;

    public GameObject player;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTime = Time.time;
                endPos = touch.position;

                swipeDistance = (endPos - startPos).magnitude;
                swipeTime = endTime - startTime;

                if (swipeTime < maxTime && swipeDistance > minSwipeDist)
                {
                    swipe();
                }
            }
        }
    }

    void swipe()
    {
        Vector2 distance = endPos - startPos;

        if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            Debug.Log("horizontal swipe");

            if (distance.x > 0)
            {
                Debug.Log("derecha");
                player.GetComponent<PlayerMovement>().Derecha();
            }
            if (distance.x < 0)
            {
                player.GetComponent<PlayerMovement>().Izquierda();
                Debug.Log("izquierda");
            }
        }
        else if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        {
            Debug.Log("vertical swipe");

            if (distance.y > 0)
            {
                Debug.Log("arriba");
                player.GetComponent<PlayerMovement>().Arriba();
            }
            if (distance.y < 0)
            {
                Debug.Log("abajo");
                player.GetComponent<PlayerMovement>().Abajo();
            }
        }
    }
}
