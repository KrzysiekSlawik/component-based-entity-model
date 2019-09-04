using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace EntityComponent
{
    public abstract class BBehaviour : ScriptableObject
    {
        [HideInInspector]public Brain brain;

        public void LoadBrain(Brain brain)
        {
            this.brain = brain;
        }

        public abstract void Update();
    }
}

