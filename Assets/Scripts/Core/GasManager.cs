using UnityEngine;
using UnityEngine.UI;

namespace IronFly.Core
{    
    public class GasManager : MonoBehaviour
    {
        #region Veriables
        [SerializeField] float totalGas;
        [SerializeField] Slider totalGasSlider;        
        [SerializeField] private float gasIncreseTime;

        public float GetTotalGas=>totalGas;
        public bool IsGasComplete{get;private set;}
        
        private bool isIncrese;
        private float TotalGasStart;

        #endregion

        #region  Unity
        private void Start() 
        {
            totalGasSlider.maxValue=totalGas;    
            totalGasSlider.value=totalGas;   
            TotalGasStart=totalGas;               
        }
        #endregion

        public void DicreseGas()
        {
            totalGas-=Time.deltaTime;
            isIncrese=false;
            UpdateGasSlider();
        }

        public void IncreseGas()
        {
            if (totalGas<=TotalGasStart)
            {                        
                isIncrese=true;
                totalGas+=(Time.deltaTime/gasIncreseTime);
                UpdateGasSlider();            
            }
        }

        private void UpdateGasSlider()
        {
            totalGasSlider.value = totalGas;

            if (totalGas <=0)
            {
                IsGasComplete=true;
            }
            else if (totalGas>0)
            {
                IsGasComplete=false;                
            }
        }

    }
}
