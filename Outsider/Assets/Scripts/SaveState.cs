

public class SaveState
{
    public int TotalGold = 0;
    public int goldObtenido;

    public int Highscore = 0; 

    public int MejoraStoreVida = 0; //marca el nivel de mejora. 0 = nada comprado 1=una mejora...
    public int MejoraStoreCantChest = 0;
    public int MejoraStoreLoot = 0;
    public int MejoraStoreDrops = 0;

    public bool CanBuy = false; //FIX

    public int currentModel = 0; //cada int es un personaje
    public bool[] ModelComprado = new bool[5]; //CAMBIAR EL NUMERO A LA CANTIDAD DE MODELS QUE EXISTAN!!

    public bool MuteAudio;
	
}
