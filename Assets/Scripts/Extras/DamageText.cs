using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI DmgText => GetComponentInChildren<TextMeshProUGUI>();

    public void ReturnTextToCapsule()
    {
        transform.SetParent(null);
        ObjectCapsule.ReturnToCapsule(gameObject);
    }
}
