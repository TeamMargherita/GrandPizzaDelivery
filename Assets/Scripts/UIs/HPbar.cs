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
        HP.fillAmount = (float)PlayerMove.HP / PlayerMove.MaxHP;
        HPText.text = PlayerMove.HP + " / "+ PlayerMove.MaxHP;
    }
}
