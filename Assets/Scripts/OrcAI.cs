using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrcAI : MonoBehaviour
{
    bool isThink;

    Animator OrcAnimator;
    [SerializeField] private GameObject Player;
    Transform playerTransform;
    float distance_Player;
    float distace_Player_abs;
    NavMeshAgent nav;

    private void Start()
    {
        OrcAnimator = GetComponentInChildren<Animator>();
        playerTransform = Player.GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        
    }
    


    void Update()
    {

        nav.SetDestination(playerTransform.position);
        

/*
        distance_Player = playerTransform.position.x - transform.position.x;
        distace_Player_abs = Mathf.Abs(distance_Player);


        if (isThink == false)
        {
            StartCoroutine("OrcMoveThink");
        }
*/

    }

    IEnumerator OrcMoveThink()
    {
        isThink = true;

        float _moveValue_x;
        _moveValue_x = distance_Player / distace_Player_abs  * (distace_Player_abs - 2);

        transform.Translate(new Vector3(_moveValue_x, 0, 0));

        yield return new WaitForSeconds(5f);


        isThink = false;
        yield return null;
    }

    IEnumerator OrcAttackThink()
    {
        isThink = true;

        distance_Player = playerTransform.position.x - transform.position.x;


        yield return new WaitForSeconds(5f);


        isThink = false;
        yield return null;
    }
}
