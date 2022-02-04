using System.Collections;
using UnityEngine;
using System;

namespace IronFly.Core
{    
    public class Timer : MonoBehaviour
    {
        [SerializeField] private int levelTime;
        public delegate void UpdateTimer(int time);
        public static event UpdateTimer updateTime;
        public delegate void GameOver();
        public static event GameOver gameOver;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(COR_Timer());
        }

        private IEnumerator COR_Timer()
        {
            for (int i = levelTime; i >= 0; i--)
            {
                yield return new WaitForSecondsRealtime(1f);
                
                if (updateTime!=null)
                {                    
                    updateTime(i);
                }
            }    

            if (gameOver!=null)
            {                
                gameOver();
            }
        }
    }
}
