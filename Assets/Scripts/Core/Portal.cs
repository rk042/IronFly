using UnityEngine;

namespace IronFly.Core
{
    public class Portal : MonoBehaviour 
    {
        [SerializeField] ParticleSystem ps;

        private bool isProtalOpen=false;

        private void OnEnable() 
        {
            Key.playerHaveKey+=OpenDoorPlayerHaveKey;
        }        

        private void OnDisable() 
        {
            Key.playerHaveKey-=OpenDoorPlayerHaveKey;
        }

        private void OpenDoorPlayerHaveKey()
        {
            isProtalOpen=true;
            Debug.Log("key is collected goto door");
            ps.Play();
            //TODO: apply some particle system here
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (isProtalOpen)
                {                    
                    Debug.Log("GameOver Player Win");
                }
                else 
                {                    
                    Debug.Log("Find Key");
                }
            }
        }
    }    
}