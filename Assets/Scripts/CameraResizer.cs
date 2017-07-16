using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    public float TargetScreenHeight = 1920;
    public float TargetScreenWidth = 1080;

    void Start()
    {
        Camera mainCamera = GetComponent<Camera>();
        float orthographicSize = mainCamera.orthographicSize;
        float targetAspect = TargetScreenWidth / TargetScreenHeight;

        // Calculating ortographic width
        float orthoWidth = orthographicSize / TargetScreenHeight * TargetScreenWidth;
        // Setting aspect ration
        orthoWidth = orthoWidth / (targetAspect / mainCamera.aspect);
        // Setting Size
        mainCamera.orthographicSize = (orthoWidth / Screen.width * Screen.height);

        Debug.Log("new: " + mainCamera.orthographicSize + ", " + (orthoWidth / Screen.width * Screen.height));
    }

}
