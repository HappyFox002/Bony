using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRemove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<Health>().RemoveHealth(35f);
    }
}
