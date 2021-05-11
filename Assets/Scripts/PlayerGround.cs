using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour
{
    Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        playerAnimator.SetBool("isJump", false);
    }
}
