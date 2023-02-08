using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] Text text;
    public void OnInit(float hp)
    {
        text.text = hp.ToString();
        Invoke(nameof(OnDespawn), 12f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
