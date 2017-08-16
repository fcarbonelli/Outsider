using UnityEngine;
using System.Collections;

public class FloatingObj : MonoBehaviour {

    public float MoveSpeed;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x, transform.position.y + (MoveSpeed * Time.deltaTime), transform.position.z);

    }
}
