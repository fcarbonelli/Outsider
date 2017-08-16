using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour {

    public float MoveSpeed;
    public int cantCoins;
    public Text displayNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        displayNumber.text = "+" + cantCoins;
        transform.position = new Vector3(transform.position.x, transform.position.y + (MoveSpeed * Time.deltaTime), transform.position.z);
	}
}
