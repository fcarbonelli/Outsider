using UnityEngine;
using System.Collections;


public class BulletScript : MonoBehaviour {

    public float speed;
    public PlayerMovement player;
    public GameObject blood;
    public ParticleSystem sistem;
    
    private Vector3 position;

    

    // Use this for initialization
    void Start () {
        

        player = FindObjectOfType<PlayerMovement>();
        speed = 10;
        if (player.lado ==0)
        {
            position = new Vector3(0, 6, 0);
        }
        if (player.lado ==1)
        {
            position = new Vector3(0, -6, 0);
        }
        if (player.lado == 2)
        {
            position = new Vector3(-4, 0, 0);
        }
        if (player.lado == 3)
        {
            position = new Vector3(4, 0, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {

        
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {       
        if (other.tag == "enemy")
        {
            Instantiate(sistem, gameObject.transform.position, Quaternion.identity);
            
            Destroy(other.gameObject);
            Destroy(gameObject);

            
            player.GetComponent<PlayerMovement>().SumarPuntos(gameObject.transform.position);
        }
    }
    
    
}
