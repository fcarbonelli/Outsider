using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public GameObject pergamino,canvas,Hands,Settings, CanBuySprite, CloudPrefab, CanvasTut, PlayerModelTutorial, AdButton, thankText;
    bool SettingOpen, muteSong;
    public AudioSource Song;

    public Button MuteAudioButton, MuteSongButton;
    public Sprite AudioOn, AudioOff, SongOn, SongOff;
    public Sprite[] models;

    SoundManager SoMa;

    public void Awake()
    {
        if (SaveManager.Instance.RetornarCanBuy()==true)
        {
            CanBuySprite.SetActive(true);
        }
        else
        {
            CanBuySprite.SetActive(false);
        }

        if (SaveManager.Instance.state.MuteAudio)
        {           
            AudioListener.volume = 0f;
            MuteAudioButton.image.sprite = AudioOff;
        }
        else if (!SaveManager.Instance.state.MuteAudio)
        {           
            MuteAudioButton.image.sprite = AudioOn;
            AudioListener.volume = 1f;
        }

        PlayerModelTutorial.GetComponent<SpriteRenderer>().sprite = models[SaveManager.Instance.state.currentModel];

        //AdManager.Instance.LoadAd();
    }

    void Start()
    {
        SoMa = GameObject.FindObjectOfType(typeof(SoundManager)) as SoundManager;
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("Outsuder");
    }
    

    public void StartGame()
    {
        SettingOpen = false;
        StartCoroutine(Transicion());
    }

    private IEnumerator Transicion()
    {
        var animator = pergamino.GetComponent<Animator>();
        animator.SetTrigger("TocoPlay");

        var animator2 = canvas.GetComponent<Animator>();
        animator2.SetTrigger("StartTran");

        var animator3 = Hands.GetComponent<Animator>();
        animator3.SetTrigger("HandOut");

        
        SoMa.PlayWesternWhistle();

        yield return new WaitForSeconds(0.4f);

        CanvasTut.SetActive(true);
        Instantiate(CloudPrefab);
        
        yield return new WaitForSeconds(3.5f);

        SceneManager.LoadScene("Outsuder");

    }

    public void OpenSettings()
    {
        if (SettingOpen==false)
        {
            var animatorIn = Settings.GetComponent<Animator>();
            animatorIn.SetTrigger("SettingIn");
            SettingOpen = true;
            SoMa.PlayButtonTouch();

            //TEST
           // SaveManager.Instance.Reset();

        }

    }
    public void CloseSettings()
    {
        if (SettingOpen==true)
        {
            var animatorOut = Settings.GetComponent<Animator>();
            animatorOut.SetTrigger("SettingOut");
            SettingOpen = false;
            SoMa.PlayButtonTouch();

        }

    }

    public void GoToStore()
    {
        
        SceneManager.LoadScene("Store");
        SoMa.PlayButtonTouch();
    }
    public void GoToMenu()
    {
        SoMa.PlayButtonTouch();
        SceneManager.LoadScene("Menu");
        
    }

    
    public void MuteAudio()
    {
        
        if (AudioListener.volume == 0f)
        {
            AudioListener.volume = 1f;
            MuteAudioButton.image.sprite = AudioOn;
            SaveManager.Instance.IsMuted(false);
        }
        else if (AudioListener.volume == 1f)
        {
            AudioListener.volume = 0f;
            SaveManager.Instance.IsMuted(true);
            MuteAudioButton.image.sprite = AudioOff;
        }
        
    }

    public void ShowAd()
    {
        if (AdManager.Instance.ShowVideo())
        {
            int temp = SaveManager.Instance.state.goldObtenido;
            SaveManager.Instance.SumarOroGanado(temp);
            StartCoroutine(SumaDeMonedas(SaveManager.Instance.state.TotalGold));
            AdButton.SetActive(false);
            thankText.SetActive(true);
        }
       
        
    }
    private IEnumerator SumaDeMonedas(int oro)
    {

        yield return new WaitForSeconds(1f);
        SlidingNumber slide = FindObjectOfType<SlidingNumber>();
        slide.AddNumber(oro);
    }
}
