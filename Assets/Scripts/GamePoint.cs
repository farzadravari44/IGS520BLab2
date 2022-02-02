using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGS520b.starter.SampleGame
{
    [RequireComponent(typeof(Collider))]
    public class GamePoint : MonoBehaviour
    {
        [Tooltip("Points scored by touching this object.")]
        public float points = 10;

        public System.Action<GamePoint> OnTriggerEnterAction;

        void Start()
        {
            // Make sure non of the colliders in child objects are active
            foreach (Collider collider in GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }

            // Make sure the root collider is a trigger and enabled
            Collider rootCollider = GetComponent<Collider>();
            rootCollider.enabled = true;
            rootCollider.isTrigger = true;
        }

        void OnTriggerEnter(Collider collider)
        {
            OnTriggerEnterAction?.Invoke(this);
        }
    }
}
