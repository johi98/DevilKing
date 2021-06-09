using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class OrcAI : MonoBehaviour
{
    bool isThink;
    bool isChase;
    Animator orcAnimator;
    public GameObject player;
    Transform playerTransform;
    Rigidbody rig;
    NavMeshAgent nav;
    public Collider jumpAttack;
    public Collider swingAttack;
    public GameObject dropStone;
    public GameObject shotStone;
    public Transform bulletTransform;
    public bool canRangeAttack = false;

    public enum OrcAttack
    {
        SwingAttack,
        JumpAttack,
        DropStone,
        Roaring,
        Angry,
        ShotStone
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
        Instantiate(dropStone, new Vector3(playerTransform.position.x, 10, 0), dropStone.transform.rotation);
    }
    public void ShotStone()
    {
        GameObject instantStone = Instantiate(shotStone, bulletTransform.position, shotStone.transform.rotation);
        instantStone.GetComponent<Rigidbody>().velocity = gameObject.transform.forward*10;
    }

    IEnumerator OrcThink()
    {
        isThink = true;
        
        yield return new WaitForSeconds(0.2f);

        //Debug.Log("오크 생각");
        ChaseEnd();



        if(nav.remainingDistance > nav.stoppingDistance +5 )
        {
            if(canRangeAttack)
            {
                nav.enabled = false;
                orcAnimator.SetTrigger("dropStone");


                yield return new WaitForSeconds(3.5f);
                nav.enabled = true;
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
            _orcAttack = (OrcAttack)Random.Range(0, 4);

            nav.enabled = false;
            switch (_orcAttack)
            {
                case OrcAttack.SwingAttack:
                    orcAnimator.SetTrigger("swingAttack");
                    yield return new WaitForSeconds(6f);
                    nav.enabled = true;
                    break;
                case OrcAttack.JumpAttack:
                    orcAnimator.SetTrigger("jumpAttack");
       
                    yield return new WaitForSeconds(4f);
                    nav.enabled = true;
                    break;
                case OrcAttack.DropStone:
                    orcAnimator.SetTrigger("shotStone");
                    yield return new WaitForSeconds(3.5f);
                    nav.enabled = true;
                    break;
                case OrcAttack.Roaring:
                    orcAnimator.SetTrigger("roaring");
                    yield return new WaitForSeconds(3.5f);
                    nav.enabled = true;
                    break;
                case OrcAttack.Angry:
                    orcAnimator.SetTrigger("angry");
                    yield return new WaitForSeconds(10f);
                    nav.enabled = true;
                    break;
            }

            canRangeAttack = true;

            nav.enabled = true;
            nav.isStopped = true;
            //Debug.Log("오크 생각 끝");
        
            yield return new WaitForSeconds(1f);
            isThink = false;
        }
        yield return null;


    }
}
