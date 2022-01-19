using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject GameObjectData;
    private Enemy enemy;

    void Start()
    {
        enemy = GameObjectData.GetComponent<Enemy>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            other.GetComponent<Health>().RemoveHealth(enemy.Damage);
        }
    }
}
