using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EntityComponent
{
    [Serializable]
    public class BPair
    {
        public BSTransition transition;
        public string transitionTo;
    }
}