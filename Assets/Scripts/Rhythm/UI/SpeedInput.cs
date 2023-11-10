using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedInput : MonoBehaviour
{
    private RhythmManager manager;
    private float value = 0.1f;
    [SerializeField] private Text text;
    private void Start()
    {
        manager = RhythmManager.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            manager.Speed = Mathf.Clamp(manager.Speed - value, 0.1f, 5f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            manager.Speed = Mathf.Clamp(manager.Speed + value, 0.1f, 5f);
        }

        text.text = manager.Speed.ToString("0.0");
    }
}
