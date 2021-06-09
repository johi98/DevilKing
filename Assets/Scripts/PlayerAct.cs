using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAct : MonoBehaviour
{

    Animator playerAnimator;
    Rigidbody playerRigidbody;
    public BoxCollider swordCollider;
    bool isCanDamage;
    bool isDoAttack;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        isCanDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "OrcAttack"&& isCanDamage)
        {
            Damaged();
            StartCoroutine("AvoidDamage");
        }
    }

    public void Attack()
    {
        playerAnimator.SetTrigger("doAttack");

    }
    public void AttackDo()
    {
        swordCollider.enabled = true;

    }
    public void AttackEnd()
    {
        swordCollider.enabled = false;

    }

    public void Rolling()
    {
       
        if(isCanDamage)
        {
            playerAnimator.SetTrigger("doRolling");
            isCanDamage = false;
            StartCoroutine("AvoidDamage");
         
        }
     
    }

    private void Damaged()
    {
        Debug.Log("맞음");
        playerAnimator.SetTrigger("doDamage");
        playerAnimator.SetBool("isDamage", true);
    }

    IEnumerator AvoidDamage()
    {
        isCanDamage = false;
        yield return new WaitForSeconds(6f);
        playerAnimator.SetBool("isDamage", false);
        isCanDamage = true;
        yield return null;
    }

 
}
