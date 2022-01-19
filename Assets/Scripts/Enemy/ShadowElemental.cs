using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowElemental : Enemy
{
    [SerializeField]
    private float DistanceAggression = 10f;
    [SerializeField]
    private float DistanceAttack = 0.65f;
    [SerializeField]
    private float RangeWalking = 25f;
    [SerializeField]
    private float TimeIdle = 2f;
    [SerializeField]
    private float TimePursuit =10f;
    [SerializeField]
    private float TimeCooldownAttack = 1.5f;
    [SerializeField]
    private float BeamRay = 1.6f;

    private Vector3 StartPosition;
    private float time = 0;

    private enum LogicState { 
        Idle = 0,
        Move,
        Attack,
        Dead
    }

    [SerializeField]
    private LogicState State = LogicState.Idle;
    private void SwitchState(LogicState st) { time = 0; State = st; animator.SetInteger("State",(int)State); }

    protected override void EnemyStart()
    {
        base.EnemyStart();

        StartPosition = transform.position;
        agent.destination = StartPosition;
        
        SwitchState(State);
    }

    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();

        AggresiveLogic();

        switch (State) {
            case LogicState.Idle:
                IdleLogic();
                break;
            case LogicState.Move:
                WalkingLogic();
                break;
            case LogicState.Attack:
                AttackLogic();
                break;
        }
    }

    protected override void DeadEnemy()
    {
        base.DeadEnemy();
        SwitchState(LogicState.Dead);
    }

    private void IdleLogic() {
        time += Time.deltaTime;

        if (time > TimeIdle)
        {
            GeneratePositionWalking();
            time = 0;
        }
    }

    private void GeneratePositionWalking() {
        Vector3 pos = RangeWalking * new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f, 1f));
        agent.destination = StartPosition + pos;
        SwitchState(LogicState.Move);
    }

    private void WalkingLogic() {
        time += Time.deltaTime;

        if (time > TimePursuit)
            GeneratePositionWalking();

        if (agent.pathPending || agent.remainingDistance > 0.1f)
            return;

        SwitchState(LogicState.Idle);
    }

    private void AggresiveLogic() {
        RaycastHit hit;
        //Debug.DrawRay(transform.position + (BeamRay * transform.TransformDirection(Vector3.forward)), transform.TransformDirection(Vector3.forward) * DistanceAttack, Color.yellow);
        if (Physics.Raycast(transform.position + (BeamRay * transform.TransformDirection(Vector3.forward)), transform.TransformDirection(Vector3.forward), out hit, DistanceAttack) && State != LogicState.Attack)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                agent.destination = transform.position;
                SwitchState(LogicState.Attack);
            }
        }
        else if (Vector3.Distance(Player.transform.position, transform.position) <= DistanceAggression && State != LogicState.Attack)
        {
            agent.destination = Player.transform.position;
            SwitchState(LogicState.Move);
        }
    }

    private void AttackLogic() {
        time += Time.deltaTime;

        if (time > TimeCooldownAttack)
            SwitchState(LogicState.Idle);
    }
}
