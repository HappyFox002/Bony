using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField]
    public float MaxMana { get; set; } = 100f;
    [SerializeField]
    private float ManaRegeneration = 5f;

    private float _Mana;

    public delegate void _UpdateMana(float Mana);
    public event _UpdateMana UpdateMana;

    public bool RemoveMana(float mana) {
        if (_Mana - mana > 0)
        {
            _Mana -= mana;
            UpdateMana?.Invoke(_Mana);
            return true;
        }
        return false;
    }

    public void AppendMana(float mana)
    {
        if (_Mana + mana <= MaxMana)
        {
            _Mana += mana;
        }
        else
        {
            _Mana = MaxMana;
        }
        UpdateMana?.Invoke(_Mana);
    }

    void Start()
    {
        _Mana = 0;
    }

    void FixedUpdate()
    {
        if (GameState.GS().IsPaused)
            return;

        if (_Mana < MaxMana)
            _Mana += Time.fixedDeltaTime * ManaRegeneration;
        else
            _Mana = MaxMana;

        UpdateMana?.Invoke(_Mana);
    }
}
