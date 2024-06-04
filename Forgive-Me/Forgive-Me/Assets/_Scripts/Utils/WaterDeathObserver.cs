using UnityEngine;

namespace _Scripts.Utils
{
    public class WaterDeathObserver : MonoBehaviour
    {
        public delegate void WaterDeathEvent();

        public static event WaterDeathEvent OnWaterDeath;

        public void OnDeath()
        {
            OnWaterDeath?.Invoke();
        }
    }
}
