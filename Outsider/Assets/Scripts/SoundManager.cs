using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    public AudioSource Song;

    public AudioSource[] ShotgunSound;
    public AudioSource ChestOpen;
    public AudioSource ChestSpawn;
    public AudioSource Swish;
    public AudioSource[] ZombieSounds;
    public AudioSource PickupCoin;
    public AudioSource ButtonTouch;
    public AudioSource BuyError;
    public AudioSource WesternWhistle;

    // Use this for initialization
    void Start () {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void PlayShotgunSound()
    {
        int randomIndex = Random.Range(0, 3);
        ShotgunSound[randomIndex].Play();
    }

    public void PlayChestOpenSound()
    {
        ChestOpen.Play();
    }   

    public void PlayChestSpawn()
    {
        ChestSpawn.Play();
    }

    public void PlaySwish()
    {
        Swish.Play();
    }

    public void PlayZombiesSounds()
    {
        int randomIndex = Random.Range(0, 3);
        ZombieSounds[randomIndex].Play();
    }

    public void PlayPickUpCoin()
    {
        PickupCoin.Play();
    }
    
    public void PlayButtonTouch()
    {
        ButtonTouch.Play();
    }

    public void PlayBuyError()
    {
        BuyError.Play();
    }

    public void PlayWesternWhistle()
    {      
            StartCoroutine(VolumenCancion());                
    }
    private IEnumerator VolumenCancion()
    {
        Song.volume = 0.2f;
        yield return new WaitForSeconds(0.1f);
        Song.volume = 0.0f;
        yield return new WaitForSeconds(0.1f);
        WesternWhistle.Play();
        yield return new WaitForSeconds(2.7f);
        Song.volume = 0.3f;
        yield return new WaitForSeconds(0.1f);
        Song.volume = 0.5f;

    }
}
