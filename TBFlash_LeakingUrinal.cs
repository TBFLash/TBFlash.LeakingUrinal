using Agent;
using SimAirport.Logging;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace TBFlash.LeakingUrinal
{
    public class TBFlash_LeakingUrinal : ToiletUtility
    {
        [JsonIgnore]
        private Dictionary<PlaceableObject.Orientation, List<SpriteDefinition>> definitions;
        [JsonIgnore]
        public List<Sprite> frontSprites;
        [JsonIgnore]
        public List<Sprite> leftSprites;
        [JsonIgnore]
        public List<Sprite> backSprites;
        [JsonIgnore]
        private readonly bool isTBFlashDebug = false;
        [JsonIgnore]
        private int currentIndex;

        public override object GetData(object obj = null)
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
            return base.GetData(obj);
        }

        public override void ReturnData(object obj)
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
            base.ReturnData(obj);
        }

        public override void OnConstructionComplete()
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
            base.OnConstructionComplete();
            UpdateVisual();
        }

        public override void AwakeComponent()
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
            base.AwakeComponent();
            definitions = new Dictionary<PlaceableObject.Orientation, List<SpriteDefinition>>
            {
                {
                    PlaceableObject.Orientation.Front,
                    new List<SpriteDefinition>()
                },
                {
                    PlaceableObject.Orientation.Left,
                    new List<SpriteDefinition>()
                },
                {
                    PlaceableObject.Orientation.Back,
                    new List<SpriteDefinition>()
                }
            };
            AddSprites(frontSprites, PlaceableObject.Orientation.Front);
            AddSprites(leftSprites, PlaceableObject.Orientation.Left);
            AddSprites(backSprites, PlaceableObject.Orientation.Back);
        }

        new public bool TryReserveForMaintenance()
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
            isBeingRepaired = true;
            return true;
        }

        new public void UnreserveForMaintenance()
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
            UpdateVisual();
            isBeingRepaired = false;
        }

        public override void ApplyEffects(Passenger agent)
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
            base.ApplyEffects(agent);
            UpdateVisual();
        }

        public void UpdateVisual()
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
            int index = (placeableObject.maintainable?.condition ?? 1f) < (placeableObject.maintainable?.RepairThreshold ?? .8f) ? 1 : 0;
            if (currentIndex != index)
            {
                POSprite posprite = placeableObject.sprites[0];
                PORenderer renderer = posprite.renderer;
                renderer?.Remove(posprite);
                if (placeableObject.facing == PlaceableObject.Orientation.Right)
                {
                    posprite.sd = definitions[PlaceableObject.Orientation.Left][index];
                    posprite.flipX = true;
                }
                else
                {
                    posprite.sd = definitions[placeableObject.facing][index];
                    posprite.flipX = false;
                }
                posprite = placeableObject.sprites[0];
                if (renderer == null)
                {
                    return;
                }
                renderer.Add(posprite);
                currentIndex = index;
            }
        }

        private void AddSprites(List<Sprite> source, PlaceableObject.Orientation orientation)
        {
            TBFlashLogger(Log.FromPool("").WithCodepoint());
            foreach (Sprite sprite in source)
            {
                definitions[orientation].Add(SpriteManager.GetSpriteDefinition(sprite.name, true));
            }
        }

        private void TBFlashLogger(Log log)
        {
            if (isTBFlashDebug)
            {
                Game.Logger.Write(log);
            }
        }
    }
}