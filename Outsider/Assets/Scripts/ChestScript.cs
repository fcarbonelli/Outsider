using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    public ChestSpawn Spawned;
    public SoundManager SoMa;

    public GameObject pickupCoins;

    PlayerMovement PLAYER;

    bool Touched;

    // Use this for initialization
    void Start()
    {
        Spawned = FindObjectOfType<ChestSpawn>();
        SoMa = GameObject.FindObjectOfType(typeof(SoundManager)) as SoundManager;
        PLAYER = FindObjectOfType<PlayerMovement>();
        Touched = false;
    }

    // Update is called once per frame
    void Update()
    {
     if (Touched == false)
      {

        //--TOUCH INPUT--
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
              {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
                   if (hit.collider && hit.collider.tag == "Chest")
                   {
                       StartCoroutine(OpenChest());
                   }
             }
        }
        //---------------
       /*
        //--PC INPUT--
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider && hit.collider.tag == "Chest")
            {
                StartCoroutine(OpenChest());
            }
        }
        //---------
        */
    }
        
    }
    private IEnumerator OpenChest()
    {
        var animator = gameObject.GetComponent<Animator>();
        Spawned.Spawned = false;
        SoMa.PlayChestOpenSound();
        animator.SetTrigger("ChestOpen");
        Touched = true;

         createNumber();

        yield return new WaitForSeconds(1.2f);
        
        Destroy(gameObject);
        
    }

    public void createNumber()
    {
        int randomCoins = Random.Range(2, 6 + SaveManager.Instance.RetornarMejoraLoot());
        var clone = (GameObject)Instantiate(pickupCoins, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingNumbers>().cantCoins = randomCoins;

        PLAYER.SumarMonedas(randomCoins);
    }
}
