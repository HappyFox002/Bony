using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
    public GameObject Data;
    public override void DataInit()
    {
        if (Data) {
            Health ht = Data.GetComponent<Health>();
            slider.maxValue = ht.MaxHealth;
            ht.UpdateHealth += SliderUpdate;
        }
    }
}
