using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float h;

    public int speed;
    Vector3 moveVec;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");

  

        moveVec = new Vector3(h, 0, 0).normalized;

        transform.position += moveVec * speed * Time.deltaTime;




        transform.LookAt(transform.position + moveVec);
    }

}
