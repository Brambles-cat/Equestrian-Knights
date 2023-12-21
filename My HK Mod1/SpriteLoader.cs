using Logger = Modding.Logger;
using UnityEngine;
using System.IO;

namespace PonyMod {
    class SpriteLoader {

        static tk2dSpriteDefinition[] defs;
        public static HeroAnimationController animCtrl;
        static Texture2D pinkieTest;

        public static void init(On.HeroController.orig_Awake orig, HeroController self)
        {
            animCtrl = self.GetComponent<HeroAnimationController>();
            defs = self.GetComponent<tk2dSprite>().Collection.spriteDefinitions;
            pinkieTest = new Texture2D(4096, 4096);
            pinkieTest.LoadImage(File.ReadAllBytes($"{PonyMod.DataDirectory}/Pinkie.png"));
            orig(self);
            Logger.Log("awaken");
        }

        public static void LoadSprites()
        {

            tk2dSpriteAnimationClip
                clip = animCtrl.animator.GetClipByName("Run"),
                nextClip = animCtrl.animator.GetClipByName("Slash");
            HeroController.instance.gameObject.GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = pinkieTest;
            Logger.Log(clip.frames.Length);
            int i = 0;
            foreach(var frame in clip.frames)
            {
                Logger.Log(frame.spriteId);
                defs[frame.spriteId].indices = [0, 1, 2, 2, 3, 0];
                defs[frame.spriteId]
                    .uvs = [
                        new Vector2((float)(220 * i) / 4096, 1),
                        new Vector2((float)(220 * i) / 4096, 3902f / 4096f),
                        new Vector2((float)(220 * (i + 1)) / 4096, 3902f / 4096f),
                        new Vector2((float)(220 * (i + 1)) / 4096, 1)
                    ];
                defs[frame.spriteId]
                    .positions = [
                        new Vector3(-1.3f, 1.2f),
                        new Vector3(-1.3f, -1.4f),
                        new Vector3(1.3f, -1.4f),
                        new Vector3(1.3f, 1.2f)
                    ];
                ++i;
            }
            /*for (int i = 0; i < 16; ++i)
            {
                defs[i + 9].indices = [0, 1, 2, 2, 3, 0];
                defs[i + 9]
                    .uvs = [
                        new Vector2((float)(220 * i) / 4096, 1),
                        new Vector2((float)(220 * i) / 4096, 3902f / 4096f),
                        new Vector2((float)(220 * (i + 1)) / 4096, 3902f / 4096f),
                        new Vector2((float)(220 * (i + 1)) / 4096, 1)
                    ];
                defs[i + 9]
                    .positions = [
                        new Vector3(-1.3f, 1.1f),
                        new Vector3(-1.3f, -1.3f),
                        new Vector3(1.3f, -1.3f),
                        new Vector3(1.3f, 1.2f)
                    ];
            }*/

            clip.fps = 20;
            Logger.Log(clip.wrapMode);
            clip.wrapMode = tk2dSpriteAnimationClip.WrapMode.Loop;
            /*
            tk2dSpriteAnimationFrame[] frames = new tk2dSpriteAnimationFrame[16];
            int i2 = 0;
            for (;i2 < clip.frames.Length; ++i2)
            {
                frames[i2] = clip.frames[i2];
            }
            for(; i2 < 16; ++i2)
            {
                tk2dSpriteAnimationFrame frame = new tk2dSpriteAnimationFrame();
                frame.spriteId = nextClip.frames[i2 - clip.frames.Length].spriteId;
                frames[i2] = frame;
            }*/
            Logger.Log("Attempting build...");
            Logger.Log(clip.frames.Length);
            HeroController.instance.GetComponent<tk2dSprite>().ForceBuild();
        } // 1 5 9 7 3 13 15
    }
}
