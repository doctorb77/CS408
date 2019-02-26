using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindGame_Script : MonoBehaviour
{

    public MainMenu_Script menu;

    public InputField address;

    public GameObject error_panel;
    public Text error_msg;

    bool bad_ip;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //search for game button is clicked
    public void search()
    {
        if (menu.panel_find_clean_address(address.text))
        {
            //IP Address is acceptable (NOTE: "acceptable" as in correct format, doesn't mean that ip address is found)
            //continue...
            search_cont();
            bad_ip = true;
        }
        else
        {
            //IP Address is not acceptable (NOTE: "unacceptable" as in incorrect format, doesn't mean that ip address is not found)
            error_not_address();
        }       
    }

    public void search_cont()
    {
        //continue...

        if (menu.panel_find_start(address.text))
        {
            //This code block will never be reached,
            //because if menu.panel_find_clean_address(address.text) is true,
            //then the game would already have switched
        }
        else
        {
            //Cannot find Game!
            error_cannot_find();
        }
    }

    public void error_not_address()
    {
        error_msg.text = "NOT AN IP ADDRESS!";
        error_panel.SetActive(true);
    }

    public void error_cannot_find()
    {
        error_msg.text = "CANNOT FIND GAME!";
        error_panel.SetActive(true);
    }

    public void back()
    {
        error_panel.SetActive(false);
    }
}
