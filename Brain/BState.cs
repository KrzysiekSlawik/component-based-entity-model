using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace EntityComponent
{
    [Serializable]
    public class BState
    {
        public string name;
        public BBehaviour behaviour;
        public BPair[] transitions;

        public string EvalTransitions()
        {
            foreach (BPair p in transitions)
            {
                if(p.transition.Eval())
                {
                    return p.transitionTo;
                }
            }
            return name;
        }
    }
    
}
