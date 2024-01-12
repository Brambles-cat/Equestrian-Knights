using Modding;
using UnityEngine;
using Satchel;
using PonyMod.Ponies;
using HutongGames.PlayMaker.Actions;

namespace PonyMod
{
    public class PonyMod : Mod, ILocalSettings<LocalData>, IGlobalSettings<GlobalData>
    {
        public static string DataDirectory = AssemblyUtils.getCurrentDirectory();
        new public string GetName() => "Equestrian Knights";
        public override string GetVersion() => "v0.0.1";
        public static LocalData data { get; set; } = new LocalData();
        public static GlobalData globalSaveData { get; set; } = new GlobalData();


        public override void Initialize()
        {
            On.HeroController.Awake += SpriteAnimator.init;
            On.HeroController.Awake += InitHero;
            On.HeroController.Awake += InitHero;
            //On.HeroController.FixedUpdate += test;
            ModHooks.HeroUpdateHook += OnHeroUpdate;
            ModHooks.HeroUpdateHook += SpriteAnimator.AnimationUpdate;
            //On.HeroController.SceneInit += InitHero;
        }


        private void InitHero(On.HeroController.orig_Awake orig, HeroController self)
        {
            orig(self);
            //rb = HeroController.instance.GetComponent<Rigidbody2D>();
            PlayMakerFSM
                    color_fader = HeroController.instance.heroLight.gameObject.LocateMyFSM("color_fader"),
                    Light_Control = HeroController.instance.heroLight.gameObject.LocateMyFSM("HeroLight Control");

            Log($"{color_fader != null} {Light_Control != null}");
        }

        Vector3 offset = new Vector3(0, 0, 0);

        public void LogAllObjects(Transform parent, string tabs) {
            Log(tabs + parent.name);
            for(int i = 0; i < parent.childCount; ++i)
            {
                var child = parent.GetChild(i);
                if (child.gameObject.activeSelf)
                    LogAllObjects(parent.GetChild(i), tabs + "\t");
            }
        }

        public void OnHeroUpdate()
        {

            //HeroController.instance.AffectedByGravity(false);

            // unlock all ponies
            if(Input.GetKeyDown(KeyCode.U))
            {
                string[] ponies = { "twilight sparkle", "applejack", "fluttershy", "pinkie pie", "rainbow dash", "rarity" };
                foreach (var pony in ponies) {
                    if (!data.party.Contains(pony))
                        data.party.Add(pony);
                }
                //Log(HeroController.instance.heroLight.gameObject.LocateMyFSM("Light-FSM") == null);
                //Log(HeroController.instance.heroLight.gameObject.LocateMyFSM("Vignette-Darkness Control") == null);
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                //Log(HeroController.instance.heroLight.gameObject.LocateMyFSM("color_fader").FsmVariables.);
                GameObject[] o = HeroController.instance.gameObject.scene.GetRootGameObjects(); // .GetAllGameObjects();
                
                foreach (var ob in o)
                {
                    LogAllObjects(ob.transform, "");
                }
                //GameObject light = HeroController.instance.gameObject.scene.FindGameObject("Light");
                //Log(light);
                //if (light != null)
                //{
                //    light.LocateMyFSM("FSM").RemoveAction("Init", 3);
                //}
            }
            else if(Input.GetKeyDown(KeyCode.V)) {
                EaseColor c = HeroController.instance.heroLight.gameObject.LocateMyFSM("color_fader").GetAction<EaseColor>("Up", 3);
                c.Active = false;
                c.Enabled = false;
                /*HeroController.instance.heroLight.gameObject.LocateMyFSM("color_fader").InsertAction("Up", () =>
                {
                    Log("");
                }, 3);*/
            } else if (Input.GetKeyDown(KeyCode.H))
            {
                PonySwitcher.previousPony();
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                PonySwitcher.nextPony();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                HeroController.instance.GetComponent<MeshRenderer>().enabled = !HeroController.instance.GetComponent<MeshRenderer>().enabled;
            }

            // offset
            else if (Input.GetKeyDown(KeyCode.J))
            {
                offset.x += -0.1f;
                SpriteAnimator.displayedPony.transform.position = HeroController.instance.transform.position + offset;
                Log(offset);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                offset.x += 0.1f;
                SpriteAnimator.displayedPony.transform.position = HeroController.instance.transform.position + offset;
                Log(offset);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                offset.y += 0.1f;
                SpriteAnimator.displayedPony.transform.position = HeroController.instance.transform.position + offset;
                Log(offset);
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                offset.y += -0.1f;
                SpriteAnimator.displayedPony.transform.position = HeroController.instance.transform.position + offset;
                Log(offset);
            }
        }


        void ILocalSettings<LocalData>.OnLoadLocal(LocalData data) {
            PonyMod.data = data;
            Pony.currentPony = Pony.getFromStr(data.currentPony);
            Log($"current pony = {Pony.currentPony}");
        }
        public LocalData OnSaveLocal() => data;

        public void OnLoadGlobal(GlobalData data) => globalSaveData = data;
        public GlobalData OnSaveGlobal() => globalSaveData;
    }
}


/*
 public override List<(string, string)> GetPreloadNames()
        {
            if (GlobalSettings.Preloads)
            {
                return new List<(string, string)>
                {
                    ("Abyss_10", "higher_being/Dish Plat/Knight Dummy"),
                    ("Abyss_12", "Scream 2 Get/Cutscene Knight"),
                    ("Abyss_21", "Shiny Item DJ/Knight Cutscene"),
                    ("Crossroads_50", "Quirrel Lakeside/Sit Region/Knight Sit"),
                    ("Deepnest_East_12", "Hornet Blizzard Return Scene"),
                    ("Deepnest_Spider_Town", "RestBench Spider/Webbed Knight"),
                    ("Dream_Abyss", "End Cutscene/Dummy"),
                    ("GG_Door_5_Finale", "abyss_door_5_cutscene_sequence/main_chars"),
                    ("GG_Vengefly", "Boss Scene Controller/Dream Entry/Knight Dream Arrival"),
                    ("RestingGrounds_07", "Dream Moth/Knight Dummy"),
                    
                };   
            }
            
            return new List<(string, string)>();
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
*/