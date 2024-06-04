using UnityEngine;

namespace _Scripts.Components
{
    public class StopObjectComponent : MonoBehaviour
    {
        public void Stop(GameObject other)
        {
            var otherRigidbody = other.GetComponent<Rigidbody>();
            otherRigidbody.velocity = Vector3.zero;
        }
    }
}