using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float MaxHealth { get; set; } = 100f;
    [SerializeField]
    private float HealthRegeneration = 0.5f;

    private float _Health;

    public delegate void _UpdateHealth(float Health);
    public event _UpdateHealth UpdateHealth;

    public delegate void _Dead();
    public event _Dead Dead;

    public void RemoveHealth(float Health) {
        if (_Health - Health >= 0)
        {
            _Health -= Health;
            UpdateHealth?.Invoke(_Health);
        }
        else
        {
            _Health -= Health;
            Dead?.Invoke();
        }
    }

    public void AppendHealth(float Health)
    {
        if (_Health + Health <= MaxHealth)
        {
            _Health += Health;
        }
        else
        {
            _Health = MaxHealth;
        }
        UpdateHealth?.Invoke(_Health);
    }

    void Start()
    {
        _Health = MaxHealth;
        Dead += () =>{ Debug.Log("Смерть"); };
    }

    void FixedUpdate()
    {
        if (GameState.GS().IsPaused)
            return;

        if (_Health < MaxHealth)
            _Health += Time.fixedDeltaTime * HealthRegeneration;
        else
            _Health = MaxHealth;

        UpdateHealth?.Invoke(_Health);
    }
}
