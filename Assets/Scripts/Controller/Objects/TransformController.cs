using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.Observer;

namespace Controller.Object
{
    public abstract class TransformController : Observable
    {
        protected Transform Tf;
        protected override void Awake()
        {
            base.Awake();
            Tf = GetComponent<Transform>();
        }
    }
}
