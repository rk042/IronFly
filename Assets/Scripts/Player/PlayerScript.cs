using UnityEngine;
using IronFly.Core;

namespace IronFly.Player
{    
    public class PlayerScript : MonoBehaviour
    {
        #region Veriables
        public delegate void HitEvent();
        public static event HitEvent hitEvent;        
        [SerializeField] private float moveForce=20f;
        [SerializeField] private float jumpForce=700f;
        [SerializeField] private float maxVelocity=4f;        
        private Rigidbody2D myRd;
        private Animator myAnim;        
        private GasManager gasManager;
        private int animWalkBool=Animator.StringToHash("Walk");
        private int animJumpBool=Animator.StringToHash("Jump");
        [SerializeField]private bool isGround;
        #endregion

        #region  Unity
        private void Awake() 
        {
            myRd=GetComponent<Rigidbody2D>();    
            myAnim=GetComponent<Animator>();       
            gasManager=GetComponent<GasManager>();     
        }
        
        private void Update() 
        {
            if (isGround)
            {
                myAnim.SetBool(animJumpBool,false);
            }    
            else if(!isGround)
            {
                myAnim.SetBool(animJumpBool,true);
            }
        }
        private void FixedUpdate() 
        {
            PlayerWalkKeyboard();            
        }
        #endregion

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
                    forceX=moveForce;                                     
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
                    forceX = -moveForce;                    
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

            if (Input.GetKey(KeyCode.Space) && !gasManager.IsGasComplete)
            {
                gasManager.DicreseGas();                
                forceY = jumpForce;
            }
            else
            {
                gasManager.IncreseGas();
            }
            
            myRd.AddForce(new Vector2(forceX,forceY));
        }        
    
        private void OnCollisionEnter2D(Collision2D other) 
        {
            if (other.gameObject.CompareTag("Obstacles"))
            {
                Debug.Log("is hit");                
                hitEvent?.Invoke();
            }    
            
            if (other.gameObject.CompareTag("Ground"))
            {
                isGround=true;
            }            
        }

        private void OnCollisionExit2D(Collision2D other) 
        {
            
            if (other.gameObject.CompareTag("Ground"))
            {
                isGround=false;
            }    
        }        

        private void OnCollisionStay2D(Collision2D other) 
        {
            
            if (other.gameObject.CompareTag("Ground"))
            {
                isGround=true;
            }            
        }
    }
}
