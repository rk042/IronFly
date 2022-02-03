using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IronFly.Player
{    
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private float moveForce=20f;
        [SerializeField] private float jumpForce=700f;
        [SerializeField] private float maxVelocity=4f;

        private Rigidbody2D myRd;
        private Animator myAnim;
        private bool isGrounded;
        private int animWalkBool=Animator.StringToHash("Walk");

        private void Awake() 
        {
            myRd=GetComponent<Rigidbody2D>();    
            myAnim=GetComponent<Animator>();            
        }

        private void FixedUpdate() 
        {
            Debug.Log(myRd.velocity);
            PlayerWalkKeyboard();            
        }

        private void PlayerWalkKeyboard()
        {
            float forceX=0f;
            float forceY=0f;
            
            float vel=Mathf.Abs(myRd.velocity.x);
            
            float h=Input.GetAxisRaw("Horizontal");

            if (h>0)
            {
                if (vel<maxVelocity)
                {
                    if (isGrounded)
                    {                        
                        forceX=moveForce;
                    }
                    else
                    {
                        forceX=moveForce*1.1f ;
                    }
                }

                Vector3 scale=transform.localScale;
                scale.x=1;
                transform.localScale=scale;

                myAnim.SetBool(animWalkBool,true);                
            }
            else if (h<0)
            {
                if (vel < maxVelocity)
                {
                    if (isGrounded)
                    {
                        forceX = -moveForce;
                    }
                    else
                    {
                        forceX = -moveForce * 1.1f;
                    }
                }

                Vector3 scale = transform.localScale;
                scale.x = -1;
                transform.localScale = scale;

                myAnim.SetBool(animWalkBool, true);
            }
            else  
            {
                myAnim.SetBool(animWalkBool, false);
            }  

            if (Input.GetKey(KeyCode.Space))
            {
                if (!isGrounded)
                {
                    //isGrounded=false;
                    forceY = jumpForce;
                }                
            }
            myRd.AddForce(new Vector2(forceX,forceY));
        }
        private void OnCollisionEnter2D(Collision2D other) 
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                // isGrounded=true;
            }
        }
    }

}
