using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    /* Prefabs for tiles */
    public GameObject dirtPrefab;
    public GameObject grassPrefab;
    public GameObject stonePrefab;

    public GameObject player; /* Player's position for procedural generation */

    public int height; /* Height of chunk */
    public int range; /* Spawn range */
    public int randomness; /* How flat or random the terrain will be */
    public int chunkSize; /* Size of each block chunk */

    private static int dirtSpace; /* Amount of dirt on the top */
    private static int stoneSpace; /* Amount of stone on the under the dirt */

    /* Saves the previous and next chunks */
    private static int previousChunk;
    private static int newChunk;

    public static int rangeProxy; /* Proxy for the DisposeBlocks class */

    private void Start()
    {
        rangeProxy = range;
        player.transform.position = new Vector2(0f, (float)height + 2);
        for (int i = -range / 2; i < range * 4; i++) /* Generates the starting area */
        {
            GeneratePlain(i);
        }
        previousChunk = (int)player.transform.position.x / chunkSize;
    }

    private void FixedUpdate()
    {
        newChunk = (int)player.transform.position.x / chunkSize; /* Every few blocks, the player goes to another chunk */
        if (newChunk == previousChunk + 1) /* Checks for the next chunk */
        {
            for (int i = 0; i < chunkSize; i++) /* Generates one single chunk */
            {
                GeneratePlain((newChunk + 1)  * chunkSize + range - 1 + i);
                previousChunk = newChunk;
            }
            newChunk = 0;
        }
    }

    void RandomHeight() /* Moves the height either by one up or down */
    {
        int random = Random.Range(0, 100);

        if (random < randomness)
        {
            height -= 1;
        }
        else if (randomness < random && random < randomness * 2)
        {
            height += 1;
        }
    }

    void ReverseRandomHeight()
    {
        int random = Random.Range(0, 100);

        if (random < randomness)
        {
            height += 1;
        }
        else if (randomness < random && random < randomness * 2)
        {
            height -= 1;
        }
    }

    void GeneratePlain(int location) /* Generates the terrain */
    {
        RandomHeight();

        dirtSpace = Random.Range(3, 5);
        stoneSpace = height - dirtSpace;

        for (int i = 0; i < stoneSpace; i++)
        {
            Instantiate(stonePrefab, new Vector2(location, i), Quaternion.identity);
        }
           
        for (int i = stoneSpace; i < height; i++)
        {
            Instantiate(dirtPrefab, new Vector2(location, i), Quaternion.identity);
        }
         
        Instantiate(grassPrefab, new Vector2(location, height), Quaternion.identity);
    }
}