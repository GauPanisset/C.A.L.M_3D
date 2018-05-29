using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageManager : MonoBehaviour
{
    public Image rage_1;
    public Image rage_2;
    public float taux_rage_1;
    public float taux_rage_2;
    public float maxrage = 100f;

    // Use this for initialization
    void Start ()
    {
        taux_rage_1 = 0;
        taux_rage_2 = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        rage_1.fillAmount = taux_rage_1 / maxrage;
        rage_2.fillAmount = taux_rage_2 / maxrage;
    }

    public void AddRage(float rage,int player) {
        if (player == 1)
        {
            taux_rage_1 += rage;
        }else
        {
            taux_rage_2 += rage;
        }
    }
}
