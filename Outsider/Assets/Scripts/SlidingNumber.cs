using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlidingNumber : MonoBehaviour {

    
    public Text GoldTxt;
    public float AnimationTime = 1f;

    private float DesiredNumber, InitialNumber, CurrentNumber;

    public void AddNumber(float value)
    {
        InitialNumber = SaveManager.Instance.state.TotalGold;
        InitialNumber = CurrentNumber;
        DesiredNumber = value;
    }

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
        if (InitialNumber !=DesiredNumber)
        {
            if (InitialNumber < DesiredNumber)
            {
                CurrentNumber += (AnimationTime * Time.deltaTime) * (DesiredNumber - InitialNumber);
                if (CurrentNumber>=DesiredNumber)
                {
                    CurrentNumber = DesiredNumber;
                }
            }
            else
            {
                CurrentNumber -= (AnimationTime * Time.deltaTime) * (InitialNumber - DesiredNumber);
                if (CurrentNumber <= DesiredNumber)
                {
                    CurrentNumber = DesiredNumber;
                }
            }

            GoldTxt.text = "$" + CurrentNumber.ToString("0");
        }
	}
}
