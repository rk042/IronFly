using UnityEngine;

namespace IronFly.Core
{
    public class Portal : MonoBehaviour 
    {
        public delegate void GameWinDelegate();
        public static event GameWinDelegate gameWinEvent;
        public static event GameWinDelegate gameFindKeyEvent;
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
            // Debug.Log("key is collected goto door");
            ps.Play();
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (isProtalOpen)
                {                    
                    // Debug.Log("GameOver Player Win");                    
                    gameWinEvent?.Invoke();                    
                }
                else 
                {                    
                    // Debug.Log("Find Key");
                    gameFindKeyEvent?.Invoke();
                }
            }
        }
    }    
}