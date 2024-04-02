using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;

    private void Start()
    {
        LoadPositions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SavePositions();
        }
    }

    private void SavePositions()
    {
        PlayerPrefs.SetFloat("PlayerPosX", playerTransform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerTransform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerTransform.position.z);

        PlayerPrefs.SetFloat("CameraPosX", cameraTransform.position.x);
        PlayerPrefs.SetFloat("CameraPosY", cameraTransform.position.y);
        PlayerPrefs.SetFloat("CameraPosZ", cameraTransform.position.z);

        PlayerPrefs.Save();
    }

    private void LoadPositions()
    {
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float playerPosX = PlayerPrefs.GetFloat("PlayerPosX");
            float playerPosY = PlayerPrefs.GetFloat("PlayerPosY");
            float playerPosZ = PlayerPrefs.GetFloat("PlayerPosZ");

            playerTransform.position = new Vector3(playerPosX, playerPosY, playerPosZ);
        }

        if (PlayerPrefs.HasKey("CameraPosX"))
        {
            float cameraPosX = PlayerPrefs.GetFloat("CameraPosX");
            float cameraPosY = PlayerPrefs.GetFloat("CameraPosY");
            float cameraPosZ = PlayerPrefs.GetFloat("CameraPosZ");

            cameraTransform.position = new Vector3(cameraPosX, cameraPosY, cameraPosZ);
        }
    }
}
