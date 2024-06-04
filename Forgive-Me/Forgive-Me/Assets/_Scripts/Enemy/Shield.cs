using _Scripts.Components.Triggers;
using UnityEngine;

namespace _Scripts.Enemy
{
    [RequireComponent(typeof(EnterTrigger))]
    public class Shield : MonoBehaviour
    {
        [SerializeField] private float _reflectSpeed = 20f;

        public void Reflect(GameObject other)
        {
            var otherRigidbody = other.GetComponent<Rigidbody>();
            var vector = other.transform.position - transform.position;
            otherRigidbody.AddForce(vector.normalized * _reflectSpeed, ForceMode.Impulse);
        }
    }
}