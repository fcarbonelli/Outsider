using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
    float spawnTimer = 1.1f;
    float timer = 0f;
    int randomSpawn, randomEvent, evento;
    public GameObject[] enemy, enemyNinja, Churchenemy;
    public Transform[] Direccion;

    
   
    int cont,contspawn;
    bool cantSpawn = true;

    // Use this for initialization
    void Start() {
        cont = 0;
        contspawn = 0;

        StartCoroutine(StartGameSpawns());

    }

    public void Awake()
    {
        CambiarZombies();
    }
    
    public void CambiarZombies()
    {
        //NINJA MODEL
        if (SaveManager.Instance.state.currentModel == 3)
        {
            for (int i = 0; i < 4; i++)
            {
                enemy[i] = enemyNinja[i];
                
            }
        }

        //ILUMIINATI MODEL
        if (SaveManager.Instance.state.currentModel == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                enemy[i] = Churchenemy[i];

            }
        }
    }

    // Update is called once per frame
    void Update() {


        timer += Time.deltaTime;
        if (timer >= spawnTimer)
        {

            randomSpawn = Random.Range(0, 4);
            randomEvent = Random.Range(0,3);
            
           
               
            if (randomEvent == 0 && contspawn > 2)
            {
                cantSpawn = false;

                evento = Random.Range(0,5);
                switch (evento)
                {
                    case 0:
                        StartCoroutine(SpawnFilas(randomSpawn));
                        break;
                    case 1:
                        StartCoroutine(SpawnDosLados());                       
                        break;
                    case 2:
                        StartCoroutine(SpawnTodosLados());
                        break;
                    case 3:
                        StartCoroutine(SpawnDosLados());
                        break;
                    case 4:
                        StartCoroutine(SpawnDosTandas());
                        break;
                }

                contspawn = 0;
                 
                
            }
            else if(cantSpawn == true )
            {
                               
                Instantiate(enemy[randomSpawn], Direccion[randomSpawn].transform.position, Quaternion.identity);
                contspawn++;
                
            }

            
             cont++;


            if (cont>20)
            {
                cont = 0;
                //limitar velocidad
                if (spawnTimer>=0.4f)
                {
                     spawnTimer = spawnTimer - 0.1f;
                }
               
            }

            timer = 0f;


        }

        
    }

    private IEnumerator StartGameSpawns()
    {
        cantSpawn = false;

        yield return new WaitForSeconds(2f);
        Instantiate(enemy[1], Direccion[1].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Instantiate(enemy[0], Direccion[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Instantiate(enemy[2], Direccion[2].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(enemy[0], Direccion[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(enemy[3], Direccion[3].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(enemy[0], Direccion[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);


        cantSpawn = true;
        
    }

    private IEnumerator SpawnFilas(int lado)
    {
        lado = lado - 1;
        if (lado-1 < 0)
        {
            lado = lado * -1;
        }
        int ran = Random.Range(3,5);
        for (int i = 0; i < ran; i++)
        {
            Instantiate(enemy[lado], Direccion[lado].transform.position, Quaternion.identity);

            yield return new WaitForSeconds(0.4f);
        }

        cantSpawn = true;

       
    }
    private IEnumerator SpawnTodosLados()
    {   
        yield return new WaitForSeconds(0.3f);
        Instantiate(enemy[0], Direccion[0].transform.position, Quaternion.identity);
        Instantiate(enemy[2], Direccion[2].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        Instantiate(enemy[1], Direccion[1].transform.position, Quaternion.identity);
        Instantiate(enemy[3], Direccion[3].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        cantSpawn = true;
    }

    private IEnumerator SpawnDosLados()
    {
        yield return new WaitForSeconds(0.1f);
        int random = Random.Range(0,3);
        Instantiate(enemy[random], Direccion[random].transform.position, Quaternion.identity);
        Instantiate(enemy[random+1], Direccion[random+1].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);

        cantSpawn = true;
    }

    private IEnumerator SpawnDosTandas()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(enemy[0], Direccion[0].transform.position, Quaternion.identity);
        Instantiate(enemy[1], Direccion[1].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(enemy[2], Direccion[2].transform.position, Quaternion.identity);
        Instantiate(enemy[3], Direccion[3].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);

        cantSpawn = true;
    }
}
