using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour {

    

    public Text goldTxt;
    public Sprite SpriteCompra;
    public Button buton1, buton2, buton3, buton4;
    public Sprite SpriteVerde, SpriteRojo, SpriteAzul;
    bool CanBuy = false;

    public GameObject canvasMejoras, CanvasSeleccion;

    //VARIABLES SUBIR VIDA
    public Image[] Up_life;    
    public int[] PrecioLife;
    public Text VidaOroTxt;

    //VARIABLES SUBIR SPAWN DE COFRES
    public Image[] Up_Chest;
    public int[] PrecioChest;
    public Text ChestOroTxt;

    //VARIABLES SUBIR LOOT COFRES
    public Image[] Up_Loot;
    public int[] PrecioLoot;
    public Text LootOroTxt;

    //VARIABLES SUBIR DROPS DE ZOMBIES
    public Image[] Up_Drops;
    public int[] PrecioDrops;
    public Text DropsOroTxt;

    SoundManager SoMa;

    void Start()
    {
        SoMa = GameObject.FindObjectOfType(typeof(SoundManager)) as SoundManager;
             
    }


    public void GoToMenu()
    {
        SoMa.PlayButtonTouch();
        SceneManager.LoadScene("Menu");
    }

    public void CambiarPestaña(bool cambio)
    {
        if (cambio)
        {
            canvasMejoras.SetActive(false);
            CanvasSeleccion.SetActive(true);
        }
        else
        {
            canvasMejoras.SetActive(true);
            CanvasSeleccion.SetActive(false);
        }
    }

    public void Awake()
    {
        ActualizarOro();

        ColocarComprasVida();
        ColocarComprasCantChest();
        ColocarComprasLoot();
        ColocarComprasDrops();
    }

    public void ActualizarOro()
    {
       int temp = SaveManager.Instance.RetornarOro();

        //goldTxt.text = "$" + temp.ToString();
        SlidingNumber slide = FindObjectOfType<SlidingNumber>();
        slide.AddNumber(temp);

        
        CanBuy = false;

        //BOTON COMPRA VIDA
        if (SaveManager.Instance.RetornarMejoraVida() < 3)
        {
            VidaOroTxt.text = "$" + PrecioLife[SaveManager.Instance.RetornarMejoraVida()];
            if (temp >= PrecioLife[SaveManager.Instance.RetornarMejoraVida()])
            {
                buton1.image.sprite = SpriteVerde;
                CanBuy = true;              
            }
            else
            {
                buton1.image.sprite = SpriteRojo;
            }
        }
        else
        {
            VidaOroTxt.text = "MAX!";
            buton1.image.sprite = SpriteAzul;
        }

        //BOTON COMPRA CANTIDAD COFRE
        if (SaveManager.Instance.RetornarMejoraCantChest() < 3)
        {
            ChestOroTxt.text = "$" + PrecioChest[SaveManager.Instance.RetornarMejoraCantChest()];
            if (temp >= PrecioChest[SaveManager.Instance.RetornarMejoraCantChest()])
            {
                buton2.image.sprite = SpriteVerde;
                CanBuy = true;
            }
            else
            {
                buton2.image.sprite = SpriteRojo;
            }
        }
        else
        {
            buton2.image.sprite = SpriteAzul;
            ChestOroTxt.text = "MAX!";
        }

        //BOTON COMPRA LOOT
        if (SaveManager.Instance.RetornarMejoraLoot() < 3)
        {
            LootOroTxt.text = "$" + PrecioLoot[SaveManager.Instance.RetornarMejoraLoot()];
            if (temp >= PrecioLoot[SaveManager.Instance.RetornarMejoraLoot()])
            {
                buton3.image.sprite = SpriteVerde;
                CanBuy = true;
            }
            else
            {
                buton3.image.sprite = SpriteRojo;
            }
        }
        else
        {
            buton3.image.sprite = SpriteAzul;
            LootOroTxt.text = "MAX!";
        }

        //BOTON COMPRA DROPS
        if (SaveManager.Instance.RetornarMejoraDrops() < 3)
        {
            DropsOroTxt.text = "$" + PrecioDrops[SaveManager.Instance.RetornarMejoraDrops()];
            if (temp >= PrecioDrops[SaveManager.Instance.RetornarMejoraDrops()])
            {
                buton4.image.sprite = SpriteVerde;
                CanBuy = true;
            }
            else
            {
                buton4.image.sprite = SpriteRojo;
            }
        }
        else
        {
            DropsOroTxt.text = "MAX!";
            buton4.image.sprite = SpriteAzul;
        }

        SaveManager.Instance.SetCanBuy(CanBuy);
        
    }

    public void ColocarComprasVida()
    {
        int temp = SaveManager.Instance.RetornarMejoraVida();
        for (int i = 0; i < temp; i++)
        {
            Up_life[i].sprite = SpriteCompra;

        }        

    }
    public void ColocarComprasCantChest()
    {
        int temp = SaveManager.Instance.RetornarMejoraCantChest();
        for (int i = 0; i < temp; i++)
        {
            Up_Chest[i].sprite = SpriteCompra;

        }
    }
    public void ColocarComprasLoot()
    {
        int temp = SaveManager.Instance.RetornarMejoraLoot();
        for (int i = 0; i < temp; i++)
        {
            Up_Loot[i].sprite = SpriteCompra;

        }
    }
    public void ColocarComprasDrops()
    {
        int temp = SaveManager.Instance.RetornarMejoraDrops();
        for (int i = 0; i < temp; i++)
        {
            Up_Drops[i].sprite = SpriteCompra;

        }
    }

    public void ColocarColoresBotones()
    {

    }

    public void MejoraVida()
    {
        int temp = SaveManager.Instance.RetornarMejoraVida();
        if (SaveManager.Instance.ComprarMejoraVida(PrecioLife[temp]))
        {
           
            Up_life[temp].sprite = SpriteCompra; //cambio el sprite
            ActualizarOro();
            
        }
        else
        {
            Debug.Log("Falta money");
            SoMa.PlayBuyError();
        }
    }

    public void MejoraCantChest()
    {
        int temp = SaveManager.Instance.RetornarMejoraCantChest();
        if (SaveManager.Instance.ComprarMejoraCantChest(PrecioChest[temp]))
        {

            Up_Chest[temp].sprite = SpriteCompra; //cambio el sprite
            ActualizarOro();

        }
        else
        {
            Debug.Log("Falta money");
            SoMa.PlayBuyError();
        }
    }

    public void MejoraLoot()
    {
        int temp = SaveManager.Instance.RetornarMejoraLoot();
        if (SaveManager.Instance.ComprarMejoraLoot(PrecioLoot[temp]))
        {

            Up_Loot[temp].sprite = SpriteCompra; //cambio el sprite
            ActualizarOro();

        }
        else
        {
            Debug.Log("Falta money");
            SoMa.PlayBuyError();
        }
    }

    public void MejoraDrops()
    {
        int temp = SaveManager.Instance.RetornarMejoraDrops();
        if (SaveManager.Instance.ComprarMejoraDrops(PrecioDrops[temp]))
        {

            Up_Drops[temp].sprite = SpriteCompra; //cambio el sprite
            ActualizarOro();

        }
        else
        {
            Debug.Log("Falta money");
            SoMa.PlayBuyError();
        }
    }
}
