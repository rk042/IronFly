using System.Collections;
using UnityEngine;

namespace IronFly.Core
{    
    public class Timer : MonoBehaviour
    {
        [SerializeField] private int levelTime;
        public delegate void UpdateTimer(int time);
        public delegate void GameOver();
        public static event UpdateTimer updateTimeEvent;
        public static event GameOver gameOverEvent;
        public static event UpdateTimer gameScoreEvent;
        private int scoreByTime=0;

        private void OnEnable() 
        {
            Portal.gameWinEvent+=StopTime;
        }

        private void OnDisable() 
        {            
            Portal.gameWinEvent-=StopTime;
        }

        private Coroutine StopStartTime;

        // Start is called before the first frame update
        void Start()
        {
            StopStartTime=StartCoroutine(COR_Timer());
        }

        private IEnumerator COR_Timer()
        {
            for (int i = levelTime; i >= 0; i--)
            {
                yield return new WaitForSecondsRealtime(1f);
                scoreByTime++;
                if (updateTimeEvent != null)
                {
                    updateTimeEvent(i);
                }
            }

            GameOverCall();
        }

        public void GameOverCall()
        {
            if (gameOverEvent != null)
            {
                gameOverEvent();
                StopTime();
            }
        }

        private void StopTime()
        {
            StopCoroutine(StopStartTime);
            int realScore = levelTime- scoreByTime;
            gameScoreEvent(realScore);
        }
    }
}
