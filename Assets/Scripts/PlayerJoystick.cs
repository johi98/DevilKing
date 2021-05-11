using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerJoystick : MonoBehaviour, IPointerDownHandler , IPointerUpHandler , IDragHandler
{

   
  

    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;

    private float radius;

    [SerializeField] private GameObject go_Player;
    [SerializeField] private float moveSpeed;

    private bool isTouch = false;
    private Vector3 movePosition;
    private bool canJump = false;
    Animator playerAnimator;
    private bool isMove;
    Rigidbody playerRigidbody;
     bool isJumping = false;


    // Start is called before the first frame update
    void Start()
    {
        radius = rect_Background.rect.width * 0.5f;
        playerAnimator = go_Player.GetComponentInChildren<Animator>();
        playerRigidbody = go_Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        if (isTouch)
        {
            //이동
            go_Player.transform.position += movePosition;

            //점프
            if (canJump == true && isJumping == false)
            {
                StartCoroutine("Jump");
                isJumping = true;
            }

        }
      
       

    }
    private void Update()
    {
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rect_Background.position;// = 
        value = Vector2.ClampMagnitude(value, radius);
        rect_Joystick.localPosition = value;

        // float distance = Vector2.Distance(rect_Background.position, rect_Joystick.position) / radius;

        JoystickNomalize(ref value);

        movePosition = new Vector3(value.x * moveSpeed * Time.deltaTime, 0f, 0f);
        go_Player.transform.LookAt(go_Player.transform.position + movePosition);


        playerAnimator.SetBool("isMove", value != Vector2.zero);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_Joystick.localPosition = Vector3.zero;
        movePosition = Vector3.zero;

        playerAnimator.SetBool("isMove", movePosition != Vector3.zero);
    }

    private void JoystickNomalize(ref Vector2 v2)
    {
        if(v2.x >= 0 )
        {
            v2.x = 1f;
        }
        else if(v2.x <= 0)
        {
            v2.x = -1f;
        }
        else
        {
            v2 = Vector2.zero;
        }
        if(v2.y >= radius*0.8)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
       
    }

    IEnumerator Jump()
    {
        playerAnimator.SetBool("isJump", true);
        playerAnimator.SetTrigger("doJump");
        playerRigidbody.AddForce(Vector2.up * 8, ForceMode.Impulse);
        yield return new WaitForSeconds(3f);
       // playerAnimator.SetBool("isJump", false);
        isJumping = false;

    }

 
}
