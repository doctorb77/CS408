﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Script : MonoBehaviour
{

    public float sky_rotate_speed;

    public GameObject panel_main;
    public GameObject panel_host;
    public GameObject panel_find;
    public GameObject panel_scores;
    public GameObject panel_options;
    public GameObject panel_credits;
    public VideoGlitches.VideoGlitchSpectrumOffset glitch_script;
    public float glitch_length;

    public GameObject panel_curr; //current panel

    /*******************************************************************************/
    /*******************************************************************************/
    //ALL THAT YOU NEED TO EDIT IS HERE (YOU DON'T HAVE TO EDIT ANYTHING ELSE IN THIS UNITY PROJECT):

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
    }

    //this function gets an ip address, checks if the format is valid (e.g. "192.168.137.1" is okay but "sh2@hd" is not okay)
    //and returns true for valid or false for invalid.
    public bool panel_find_clean_address(string address)
    {
        //You will have to edit this
        /*if (address is correct format)
        {
            return true;
        }
        else
        {
            return false;
        }*/

        return true;
    }

    //this function will attempt to join a game given an address. If fails then do nothing.
    public bool panel_find_start(string address)
    {
        //Add your code to switch to the game that was found given the address
        //If game cannot be found then do nothing and let this function return false.

        return true;
    }

    /*******************************************************************************/
    /*******************************************************************************/

    void Start()
    {
        panel_curr = panel_main; //set main panel to be the current panel
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * sky_rotate_speed); //rotate background
    }

    public void button_back() //back from any panel (except main)
    {
        panel_curr.SetActive(false);
        panel_main.SetActive(true);
        panel_curr = panel_main;

        glitch();
    }

    public void buttons_main_menu(int i)
    {
        if (i < 5) //Any button except Quit was pressed
        {
            panel_main.SetActive(false);
        }

        switch (i)
        {
            case 0: //Host
                panel_host.SetActive(true);
                panel_curr = panel_host;
                break;
            case 1: //Find
                panel_find.SetActive(true);
                panel_curr = panel_find;
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
    }

    IEnumerator glitch_stop()
    {
        yield return new WaitForSeconds(glitch_length);
        glitch_script.enabled = false;
    }
}