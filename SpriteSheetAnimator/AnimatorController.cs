using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSAState;
namespace EntityComponent
{
    
    public class AnimatorController : MonoBehaviour
    {
        private Dictionary<string, float> floatVariables;
        public string[] editorFloatVariables;   
        private Dictionary<string, bool> boolVariables;
        public string[] editorBoolVariables;

        public SpriteSheetAnimator animator;

        public SSAStateBody[] editorStates;
        private Dictionary<string, SSAStateBody> states;
        private SSAStateBody defaultState;

        private SSAStateBody currentState;


        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<SpriteSheetAnimator>();
        }
        private void OnValidate()
        {
            floatVariables = new Dictionary<string, float>();
            foreach (string var in editorFloatVariables)
            {
                floatVariables.Add(var, 0);
            }
            boolVariables = new Dictionary<string, bool>();
            foreach (string var in editorBoolVariables)
            {
                boolVariables.Add(var, false);
            }
            states = new Dictionary<string, SSAStateBody>();
            for(int i = 0; i < editorStates.Length; i++)
            {
                states.Add(editorStates[i].name, editorStates[i]);
                states[editorStates[i].name].Inject(this);

                if(editorStates[i].defaultState)
                {
                    defaultState = editorStates[i];
                    currentState = editorStates[i];
                    animator.SetAnimation(currentState.anim);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            currentState.Update();
        }
        public void SetFloat(string key, float value)
        {
            if (floatVariables.ContainsKey(key))
            {
                floatVariables[key] = value;
            }
        }
        public void SetBool(string key, bool value)
        {
            if (boolVariables.ContainsKey(key))
            {
                boolVariables[key] = value;
            }
        }
        public float GetFloat(string key)
        {
            if (floatVariables.ContainsKey(key))
            {
                return floatVariables[key];
            }
            return 0;
        }
        public bool GetBool(string key)
        {
            if (boolVariables.ContainsKey(key))
            {
                return boolVariables[key];
            }
            return false;
        }

        public void TransitionTo(string stateName)
        {
            currentState = states[stateName];
            currentState.Enter();
        }

    }
}
