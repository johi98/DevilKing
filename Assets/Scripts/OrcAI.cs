using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class OrcAI : MonoBehaviour
{
    bool isThink;
    bool isChase;
    bool isAttack = false;
    Animator orcAnimator;
    public GameObject player;
    Transform playerTransform;
    Rigidbody rig;
    NavMeshAgent nav;
    public Collider jumpAttack;
    public Collider swingAttack;
    Collider attackColider;
    public GameObject stone;
    public bool canRangeAttack = false;

    public enum OrcAttack
    {
        SwingAttack,
        JumpAttack,
        DropStone
    }


    private void Awake()
    {
        // OrcAnimator = GetComponentInChildren<Animator>();
        orcAnimator = GetComponent<Animator>();
        playerTransform = player.GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        rig= GetComponent<Rigidbody>();
        ChaseStart();
        
    }
    


    void Update()
    {

        if(nav.enabled)
        {
            nav.SetDestination(playerTransform.position);
       
        }

        if(nav.enabled&&nav.remainingDistance < nav.stoppingDistance)
        {
            ChaseEnd();
        }

//생각중이 아니고 플레이어가 멀리있지 않을때
        if(isThink == false )
        {
            StartCoroutine("OrcThink");

            isThink = true;
        }
    }

    void FixedUpdate()
    {
        
        if (nav.enabled)
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
        }
      
    }

 
    void ChaseStart()
    {

        nav.isStopped = false;
        orcAnimator.SetBool("isWalk", true);
    }

    void ChaseEnd()
    {

        nav.isStopped = true;
        orcAnimator.SetBool("isWalk", false);
    }
    public void JumpAttackDo()
    {
        jumpAttack.enabled = true;
       
    }
    public void JumpAttackEnd()
    {
        jumpAttack.enabled = false;

    }
    public void SwingAttackDo()
    {
        swingAttack.enabled = true;

    }
    public void SwingAttackEnd()
    {
        swingAttack.enabled = false;

    }

    public void DropStone()
    {
        Instantiate(stone, new Vector3(playerTransform.position.x, 10, 0), Quaternion.identity);
    }


    IEnumerator OrcThink()
    {
        isThink = true;
        
        //yield return new WaitForSeconds(1f);

        //Debug.Log("오크 생각");
        ChaseEnd();



        if(nav.remainingDistance > nav.stoppingDistance +2 )
        {
            if(canRangeAttack)
            {
                nav.enabled = false;
                isAttack = true;
                orcAnimator.SetTrigger("dropStone");
                yield return new WaitForSeconds(3.5f);
                nav.enabled = true;
                isAttack = false;
                nav.isStopped = true;
                yield return new WaitForSeconds(1f);
                isThink = false;
                canRangeAttack = false;
            }
            else
            {
                ChaseStart();
                yield return new WaitForSeconds(2f);
                isThink = false;
            }
      

        }
        else
        {
            transform.LookAt(new Vector3(playerTransform.position.x, 0.5f, 0.5f));
            OrcAttack _orcAttack;
            _orcAttack =(OrcAttack)Random.Range(0, 2);

            nav.enabled = false;
            isAttack = true;
            switch (_orcAttack)
            {
                case OrcAttack.SwingAttack:
                    orcAnimator.SetTrigger("swingAttack");
       
                    yield return new WaitForSeconds(6f);
                    nav.enabled = true;
                    canRangeAttack = true;//원거리 공격 가능하게 만듬 조건 잘모르겠어서 일단 여기 넣음
                    break;
                case OrcAttack.JumpAttack:
                    orcAnimator.SetTrigger("jumpAttack");
       
                    yield return new WaitForSeconds(4f);
                    nav.enabled = true;
                    break;
             /*   case OrcAttack.DropStone:
                    orcAnimator.SetTrigger("dropStone");
                    yield return new WaitForSeconds(3.5f);
                    nav.enabled = true;
                    break;*/
            }


            isAttack = false;
            nav.enabled = true;
            nav.isStopped = true;
            //Debug.Log("오크 생각 끝");
        
            yield return new WaitForSeconds(1f);
            isThink = false;
        }
        yield return null;


    }
}
