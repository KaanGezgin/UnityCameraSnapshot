using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Snapshotcamera : MonoBehaviour
{
    Camera snapshotCamera;

    int resWidth = 256;
    int resHeight = 256;

    private void Awake()
    {
        snapshotCamera = GetComponent<Camera>();
        
        if(snapshotCamera.targetTexture == null)
        {
            snapshotCamera.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = snapshotCamera.targetTexture.width;
            resHeight = snapshotCamera.targetTexture.height;
        }
        snapshotCamera.gameObject.SetActive(false);
    }
    public void CallTakeSnapshot()
    {
        snapshotCamera.gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        if (snapshotCamera.gameObject.activeInHierarchy)
        {
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            snapshotCamera.Render();
            RenderTexture.active = snapshotCamera.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            byte[] bytes = snapshot.EncodeToPNG();
            string filename = SnapshotName();
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log("Snapshot taken");
            snapshotCamera.gameObject.SetActive(false);
        }
    }
    string SnapshotName()
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
            Application.dataPath,
            resWidth,
            resHeight,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

    }
}
