using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityComponent
{
    public class SpriteSheetAnimator : MonoBehaviour
    {
        
        public Sprite[] frames;
        private int currentFrame;
        private SpriteSheetAnimation anim;
        public SpriteRenderer render;
        public float timer;
        private Facing face;
        // Start is called before the first frame update
        void Start()
        {
            face = GetComponent<Facing>();
        }

        // Update is called once per frame
        void Update()
        {
            if(anim != null)
            {
                timer += Time.deltaTime;
                if (timer >= 1 / anim.fps)
                {
                    timer = 0;
                    NextFrame();
                }
            }
        }

        private void NextFrame()
        {
            currentFrame = (currentFrame + 1) % anim.frameCount;
            int nextFrame = anim.startFrame + currentFrame; 
            if (anim.use8Dir)
            {
                nextFrame += anim.frameCount * (int)face.GetFace();
            }
            render.sprite = frames[nextFrame];
        }
        public void SetAnimation(SpriteSheetAnimation animation)
        {
            this.anim = animation;
            currentFrame = 0;
        }
        
    }
}

