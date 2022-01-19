using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float MAX_HEALTH = 100f;
    [SerializeField]
    protected float _Health;
    [SerializeField]
    protected float Speed = 3.5f;
    [SerializeField]
    protected float AngularSpeed = 120f;
    [SerializeField]
    protected float _Damage = 10f;

    [Space(20f)]
    [SerializeField]
    private string PlayerPrefabName = "Player";

    public delegate void Action();
    public Action Dead;

    public float Health {
        get {
            return _Health;
        }
    }

    public float Damage {
        get {
            return _Damage;
        }
    }

    protected NavMeshAgent agent;
    protected Animator animator;
    protected GameObject Player;

    public void AddHealth(float hp) {
        if (_Health + hp <= MAX_HEALTH) {
            _Health += hp;
        } else {
            _Health = MAX_HEALTH;
        }
    }

    public void RemoveHealth(float hp) {
        if (_Health - hp > 0) {
            _Health -= hp;
        } else {
            _Health = 0;
            Dead?.Invoke();
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        _Health = MAX_HEALTH;
        agent.speed = Speed;
        agent.angularSpeed = AngularSpeed;

        Dead += DeadEnemy;

        if (!Player) {
            Player = GameObject.Find(PlayerPrefabName);
        }

        EnemyStart();
    }

    void Update()
    {
        if (GameState.GS().IsPaused)
            return;

        EnemyUpdate();
    }

    protected bool GetChance(float chance) { return Random.Range(0, 100) > chance; }

    protected virtual void EnemyUpdate() {}
    protected virtual void EnemyStart() {}

    protected virtual void DeadEnemy() 
    {
        Debug.Log($"Сущность: {gameObject.name} погибла");
    }
}
