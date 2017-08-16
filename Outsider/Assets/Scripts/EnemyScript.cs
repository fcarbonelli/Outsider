using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float speed;
    
    private Vector3 position;

   
    // Use this for initialization
    void Start () {
        speed = 2.6f;
       
    }
	
	// Update is called once per frame
	void Update ()
    {
       
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,0,0), speed * Time.deltaTime);
    }
    
   
}
