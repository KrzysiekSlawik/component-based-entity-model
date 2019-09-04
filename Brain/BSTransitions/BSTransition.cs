using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EntityComponent
{
    public abstract class BSTransition : ScriptableObject
    {
        [HideInInspector]public Brain brain;
        public void Link(Brain brain, string state)
        {
            this.brain = brain;
        }
        public abstract bool Eval();
    }
}
