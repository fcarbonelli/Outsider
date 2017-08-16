using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

    private GameObject[] characterList;
    private int index = SaveManager.Instance.state.currentModel;
    public string[] nombres;
    public int[] precio;
    public GameObject ButCompras, ButConfirmar;
    public GameObject Candado, Tick;
    public Text preciotxt, nombresTxt, selectTxt, indicadorTxt;

	private void Start ()
    {
        SaveManager.Instance.SetCompraModel(0, 0);

        characterList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        // CAMBIAR A CURRENT CHARACTER
        if (characterList[SaveManager.Instance.state.currentModel])
        {
            characterList[SaveManager.Instance.state.currentModel].SetActive(true);
        }
	}

    public void Update()
    {
        nombresTxt.text = nombres[index].ToString();
        Tick.SetActive(false);
        indicadorTxt.text = (index+1).ToString() + "/" + transform.childCount.ToString();

        if (SaveManager.Instance.EstaComprado(index))
        {
            //deshabilitar boton compra y set activa false un candado
            ButCompras.SetActive(false);
            ButConfirmar.SetActive(true);
            Candado.SetActive(false);

            preciotxt.text = "PURCHASED";

            if (index == SaveManager.Instance.state.currentModel)
            {
                selectTxt.text = "SELECTED";
                Tick.SetActive(true);
            }
            else
            {
                selectTxt.text = "SELECT";
                
            }
        }
        else
        {
            ButCompras.SetActive(true);
            ButConfirmar.SetActive(false);
            Candado.SetActive(true);

            preciotxt.text = "$" + precio[index];

        }
    }

    public void Izquierda()
    {
        characterList[index].SetActive(false);
        
        index--;
        if (index < 0)
        {
            index = characterList.Length - 1;
        }
        
        characterList[index].SetActive(true);
    }
    public void Derecha()
    {
        characterList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
        {
            index = 0;
        }

        characterList[index].SetActive(true);
    }

    public void Comprar()
    {
        if (SaveManager.Instance.state.TotalGold >= precio[index])
        {
            SaveManager.Instance.SetCompraModel(index, precio[index]);

            SlidingNumber slide = FindObjectOfType<SlidingNumber>();
            slide.AddNumber(SaveManager.Instance.state.TotalGold);
        }
    }

    public void Confirmar()
    {
        SaveManager.Instance.SetCurrentModel(index);
    }

}
