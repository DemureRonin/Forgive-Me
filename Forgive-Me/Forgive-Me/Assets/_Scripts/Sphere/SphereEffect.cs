using System;
using UnityEngine;

namespace _Scripts.Sphere
{
    public class SphereEffect : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
