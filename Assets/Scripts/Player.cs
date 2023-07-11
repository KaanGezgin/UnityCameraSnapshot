using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Snapshotcamera snapCam;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            snapCam.CallTakeSnapshot();
        }
    }
}
