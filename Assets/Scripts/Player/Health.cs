using UnityEngine;
using IronFly.Core;

namespace IronFly.Player
{    
    public class Health : MonoBehaviour
    {
        [SerializeField] private int TotalHealth=0;
        [SerializeField] private GameObject[] healthImage;

        public int GetTotalHealth=>TotalHealth;
        private void OnEnable() 
        {
            PlayerScript.hitEvent+=IsHit;                
        }

        private void OnDisable() 
        {
            PlayerScript.hitEvent-=IsHit;                            
        }

        private void IsHit()
        {
            if (TotalHealth<=0)
            {
                return;
            }
            TotalHealth--;            
            healthImage[TotalHealth].gameObject.SetActive(false);

            if (TotalHealth<=0)
            {
                FindObjectOfType<Timer>().GameOverCall();            
            }
        }
    }
}
