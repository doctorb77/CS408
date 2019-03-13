using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Music for main menu "Space Atmosphere"
//Copyright/Attribution Notice: 
//Alexandr Zhelanov, https://soundcloud.com/alexandr-zhelanov

public class MainMenu_Script : MonoBehaviour
{

    public AudioSource menuMusic;
    public AudioSource buttonClick;

    public GameObject maps;
    public Dropdown m_Dropdown;


    public float sky_rotate_speed;

    public GameObject panel_main;
    public GameObject panel_host;
    public GameObject panel_scores;
    public GameObject panel_options;
    public GameObject panel_credits;
    public GameObject panel_tutorial;
    public GameObject panel_submitScores;
    public VideoGlitches.VideoGlitchSpectrumOffset glitch_script;
    public float glitch_length;

    public GameObject panel_curr; //current panel

    //Do not edit these arrays here, manually edit them in the Unity inspector
    //All these arrays are size n = number of prebuilt maps (you'll have to specify that)
    public int[] panel_host_map_size; //map_size[0] = 15 means that map 1 has size 15
    public Sprite[] panel_host_map_pic; //map_pic[0] = [some image] means that map 1 has the pic [some image].

    //this function will activate when a game is hosted
    public void panel_host_start(string name, int size, bool online, bool generated)
    {
        //name = name of the map
        //size = size of the map
        //online: true if online, false if hotseat
        //generated: true if map was generated, false if map was prebuilt
        string mode;
        string gen;
        if (online) { mode = "online "; } else { mode = "hotseat "; }
        if (generated) { gen = "generated "; } else { gen = "prebuilt "; }
        string msg = "Start hosting a " + gen + mode + "game using map " + name + " with size " + size + "!";
        Debug.Log(msg);

        //Add your code here to start hosting the game. Parameters are already given for you to use as you please.

        PlayerPrefs.SetString("mapname", name);

        SceneManager.LoadScene("boardScene");

        m_Dropdown = maps.GetComponent<Dropdown>();
        PlayerPrefs.SetInt("mapID", m_Dropdown.value);

    }

    void Start()
    {
        if (menuMusic.isPlaying == false)
            menuMusic.Play(0);

        if (PlayerPrefs.GetInt("score") != 0 && PlayerPrefs.HasKey("gameWasPlayed") && PlayerPrefs.GetInt("gameWasPlayed") == 1)
        {
            PlayerPrefs.SetInt("gameWasPlayed", 0);
            panel_main.SetActive(false);
            panel_submitScores.SetActive(true);
            panel_curr = panel_submitScores;
        }
        else if (PlayerPrefs.GetInt("score") == 0 && PlayerPrefs.HasKey("scoreWasSubmitted") && PlayerPrefs.GetInt("scoreWasSubmitted") == 1)
        {
            Debug.Log("testing after score scene!");
            PlayerPrefs.SetInt("scoreWasSubmitted", 0);
            panel_submitScores.SetActive(false);
            panel_main.SetActive(false);
            panel_scores.SetActive(true);
            panel_curr = panel_scores;
        } 
        else
        {
            PlayerPrefs.SetInt("score", 0);
            panel_curr = panel_main; //set main panel to be the current panel
        }
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * sky_rotate_speed); //rotate background
    }

    public void button_back() //back from any panel (except main)
    {
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("gameWasPlayed", 0);

        panel_curr.SetActive(false);
        panel_main.SetActive(true);
        panel_curr = panel_main;

        glitch();
    }

    public void buttons_main_menu(int i)
    {
        if (i < 5 || i == 7) //Any button except Quit was pressed
        {
            panel_main.SetActive(false);
        }

        switch (i)
        {
            case 0: //Host
                panel_host.SetActive(true);
                panel_curr = panel_host;
                break;
            case 2: //Scores
                panel_scores.SetActive(true);
                panel_curr = panel_scores;
                break;
            case 3: //Options
                panel_options.SetActive(true);
                panel_curr = panel_options;
                break;
            case 4: //Credits
                panel_credits.SetActive(true);
                panel_curr = panel_credits;
                break;
            case 5: //Quit
                Application.Quit();
                break;
            case 6: //Tutorial
                panel_tutorial.SetActive(true);
                panel_curr = panel_tutorial;
                break;
            default:
                Debug.LogError("CRITICAL ERROR: buttons_main_menu()");
                break;
        }

        glitch();
    }

    public void glitch()
    {
        glitch_script.enabled = true;
        StartCoroutine(glitch_stop());
        buttonClick.Play(0);
    }

    IEnumerator glitch_stop()
    {
        yield return new WaitForSeconds(glitch_length);
        glitch_script.enabled = false;
    }
}
