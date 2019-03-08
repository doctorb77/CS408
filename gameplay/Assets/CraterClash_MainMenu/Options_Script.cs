using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Script : MonoBehaviour
{

    public Dropdown dropdown_resolution;
    public Dropdown dropdown_antialiasing;
    public Dropdown dropdown_vsync;
    public Dropdown dropdown_texture;
    public Dropdown dropdown_volume;

    /*
     * Default:
     * Resolution - 1920 x 1080
     * Anti-Aliasing - 8x
     * Vsync - On
     * Texture - Full
     * Volume - On
     */

    //PlayerPref names WARNING: DO NOT CHANGE, UNLESS YOU WANT TO MODIFY PLAYERPREFS
    string resolution_p = "resolution";
    string antialiasing_p = "antialiasing";
    string vsync_p = "vsync";
    string texture_p = "texture";
    string volume_p = "volume";

    void Start()
    {
        dropdown_resolution.onValueChanged.AddListener(delegate { resolution(dropdown_resolution); });
        dropdown_antialiasing.onValueChanged.AddListener(delegate { antialiasing(dropdown_antialiasing); });
        dropdown_vsync.onValueChanged.AddListener(delegate { vsync(dropdown_vsync); });
        dropdown_texture.onValueChanged.AddListener(delegate { texture(dropdown_texture); });
        dropdown_volume.onValueChanged.AddListener(delegate { volume(dropdown_volume); });

        //Check configuration

        if (PlayerPrefs.HasKey(resolution_p)) //Check Resolution
        {
            dropdown_resolution.value = PlayerPrefs.GetInt(resolution_p);
        }
        else
        {
            dropdown_resolution.value = 0;
            PlayerPrefs.SetInt(resolution_p, 0);
        }

        if (PlayerPrefs.HasKey(antialiasing_p)) //Check AntiAliasing
        {
            dropdown_antialiasing.value = PlayerPrefs.GetInt(antialiasing_p);
        }
        else
        {
            dropdown_antialiasing.value = 0;
            PlayerPrefs.SetInt(antialiasing_p, 0);
        }

        if (PlayerPrefs.HasKey(vsync_p)) //Check Vsync
        {
            dropdown_vsync.value = PlayerPrefs.GetInt(vsync_p);
        }
        else
        {
            dropdown_vsync.value = 0;
            PlayerPrefs.SetInt(vsync_p, 0);
        }

        if (PlayerPrefs.HasKey(texture_p)) //Check Texture
        {
            dropdown_texture.value = PlayerPrefs.GetInt(texture_p);
        }
        else
        {
            dropdown_texture.value = 0;
            PlayerPrefs.SetInt(texture_p, 0);
        }

        if (PlayerPrefs.HasKey(volume_p)) //Check Volume
        {
            dropdown_volume.value = PlayerPrefs.GetInt(volume_p);
        }
        else
        {
            dropdown_volume.value = 0;
            PlayerPrefs.SetInt(volume_p, 0);
        }

        resolution_general(PlayerPrefs.GetInt(resolution_p));
        antialiasing_general(PlayerPrefs.GetInt(antialiasing_p));
        vsync_general(PlayerPrefs.GetInt(vsync_p));
        texture_general(PlayerPrefs.GetInt(texture_p));
        volume_general(PlayerPrefs.GetInt(volume_p));
    }

    void Update()
    {

    }

    public void resolution(Dropdown change)
    {
        PlayerPrefs.SetInt(resolution_p, change.value);
        resolution_general(change.value);
    }

    public void antialiasing(Dropdown change)
    {
        PlayerPrefs.SetInt(antialiasing_p, change.value);
        antialiasing_general(change.value);
    }

    public void vsync(Dropdown change)
    {
        PlayerPrefs.SetInt(vsync_p, change.value);
        vsync_general(change.value);
    }

    public void texture(Dropdown change)
    {
        PlayerPrefs.SetInt(texture_p, change.value);
        texture_general(change.value);
    }

    public void volume(Dropdown change)
    {
        PlayerPrefs.SetInt(volume_p, change.value);
        volume_general(change.value);
    }

    public void resolution_general(int i)
    {
        switch (i)
        {
            case 0: //1920 x 1080
                Screen.SetResolution(1920, 1080, true);
                break;
            case 1: //1600 x 900
                Screen.SetResolution(1600, 900, true);
                break;
            case 2: //1440 x 900
                Screen.SetResolution(1440, 900, true);
                break;
            case 3: //1366 x 768
                Screen.SetResolution(1366, 768, true);
                break;
            case 4: //1360 x 768
                Screen.SetResolution(1360, 768, true);
                break;
            case 5://1280 x 720
                Screen.SetResolution(1280, 720, true);
                break;
            case 6: //1024 x 768
                Screen.SetResolution(1024, 768, true);
                break;
            case 7: //800 x 600
                Screen.SetResolution(800, 600, true);
                break;
            default:
                Debug.Log("RESOLUTION ERROR!!!");
                break;
        }
    }

    public void antialiasing_general(int i)
    {
        switch (i)
        {
            case 0: //8x
                QualitySettings.antiAliasing = 8;
                break;
            case 1: //4x
                QualitySettings.antiAliasing = 4;
                break;
            case 2: //2x
                QualitySettings.antiAliasing = 2;
                break;
            case 3: //Off
                QualitySettings.antiAliasing = 0;
                break;
            default:
                Debug.Log("ANTIALIASING ERROR!!!");
                break;
        }
    }

    public void vsync_general(int i)
    {
        switch (i)
        {
            case 0: //On
                QualitySettings.vSyncCount = 1;
                break;
            case 1: //Off
                QualitySettings.vSyncCount = 0;
                break;
            default:
                Debug.Log("VSYNC ERROR!!!");
                break;
        }
    }

    public void texture_general(int i)
    {
        switch (i)
        {
            case 0: //Full
                QualitySettings.masterTextureLimit = 0;
                break;
            case 1: //Half
                QualitySettings.masterTextureLimit = 1;
                break;
            case 2: //Quarter
                QualitySettings.masterTextureLimit = 2;
                break;
            case 3: //Eighth
                QualitySettings.masterTextureLimit = 3;
                break;
            default:
                Debug.Log("TEXTURE ERROR!!!");
                break;
        }
    }

    public void volume_general(int i)
    {
        switch (i)
        {
            case 0: //On
                AudioListener.volume = 1;
                break;
            case 1: //Off
                AudioListener.volume = 0;
                break;
            default:
                Debug.Log("VOLUME ERROR!!!");
                break;
        }
    }
}