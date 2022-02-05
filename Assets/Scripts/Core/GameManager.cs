using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IronFly.Core
{    
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private void Awake() 
        {
            if (instance==null)
            {
                instance=this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
