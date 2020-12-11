using UnityEngine.SceneManagement;
using UnityEngine;
using Lean.Gui;

public class MenuManager : MonoBehaviour
{
    public GameObject GameSoundObject,GS;
    public GameObject MainCanvas,OptionCanvas;
    public LeanToggle MusicToggle;
    public AudioSource GameAudio;
    public GameObject[] RadioButtons;
    private void Start()
    {
        GS = Instantiate(GameSoundObject,new Vector3(0f, 0f, 0f),Quaternion.identity);
        GameAudio = GS.GetComponent<AudioSource>();
        DontDestroyOnLoad(GS);
        if (PlayerPrefs.GetInt("MUSIC", 1) == 1)
        {
            MusicToggle.TurnOn();
            GameAudio.Play();
        }
        else
        {
            MusicToggle.TurnOff();
            GameAudio.Stop();
        }

        string quality = PlayerPrefs.GetString("QS","High");
        string[] names = QualitySettings.names;
        Debug.Log(quality);
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i] == quality)
            {
                RadioButtons[i].GetComponent<LeanToggle>().TurnOn();
                RadioButtons[i].GetComponent<LeanButton>().interactable = false;
                QualitySettings.SetQualityLevel(i, true);
            }
        }
    }
    public void OnPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnOption()
    {
        MainCanvas.SetActive(false);
        OptionCanvas.SetActive(true);
    }
    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnOptionMenuExit()
    {
        MainCanvas.SetActive(true);
        OptionCanvas.SetActive(false);
    }

    public void OnMusicOption(bool on)
    {
        int option;
        if (on)
        {
            option = 1;
            GameAudio.Play();
        }
        else
        {
            option = 0;
            GameAudio.Stop();
        }
        PlayerPrefs.SetInt("MUSIC",option);
    }

    public void OnQualitySlection(int no)
    {
        RadioButtons[0].GetComponent<LeanButton>().interactable = true;
        RadioButtons[1].GetComponent<LeanButton>().interactable = true;
        RadioButtons[2].GetComponent<LeanButton>().interactable = true;
        RadioButtons[no].GetComponent<LeanButton>().interactable = false;

        string[] names = QualitySettings.names;
        PlayerPrefs.SetString("QS", names[no]);
        QualitySettings.SetQualityLevel(no,true);
    }
}
