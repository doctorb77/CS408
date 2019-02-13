using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour
{
    public int health;
    public bool isPlayerOneUnit;
    public float defense;
    public string typeOfUnit;

    private void Start()
    {
        health = 100;
        defense = 0.0;
    }

    private void Update()
    {
        
    }

}
