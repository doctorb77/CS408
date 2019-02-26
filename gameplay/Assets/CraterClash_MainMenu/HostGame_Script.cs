using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostGame_Script : MonoBehaviour
{
    public Slider slider;
    public int int_map_size;
    public Text text_map_size;
    public Dropdown dropdown_map;
    public Text text_dropdown_map_name;
    public Text text_map_size_2;
    public Image image_current_map;
    public Sprite image_generated_map;

    public MainMenu_Script main;

    public bool generated;

    void Start()
    {
        generated = false;
        int_map_size = main.panel_host_map_size[0];

        setSliderValue();
        slider.onValueChanged.AddListener(delegate { setSliderValue(); });

        setMapName(dropdown_map);
        dropdown_map.onValueChanged.AddListener(delegate { setMapName(dropdown_map); });
    }

    void Update()
    {
        
    }

    //send parameters to start an online game
    public void online()
    {
        if (generated)
        {
            main.panel_host_start(text_dropdown_map_name.text, int_map_size, true, true);
        }
        else
        {
            main.panel_host_start(text_dropdown_map_name.text, int_map_size, true, false);
        }
    }

    //send parameters to start a hotseat game
    public void hotseat()
    {
        if (generated)
        {
            main.panel_host_start(text_dropdown_map_name.text, int_map_size, false, true);
        }
        else
        {
            main.panel_host_start(text_dropdown_map_name.text, int_map_size, false, false);
        }
    }

    //Invoked when the value of the slider changes.
    public void setSliderValue()
    {
        text_map_size.text = "Size: " + (int)slider.value;
    }

    //Invoked when the map dropdown value is changed.
    public void setMapName(Dropdown change)
    {
        generated = false;
        text_dropdown_map_name.text = "\"" + dropdown_map.options[change.value].text + "\"";
        int_map_size = main.panel_host_map_size[change.value];
        text_map_size_2.text = "Size: " + main.panel_host_map_size[change.value];
        image_current_map.sprite = main.panel_host_map_pic[change.value];
    }

    //When user presses Generate
    public void generate()
    {
        generated = true;
        text_dropdown_map_name.text = "\"GENERATED MAP\"";
        int_map_size = (int)slider.value;
        text_map_size_2.text = "Size: " + int_map_size;
        image_current_map.sprite = image_generated_map;
    }
}
