using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int playerHp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Attack"))
        {

        }
    }
}
