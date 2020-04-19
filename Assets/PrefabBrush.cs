using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class PrefabTile : TileBase
{
    public Sprite Sprite;
    public GameObject Prefab;
    public Vector3 Rotation;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = Sprite;
        if (tileData.gameObject == null)
            tileData.gameObject = Prefab;
        tileData.colliderType = Tile.ColliderType.Sprite;
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (go != null)
        {
            go.transform.localRotation = Quaternion.Euler(Rotation);
        }

        return true;
    }

}
