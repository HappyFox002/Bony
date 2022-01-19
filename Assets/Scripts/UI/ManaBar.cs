using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : Bar
{
    public GameObject Data;
    public override void DataInit()
    {
        if (Data) {
            Mana mn = Data.GetComponent<Mana>();
            slider.maxValue = mn.MaxMana;
            mn.UpdateMana += SliderUpdate;
        }
    }
}
