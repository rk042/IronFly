using UnityEngine;
using System;

namespace IronFly.Core
{    
    public class Key : MonoBehaviour 
    {
        public delegate void PlayerHaveKeyDelegate();
        public static event PlayerHaveKeyDelegate playerHaveKey;

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (playerHaveKey!=null)
                {                    
                    playerHaveKey();    
                    Destroy(gameObject);
                }
            }   
        }
    }
}