using Satchel;
using System;
using Logger = Modding.Logger;
using UnityEngine;

namespace PonyMod
{
    // basically copy paste from satchel but with slight changes
    public class AnimController : MonoBehaviour
    {
        public Satchel.Animation anim;

        public Sprite[] sprites;

        public int currentFrame;

        /// <summary>
        /// Used to prevent animations with higher priorities <br/>
        /// from being replaced by lower priority ones <br/>
        /// until they finish playing at least once <br/>
        /// </summary>
        public int priority = 0;

        public bool
            animating,
            playingBackwards;

        private SpriteRenderer sr;

        private DateTime lastFrameChange;

        public void Start()
        {
            sr = base.gameObject.GetAddComponent<SpriteRenderer>();
        }

        public void Update()
        {
            if (!animating) return;

            if ((DateTime.Now - lastFrameChange).TotalMilliseconds > (double)(1000f / anim.fps))
            {

                if (playingBackwards)
                {
                    --currentFrame;

                    if (currentFrame < 0)
                    {
                        currentFrame = anim.loop ? sprites.Length - 1 : 0;
                        priority = 0;
                    }
                }
                else
                {
                    ++currentFrame;

                    if (currentFrame >= sprites.Length)
                    {
                        currentFrame = anim.loop ? 0 : sprites.Length - 1;
                        priority = 0;
                    }
                }

                lastFrameChange = DateTime.Now;
            }

            if (sr == null) sr = base.gameObject.GetAddComponent<SpriteRenderer>();

            sr.sprite = sprites[currentFrame];
        }

        public bool Play(Satchel.Animation animation, int priority = 0, bool playBackwards = false) {
            if (this.priority > priority) return false;

            anim = animation;
            sprites = CustomAnimation.loadedSprites[anim];

            playingBackwards = playBackwards;
            Reset();
            Logger.Log(animation.frames[currentFrame]);
            this.priority = priority;

            lastFrameChange = DateTime.MinValue;
            return animating = true;
        }

        public void Play(bool playBackwards = false) {
            if (anim == null)
            {
                Logger.LogError("No animation to play");
                return;
            }
            playingBackwards = playBackwards;
            animating = true;
        }

        public void Reset() {
            currentFrame = playingBackwards ? sprites.Length - 1 : 0;
        }

        public void Pause() { animating = false; }
    }
}
