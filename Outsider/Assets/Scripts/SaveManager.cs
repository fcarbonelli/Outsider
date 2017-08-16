using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour {

	public static SaveManager Instance { set; get; }
    public SaveState state;

    public void Awake()
    {
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
        Instance = this;
        Load();

        Debug.Log(Helper.Serialize<SaveState>(state));
    }

    //SAVE
    public void Save()
    {
        PlayerPrefs.SetString("save",Helper.Serialize<SaveState>(state));
    }

    //LOAD
    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState(); //state.TotalGold = 2272; //BORRAR <----
            Save();
            Debug.Log("Creating a save file");
        }
    }

    //RESET
    public void Reset()
    {
        PlayerPrefs.DeleteKey("save");       
    }

    //SUMAR ORO GANADO
    public void SumarOroGanado(int OroGanado)
    {
        state.TotalGold += OroGanado;
        Save();
        Debug.Log(state.TotalGold);
    }

    public int OroTotal(int oroPartida)
    {
        int temp = state.TotalGold + oroPartida;

        return temp;
    }

    //RETORNAR ORO
    public int RetornarOro() { return state.TotalGold; }

    //GUARDAR HIGHSCORE
    public bool SaveScore(int ScoreGanado)
    {
        if (ScoreGanado > state.Highscore)
        {
            state.Highscore = ScoreGanado;
            Save();
            return true;
        }
        else
        {
            return false;
        }

    }

    //RETORNAR SI PUEDO COMPRAR
    public bool RetornarCanBuy() { return state.CanBuy; }
    public bool SetCanBuy(bool var)
    {
        if (var == true)
        {
            state.CanBuy = true;
            Save();
            return true;
        }
        else
        {
            state.CanBuy = false;
            Save();
            return false;
        }
    }

    //GUARDAR SONIDO MUTED
    public bool IsMuted(bool var)
    {
        if (var == true)
        {
            state.MuteAudio = true;
            Save();
            return true;
        }
        else
        {
            state.MuteAudio = false;
            Save();
            return false;
        }
    }

    //    -STORE-

    // MEJORAS: VIDA
    public bool ComprarMejoraVida(int Costo)
    {
        if (state.TotalGold >= Costo)
        {
            state.TotalGold -= Costo; // resto el costo

            state.MejoraStoreVida++; //sumo al total comprado

            Save();

            return true;
        }
        else
        {
            return false;
        }
    }

    public int RetornarMejoraVida() { return state.MejoraStoreVida; }

    //MEJORAS: CANTIDAD CHEST
    public bool ComprarMejoraCantChest(int Costo)
    {
        if (state.TotalGold >= Costo)
        {
            state.TotalGold -= Costo; // resto el costo

            state.MejoraStoreCantChest++; //sumo al total comprado

            Save();

            return true;
        }
        else
        {
            return false;
        }
    }

    public int RetornarMejoraCantChest() { return state.MejoraStoreCantChest; }

    //MEJORAS: LOOT COFRES
    public bool ComprarMejoraLoot(int Costo)
    {
        if (state.TotalGold >= Costo)
        {
            state.TotalGold -= Costo; // resto el costo

            state.MejoraStoreLoot++; //sumo al total comprado

            Save();

            return true;
        }
        else
        {
            return false;
        }
    }

    public int RetornarMejoraLoot() { return state.MejoraStoreLoot; }

    //MEJORAS: DROPS ZOMBIES
    public bool ComprarMejoraDrops(int Costo)
    {
        if (state.TotalGold >= Costo)
        {
            state.TotalGold -= Costo; // resto el costo

            state.MejoraStoreDrops++; //sumo al total comprado

            Save();

            return true;
        }
        else
        {
            return false;
        }
    }

    public int RetornarMejoraDrops() { return state.MejoraStoreDrops; }


    //COMPRAS PERSONAJES
    public bool EstaComprado(int model)
    {
        if (state.ModelComprado[model] == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetCompraModel(int model, int Costo)
    {
        state.ModelComprado[model] = true;

        state.TotalGold -= Costo;

        Save();
    }
    public void SetCurrentModel(int model)
    {
        state.currentModel = model;
        Save();
    }
}
