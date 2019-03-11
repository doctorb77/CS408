using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Script : MonoBehaviour
{
    public AudioSource buttonClick;

    public Sprite[] tutorial_panels;
    public Image current_panel;
    public int num_panels; //important, set this to the amount of panels you plan to have.
    int current_panel_index;

    // Start is called before the first frame update
    void Start()
    {
        current_panel_index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void next()
    {
        buttonClick.Play(0);

        current_panel_index++;

        if (current_panel_index == num_panels)
        {
            current_panel_index = 0;
        }

        current_panel.sprite = tutorial_panels[current_panel_index];
    }

    public void prev()
    {
        buttonClick.Play(0);

        current_panel_index--;

        if (current_panel_index == -1)
        {
            current_panel_index = num_panels-1;
        }

        current_panel.sprite = tutorial_panels[current_panel_index];
    }
}
