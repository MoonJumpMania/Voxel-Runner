using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposeBlocks : MonoBehaviour
{
    void Update()
    {
        if (Mathf.Abs(Camera.main.transform.position.x - transform.position.x) > WorldGeneration.rangeProxy * 2)
        {
            Destroy(gameObject);
        }
        else if (Mathf.Abs(transform.position.y - Camera.main.transform.position.y) > WorldGeneration.rangeProxy)
        {
            Destroy(gameObject);
        }
    }
}
