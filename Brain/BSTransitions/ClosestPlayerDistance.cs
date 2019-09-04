using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityComponent
{
    [CreateAssetMenu(fileName = "CPDTrans", menuName = "ClosestPlayerDistanceTransition", order = 4)]
    public class ClosestPlayerDistance : BSTransition
    {
        public float min;
        public float max;
        public override bool Eval()
        {
            Vector3 pPos = brain.PlayerPosition();
            float sqrDistance = (pPos - brain.self.position).sqrMagnitude;
            return sqrDistance < Mathf.Pow(max, 2) && sqrDistance > Mathf.Pow(min, 2);
        }
    }
}
