using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 05/10/2019
 * Creator:     MoonJumpMania
 * Use:         Moves the camera in chosen direction in correspondace to the player
 * Application: Plug this script to camera in the scene
 * Variables:   offset variables move the camera away from the player
 *              booleans able and disable camera translation in specific axis
 *              bounce changes how smooth or slow the camera feels
 */

public class CamCtrl2D : MonoBehaviour
{
    public GameObject player;       //Public variable to store a reference to the player game object
    public float offsetY;           //y axis offset
    public float offsetX;           //X axis offset
    public float bounce;            //how bouncy or damped the camera feels
    public bool isAxisX;            //Permission to have the x parameter controlled
    public bool isAxisY;            //Permission to have the x parameter controlled

    private Vector3 target;         //Holds the direction from the camera to the player

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        //set the new position of camera
        target = new Vector3( (player.transform.position.x + offsetX) * System.Convert.ToInt16(isAxisX), //ToInt16 converts the bool to a 0 or 1 int value
                              (player.transform.position.y + offsetY) * System.Convert.ToInt16(isAxisY),
                              transform.position.z);

        transform.position = Vector3.Lerp( transform.position,
                                           target,
                                           Time.deltaTime * bounce);
    }
}
