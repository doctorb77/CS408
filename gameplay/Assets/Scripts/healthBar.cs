using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{

    Vector3 localScale;

    void Start()
    {
        localScale = transform.localScale;

        localScale.x = this.GetComponentInParent<unit>().health / (float)this.GetComponentInParent<unit>().maxHealth;
        transform.localScale = localScale;
    }
}
