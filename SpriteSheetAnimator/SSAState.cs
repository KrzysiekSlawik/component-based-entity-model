using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EntityComponent;

namespace SSAState
{
    public enum Operator
    {
        greater,
        less
    }
    [Serializable]
    public class SSATBoolCondition
    {
        public string variableName;
        public bool expected;

        public bool Eval(AnimatorController controller)
        {
            return expected == controller.GetBool(variableName);
        }
    }
    [Serializable]
    public class SSATFloatCondition
    {
        public string variableName;
        public Operator op;
        public float expected;
        public bool Eval(AnimatorController controller)
        {
            switch(op)
            {
                case Operator.greater:
                    return controller.GetFloat(variableName) > expected;
                case Operator.less:
                    return controller.GetFloat(variableName) <= expected;
                default:
                    return false;
            }
        }
    }
    [Serializable]
    public class SSATransition
    {
        public SSATFloatCondition[] floatConditions;
        public SSATBoolCondition[] boolConditions;
        public string transitionTo;
        public bool Eval(AnimatorController controller)
        {
            foreach(SSATFloatCondition condition in floatConditions)
            {
                if(!condition.Eval(controller))
                {
                    return false;
                }
            }
            foreach(SSATBoolCondition condition in boolConditions)
            {
                if(!condition.Eval(controller))
                {
                    return false;
                }
            }
            return true;
        }
        
    }
    [Serializable]
    public class SSAStateBody
    {
        public string name;
        public SpriteSheetAnimation anim;
        public bool defaultState;
        private AnimatorController controller;
        public SSATransition[] transitions;

        public void Inject(AnimatorController controller)
        {
            this.controller = controller;
        }
        public void Enter()
        {
            controller.animator.SetAnimation(anim);
        }
        public void Update()
        {
            foreach(SSATransition transition in transitions)
            {
                if(transition.Eval(controller))
                {
                    controller.TransitionTo(transition.transitionTo);
                    return;
                }
            }
        }
    }
}

