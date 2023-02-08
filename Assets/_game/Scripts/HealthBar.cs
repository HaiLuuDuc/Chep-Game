using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    float hp;
    float maxHp;
    private void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHp, Time.deltaTime * 5f);
    }
    public void OnInit(float maxHp)
    {
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.fillAmount = 1;
    }
    public void OnDespawn()
    {

    }
    public void SetNewHp(float hp)
    {
        this.hp = hp;
    }
}
