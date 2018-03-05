using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityEngine.Tilemaps
{
    public class TileAnimated : TileBase
    {

        public Sprite[] Animation;
        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.identity;
            tileData.color = Color.white;
            if (Animation != null && Animation.Length > 0)
            {
                tileData.sprite = Animation[Animation.Length - 1];
            }
        }

        public override bool GetTileAnimationData(Vector3Int location, ITilemap tileMap, ref TileAnimationData tileAnimationData)
        {
            if (Animation.Length > 0)
            {
                tileAnimationData.animatedSprites = Animation;
                tileAnimationData.animationSpeed = 1;
                tileAnimationData.animationStartTime = 0;
                return true;
            }
            return false;
        }

    }
}