using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Animator playerAnimator;
    bool isAttack1;
    bool isAttack2;
    bool isAttack3;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        playerAnimator.SetTrigger("doAttack");
             

    }

 



}
