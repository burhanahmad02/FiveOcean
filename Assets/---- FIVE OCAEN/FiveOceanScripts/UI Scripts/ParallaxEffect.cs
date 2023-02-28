using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    //private GameObject mainCamera;
    private float playerSpeed;
    [SerializeField]
    private float bgMoveSpeed;
    private float directionX;

    private PlayerMobileInput playerMovementScript;
    private CameraFollow cameraFollowScript;
    private Transform mainCameraTransform;

    private void Start()
    {
        playerMovementScript = PlayerStaticInstance.Instance.gameObject.GetComponent<PlayerMobileInput>();
        cameraFollowScript = Camera.main.gameObject.GetComponent<CameraFollow>();
        mainCameraTransform = cameraFollowScript.gameObject.transform;
    }
    void Update()
    {
        if (!playerMovementScript)
        {
            if (!PlayerStaticInstance.Instance)
            {
                return;
            }
            else
            {
                playerMovementScript = PlayerStaticInstance.Instance.gameObject.GetComponent<PlayerMobileInput>();
            }
        }
        if (playerMovementScript)
        {
            if (playerMovementScript.dirX != 0)
            {
                //if (mainCameraTransform.position.x >= cameraFollowScript.rightLimit || mainCameraTransform.position.x <= cameraFollowScript.leftLimit)
                //{
                //    return;
                //}
              /*  if (CameraFollow.isRestricting)
                {
                    return;
                }
                else
                {*/
                    //playerSpeed = Camera.main.transform.position.x;


                    playerSpeed = -playerMovementScript.dirX;
                    playerSpeed = Mathf.Clamp(playerSpeed, -30, 40);
                    directionX = playerSpeed * bgMoveSpeed * Time.deltaTime;
                    transform.position = new Vector2(transform.position.x + directionX, transform.position.y);
                //}
            }
        }
    }
}