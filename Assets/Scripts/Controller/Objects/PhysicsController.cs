using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Observer;
namespace Controller.Object
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public abstract class PhysicsController : TransformController
    {
        protected Rigidbody Rb;
        protected override void Awake()
        {
            base.Awake();
            Rb = GetComponent<Rigidbody>();
            Rb.freezeRotation = true;
        }
    }
}