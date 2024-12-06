using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicCircle : MonoBehaviour
{
    public float lifeTime;
    private Transform caster;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    public void Initialize(Transform caster)
    {
        this.caster = caster;

        // Place the magic circle beneath the caster
        if (caster != null)
        {
            transform.position = caster.position;
        }
    }
}
