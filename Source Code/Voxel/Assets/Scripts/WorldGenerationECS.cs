using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class WorldGenerationECS : MonoBehaviour
{
    /* Prefabs for tiles */
    public GameObject dirtPrefab;
    public GameObject grassPrefab;
    public GameObject stonePrefab;

    public GameObject player; /* Player's position for procedural generation */

    public int width; /* Width of chunk */
    public int height; /* Height of chunk */
    public int xOff; /* X offset */
    public int yOff; /* Y Offset */

    private static int dirtSpace; /* Amount of dirt on the top */
    private static int stoneSpace; /* Amount of stone on the under the dirt */

    /* Entities and their manager */
    private EntityManager entMan;
    private Entity dirtEntity;
    private Entity grassEntity;
    private Entity stoneEntity;

    // Start is called before the first frame update
    private void Start()
    {
        entMan = World.Active.EntityManager;
        dirtEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(dirtPrefab, World.Active);
        grassEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(grassPrefab, World.Active);
        stoneEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(stonePrefab, World.Active);

    }


    void GenerateChunkECS(int x, int y) /* Generates the terrain in DOTS format */
    {

        for (int i = 0; i < width; i++)
        {
            height = UnityEngine.Random.Range(height - 1, height + 2);

            dirtSpace = UnityEngine.Random.Range(2, 3);
            stoneSpace = height - dirtSpace;

            for (int j = 0; j < stoneSpace; j++)
            {
                Entity stone = entMan.Instantiate(stoneEntity);
                entMan.SetComponentData(stone, new Translation { Value = new float3(i + x, j + y, 0f) });
                entMan.SetComponentData(stone, new Rotation { Value = quaternion.identity });

            }

            for (int j = stoneSpace; j < height; j++)
            {
                Entity dirt = entMan.Instantiate(dirtEntity);
                entMan.SetComponentData(dirt, new Translation { Value = new float3(i + x, j + y, 0f) });
                entMan.SetComponentData(dirt, new Rotation { Value = quaternion.identity });
            }

            Entity grass = entMan.Instantiate(grassEntity);
            entMan.SetComponentData(grass, new Translation { Value = new float3(i + x, height + y, 0f) });
            entMan.SetComponentData(grass, new Rotation { Value = quaternion.identity });
        }
    }


}