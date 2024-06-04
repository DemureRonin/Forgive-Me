using UnityEngine;

namespace _Scripts.LevelManagement
{
    public class Exit : MonoBehaviour
    {
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}