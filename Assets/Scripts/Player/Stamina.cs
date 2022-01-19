using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public bool StaminaUse { get; set; } = false;


    [SerializeField]
    public float MaxStamina { get; } = 100f;
    [SerializeField]
    private float StaminaRegeneration = 5f;

    private float _Stamina;
    public float CurrentStamina { get { return _Stamina; } }

    public delegate void _UpdateStamina(float stamina);
    public event _UpdateStamina UpdateStamina;

    void Start()
    {
        _Stamina = 0;
    }

    public bool RemoveStamina(float st) {
        if (_Stamina - st > 0) {
            _Stamina -= st;
            return true;
        }
        return false;
    }

    void FixedUpdate()
    {
        if (GameState.GS().IsPaused)
            return;

        if (_Stamina < MaxStamina)
            _Stamina += Time.fixedDeltaTime * StaminaRegeneration;
        else
            _Stamina = MaxStamina;

        UpdateStamina?.Invoke(_Stamina);
        StaminaUse = false;
    }
}
