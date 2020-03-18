using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Tilemaps
{
    public class TilemapGeneration : MonoBehaviour
    {
        public GameObject player; /* Player's position for procedural generation */

        /* Block grid */
        public Tilemap tilemap;
        public Tile dirtTile;
        public Tile grassTile;
        public Tile stoneTile;

        public int height; /* Height of chunk */
        public int range; /* Spawn range */
        public int randomness; /* How flat or random the terrain will be */
        public int chunkSize; /* Size of each block chunk */

        private static int _dirtSpace; /* Amount of dirt on the top */
        private static int _stoneSpace; /* Amount of stone on the under the dirt */

        private static int _previousChunk; /* Saves the position of the last touched chunk */
        private static int _newChunk; /* Saves the position of the newly touched chunk */

        private static int _rangeProxy = int.MaxValue; /* Proxy for the DisposeBlocks class */

        private void Start()
        {
            _rangeProxy = range;
            player.transform.position = new Vector2(0f, (float) height + 2);
            for (var i = -range / 2; i < range * 4 + 1; i++) /* Generates the starting area */
            {
                GeneratePlain(i);
            }

            _previousChunk = (int) player.transform.position.x / chunkSize;
        }

        private void FixedUpdate()
        {
            _newChunk = (int) player.transform.position.x /
                        chunkSize; /* Every few blocks, the player goes to another chunk */
            if (_newChunk != _previousChunk + 1)
            {
                return;
            }
            
            for (var i = 0; i < chunkSize; i++) /* Generates one single chunk */
            {
                GeneratePlain((_newChunk + 1) * chunkSize + range - 1 + i);
                _previousChunk = _newChunk;
            }

            _newChunk = 0;
        }

        private void RandomHeight() /* Moves the height either by one up or down */
        {
            var random = Random.Range(0, 100);

            if (random < randomness)
            {
                height -= 1;
            }
            else if (randomness < random && random < randomness * 2)
            {
                height += 1;
            }
        }

        private void GeneratePlain(int location) /* Generates the terrain */
        {
            RandomHeight();

            _dirtSpace = Random.Range(3, 5);
            _stoneSpace = height - _dirtSpace;

            for (var i = 0; i < _stoneSpace; i++)
            {
                tilemap.SetTile(new Vector3Int(location, i, 0), stoneTile);
            }

            for (var i = _stoneSpace; i < height; i++)
            {
                tilemap.SetTile(new Vector3Int(location, i, 0), dirtTile);
            }

            tilemap.SetTile(new Vector3Int(location, height, 0), grassTile);
        }
    }
}