using UnityEngine;
using TMPro;
using IronFly.Core;
using System.Collections;
using UnityEngine.SceneManagement;
using IronFly.Player;

namespace IronFly.Utilites
{    
    public class UiManager : MonoBehaviour 
    {
        #region  Veraibles
        [Header("Main Menu")]
        [SerializeField] private GameObject TutorialScreen;

        [Header("Game Screen")]
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private GameObject GameOverScreen;
        [SerializeField] private GameObject GameWinScreen;
        [SerializeField] private GameObject GameFindKeyScreen;
        [SerializeField] private TMP_Text scoreGameWinText,scoreGameLoseText;
        [SerializeField] private int scoreByTime=0;
        #endregion


        #region  Unity

        private void Start() 
        {
            Time.timeScale=1;
        }
        
        private void OnEnable() 
        {
            Timer.updateTimeEvent+=TimerUpdate;    
            Timer.gameOverEvent+=GameOver;
            Portal.gameWinEvent+=GameWin;
            Portal.gameFindKeyEvent+=GameFindKey;
            Timer.gameScoreEvent+=ScoreUpdate;
        }

        private void OnDisable() 
        {
            Timer.updateTimeEvent-=TimerUpdate;
            Timer.gameOverEvent-=GameOver;
            Portal.gameWinEvent-=GameWin;
            Portal.gameFindKeyEvent-=GameFindKey;
            Timer.gameScoreEvent-=ScoreUpdate;
        }
        #endregion

        private void TimerUpdate(int time)
        {                    
            timerText.text=$"{time/60} : {time%60}";
        }

        private void GameOver()
        {
            Debug.Log("GameOver by Timer");
            GameOverScreen.SetActive(true);
            Time.timeScale=0;
        }

        private void GameWin()
        {
            GameWinScreen.SetActive(true);            
            Time.timeScale=0;
        }

        private void GameFindKey()
        {
            StartCoroutine(COR_FindKey());
        }

        private IEnumerator COR_FindKey()
        {
            GameFindKeyScreen.SetActive(true);
            yield return new WaitForSecondsRealtime(2f);
            GameFindKeyScreen.SetActive(false);
        }

        private void ScoreUpdate(int score)
        {            
            Health health=FindObjectOfType<Health>();
            
            if (health.GetTotalHealth==2)
            {
                score-=10;
            }
            if (health.GetTotalHealth==1)
            {
                score-=20;
            }
            if (health.GetTotalHealth<=0)
            {
                score=0;
            }

            if (score<=0)
            {
                score=0;    
            }

            scoreGameWinText.text="Score : "+score;
            scoreGameLoseText.text="Score : "+score;
        }

        public void btn_home()
        {
            SceneManager.LoadScene(0);
        }

        public void btn_Play()
        {
            TutorialScreen.SetActive(true);
            Time.timeScale=1;
            StartCoroutine(COR_StartGamePlay());
        }
        private IEnumerator COR_StartGamePlay()
        {
            yield return new WaitForSecondsRealtime(5f);
            SceneManager.LoadScene(1);
        }

        public void btn_Chat()
        {
            Application.OpenURL("https://www.linkedin.com/in/ketan-rathod-rk/");
        }
    }
}