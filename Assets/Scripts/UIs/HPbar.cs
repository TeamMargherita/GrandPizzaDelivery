using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    [SerializeField]
    PlayerMove playermove;
    [SerializeField]
    Image HP;
    [SerializeField]
    Text HPText;

    private void Update()
    {
        HPUpdate();
    }

    void HPUpdate()
    {
        HP.fillAmount = (float)playermove.HP / playermove.MaxHP;
        HPText.text = playermove.HP + " / "+ playermove.MaxHP;
    }
}
