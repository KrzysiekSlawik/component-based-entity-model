using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
namespace EntityComponent
{
    public class Brain : MonoBehaviour
    {
        public EntityMovement movement;
        private Grid grid;
        public string startState;
        private BState currentState;
        public BState[] editorStates;
        private Dictionary<string, BState> states;
        public Transform[] players;
        public Transform self;
        // Start is called before the first frame update
        void Start()
        {
            states = new Dictionary<string, BState>();
            foreach(BState s in editorStates)
            {
                s.behaviour.LoadBrain(this);
                foreach(BPair p in s.transitions)
                {
                    p.transition.Link(this, p.transitionTo);
                }
                states.Add(s.name, s);
            }
            currentState = states[startState];
            self = GetComponent<Transform>();
        }
        // Update is called once per frame
        void Update()
        {
            currentState.behaviour.Update();
            currentState = states[currentState.EvalTransitions()];
        }
        public Vector3 PlayerPosition()
        {
            Vector3 pos = self.position;
            float distance = float.PositiveInfinity;
            foreach(Transform p in players)
            {
                if((p.position - self.position).sqrMagnitude < distance)
                {
                    pos = p.position;
                }
            }
            return pos;
        }
    }
}
