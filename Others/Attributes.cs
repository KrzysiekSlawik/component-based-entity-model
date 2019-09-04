using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityComponent
{
    public class Attributes : MonoBehaviour
    {
        public BoolAttribute canWalk = new BoolAttribute(true);
        public FloatAttribute moveSpeed = new FloatAttribute(2);
        public void Update()
        {

        }

    }
    [System.Serializable]
    public class FloatAttribute
    {
        [SerializeField]
        private float value;
        private Vector2[] temporaryEffect = new Vector2[10];
        
        public FloatAttribute(float value)
        {
            this.value = value;
        }
        public void update()
        {
            for(int i = 0; i < 10; i++)
            {
                temporaryEffect[i].x -= Time.deltaTime;
            }
        }
        public float Value()
        {
            float v = 0;
            foreach(Vector2 vec in temporaryEffect)
            {
                if(vec.x > 0)
                {
                    v += vec.y;
                }
            }
            return value + v;
        }
        public void AddPerm(float value)
        {
            this.value += value;
        }
        public void AddTemp(float time, float value)
        {
            for(int i = 0; i < 10; i++)
            {
                if(temporaryEffect[i].x < 0)
                {
                    temporaryEffect[i].x = time;
                    temporaryEffect[i].y = value;
                    return;
                }
            }
        }
    }
    [System.Serializable]
    public class BoolAttribute
    {
        [SerializeField]
        private bool value;
        private bool disable;
        private float disableTime;
        private bool resistance;
        private float resTime;

        public BoolAttribute(bool value)
        {
            this.value = value;
        }
        public void update()
        {
            if(disable)
            {
                disableTime -= Time.deltaTime;
                if (disableTime < 0)
                {
                    disable = false;
                }
            }
            if (resistance)
            {
                resTime -= Time.deltaTime;
                if (resTime < 0)
                {
                    resistance = false;
                }
            }
        }
        public bool Value()
        { 
            return value && (!disable || resistance);
        }
        public void AddDisable(float time)
        {
            disable = true;
            if(disableTime < time)
            {
                disableTime = time;
            }
        }
        public void AddResistance(float time)
        {
            resistance = true;
            if (resTime < time)
            {
                resTime = time;
            }
        }
    }
}
