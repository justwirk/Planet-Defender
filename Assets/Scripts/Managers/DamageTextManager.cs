using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : Singular<DamageTextManager>
{

    public ObjectCapsule Capsule { get; set; }

    void Start()
    {
        Capsule = GetComponent<ObjectCapsule>();
    }

   
}
