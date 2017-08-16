using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public GameObject projectilePrefab, NinjaStarPrefab, FireballPrefab, LaserPrefab;
    public GameObject gameoverCanvas, CanBuySprite;
    public GameObject BloodParticle;
    public GameObject HighscoreTXT;
   
    private Vector3 startPos, EndPos;

    public int lado;

    public Text TxtPuntos, TxtMonedas,TxtPuntosGanados,TxtHighscore, TxtGold, TxtBestInGame, TxtAddMonedas;
    int puntos = 0;
    int randomCoin;
    public int MonedasObtenidas;
    public GameObject pickupCoins;

    public GameObject cowSprite;
    public Sprite[] PlayerLados, MonkLados, ChewbaccaLados, NinjaLados, illuminatiLados;

    SoundManager SoMa;

    public Image[] VidaSprite;
    public Sprite VidaPerdida, VidaNula;
    int Vidas;
    bool gameover = false;

    GameObject[] gameEnemys;

    public SaveState state;

    public void Awake()
    {
        ColocarVidas();
        ColocarBest();
        CambiarPlayerModel();
    }
    public void ColocarVidas()
    {
        Vidas = 3 + SaveManager.Instance.RetornarMejoraVida();
        for (int i = 0; i < Vidas; i++)
        {            
                VidaSprite[i].sprite = VidaNula;
             
        }
    }
    public void ColocarBest()
    {
        int temp = SaveManager.Instance.state.Highscore;
        TxtBestInGame.text = temp.ToString();
    }

    public void CambiarPlayerModel()
    {
        //MONK MODEL
        if (SaveManager.Instance.state.currentModel == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerLados[i] = MonkLados[i];
                cowSprite.GetComponent<SpriteRenderer>().sprite = MonkLados[1];
            }
        }

        //CHEWBACCA MODEL
        if (SaveManager.Instance.state.currentModel == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerLados[i] = ChewbaccaLados[i];
                projectilePrefab = LaserPrefab;
                cowSprite.GetComponent<SpriteRenderer>().sprite = ChewbaccaLados[1];
            }
        }

        //NINJA MODEL
        if (SaveManager.Instance.state.currentModel == 3)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerLados[i] = NinjaLados[i];
                projectilePrefab = NinjaStarPrefab;
                cowSprite.GetComponent<SpriteRenderer>().sprite = NinjaLados[1];
            }
        }

        //ILLUMINATI MODEL
        if (SaveManager.Instance.state.currentModel == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerLados[i] = illuminatiLados[i];
                projectilePrefab = FireballPrefab;
                cowSprite.GetComponent<SpriteRenderer>().sprite = illuminatiLados[1];

            }
        }

    }

    // Use this for initialization
    void Start () {
        
        SoMa = GameObject.FindObjectOfType(typeof(SoundManager)) as SoundManager;

    }
	
	// Update is called once per frame
	void Update () {

                     // CONTROLES PC

        //IZQUIERDA
        if (Input.GetKeyDown(KeyCode.A))
        {
            Izquierda();   
        }

        //DERECHA
        if (Input.GetKeyDown(KeyCode.D))
        {
            Derecha();
        }

        //ARRIBA
        if (Input.GetKeyDown(KeyCode.W))
        {
            Arriba();
        }

        //ABAJO
        if (Input.GetKeyDown(KeyCode.S))
        {
            Abajo();
        }

        if (Vidas == 0)
        {
            //-----GAME OVER------
            
            if (gameover==false)
            {
                gameoverCanvas.SetActive(true);

               
                TxtPuntosGanados.text = "" + puntos.ToString();
                if (SaveManager.Instance.SaveScore(puntos))
                {
                    //feedback HIGHSCORE
                    HighscoreTXT.SetActive(true);
                }
                TxtHighscore.text = "" + SaveManager.Instance.state.Highscore;

                //GUARDAR ORO
                SaveManager.Instance.state.goldObtenido = MonedasObtenidas;
                int temp = SaveManager.Instance.OroTotal(MonedasObtenidas);
                TxtAddMonedas.text = "+" + MonedasObtenidas.ToString();
                TxtGold.text = "" + (temp - MonedasObtenidas).ToString();
                StartCoroutine(SumaDeMonedas(temp));
                SaveManager.Instance.SumarOroGanado(MonedasObtenidas);

                AdManager.Instance.ShowInterstitial();
               
                CanBuy();
                
                gameover = true;
            }

        }

    }

    public void CanBuy()
    {
        if (SaveManager.Instance.RetornarCanBuy() == true)
        {
            CanBuySprite.SetActive(true);
        }
        else
        {
            CanBuySprite.SetActive(false);
        }
    }

                 // CONTROLES MOBILE

    public void Derecha()
    {
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        cowSprite.GetComponent<SpriteRenderer>().sprite = PlayerLados[0];
        Instantiate(projectilePrefab);
        lado = 3;
        SoMa.PlaySwish();
        SoMa.PlayShotgunSound();
    }

    public void Izquierda()
    {
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180);
        cowSprite.GetComponent<SpriteRenderer>().sprite = PlayerLados[1];
        lado = 2;
        Instantiate(projectilePrefab);
        SoMa.PlaySwish();
        SoMa.PlayShotgunSound();
    }

    public void Arriba()
    {
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90);
        cowSprite.GetComponent<SpriteRenderer>().sprite = PlayerLados[2];
        Instantiate(projectilePrefab);
        lado = 0;
        SoMa.PlaySwish();
        SoMa.PlayShotgunSound();
    }

    public void Abajo()
    {
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 270);
        cowSprite.GetComponent<SpriteRenderer>().sprite = PlayerLados[3];
        Instantiate(projectilePrefab);
        lado = 1;
        SoMa.PlaySwish();
        SoMa.PlayShotgunSound();
    }

    public void SumarPuntos(Vector3 pos)
    {
        puntos++;
        TxtPuntos.text = puntos.ToString();

        randomCoin = Random.Range(0, 5);
        if (randomCoin == 0)
        {
            int RanZomCoin = Random.Range(1,1+SaveManager.Instance.RetornarMejoraDrops());
            SumarMonedas(RanZomCoin);
            SoMa.PlayPickUpCoin();

            var clone = (GameObject)Instantiate(pickupCoins, pos, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().cantCoins = RanZomCoin;

        }
    }

    public void SumarMonedas(int cant)
    {
            MonedasObtenidas += cant;
            TxtMonedas.text = "$ " + MonedasObtenidas.ToString();

        var animator = TxtMonedas.GetComponent<Animator>();
        animator.SetTrigger("GetMoney");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            Destroy(other.gameObject);

            
            if (Vidas > 0)
            {
               StartCoroutine(EnemyShake()); //SHAKE LIFE
               DestroyAllEnemys();       //Destroy zombies alive
               VidaSprite[Vidas-1].sprite = VidaPerdida;
               Instantiate(BloodParticle);

                Vidas--;
                
            
             }   
            

        }
    }

    private IEnumerator EnemyShake()
    {
        float elapsed = 0.0f;
        Image shake = VidaSprite[Vidas - 1];
        Quaternion originalRotation = shake.transform.rotation;

        while (elapsed < 0.3F)
        {

            elapsed += Time.deltaTime;
            float z = Random.value * 30f - (20f / 2);
            shake.transform.eulerAngles = new Vector3(originalRotation.x, originalRotation.y +z, originalRotation.z + z);
            yield return null;
        }

        shake.transform.rotation = originalRotation;
    }

    void DestroyAllEnemys()
    {
        gameEnemys = GameObject.FindGameObjectsWithTag("enemy");

        for (var i = 0; i < gameEnemys.Length; i++)
        {
            Destroy(gameEnemys[i]);
        }
    }

    private IEnumerator SumaDeMonedas(int oro)
    {
        
        yield return new WaitForSeconds(0.6f);
        SlidingNumber slide = FindObjectOfType<SlidingNumber>();
        slide.AddNumber(oro);
    }
   
}
