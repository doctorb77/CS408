using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxScript : MonoBehaviour
{
    void Update()
    {
        GetComponent<Skybox>().material.SetFloat("_Rotation", Time.time * 0.5f);
    }
}
