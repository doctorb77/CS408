using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{

    Vector3 localScale;

    void Start()
    {
        //transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);

        localScale = transform.localScale;

        localScale.x = (this.GetComponentInParent<unit>().health / (float)this.GetComponentInParent<unit>().maxHealth) * 0.75f;
        transform.localScale = localScale;
    }

    void Update()
    {
        localScale = transform.localScale;

        localScale.x = (this.GetComponentInParent<unit>().health / (float)this.GetComponentInParent<unit>().maxHealth) * 0.75f;
        transform.localScale = localScale;
    }
}
