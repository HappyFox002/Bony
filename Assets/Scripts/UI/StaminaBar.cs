using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : Bar
{
    public GameObject Data;
    public override void DataInit()
    {
        if (Data) {
            Stamina st = Data.GetComponent<Stamina>();
            slider.maxValue = st.MaxStamina;
            st.UpdateStamina += SliderUpdate;
        }
    }
}
