using UnityEngine;
using TMPro;
using IronFly.Core;

namespace IronFly.Utilites
{    
    public class UiManager : MonoBehaviour 
    {
        [SerializeField] private TMP_Text timerText;

        private void OnEnable() 
        {
            Timer.updateTime+=TimerUpdate;    
            Timer.gameOver+=GameOver;
        }

        private void OnDisable() 
        {
            Timer.updateTime-=TimerUpdate;
            Timer.gameOver-=GameOver;
        }

        public void TimerUpdate(int time)
        {                    
            timerText.text=$"{time/60} : {time%60}";
        }

        public void GameOver()
        {
            Debug.Log("GameOver by Timer");
        }
    }
}