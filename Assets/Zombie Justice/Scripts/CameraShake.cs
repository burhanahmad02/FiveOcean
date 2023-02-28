using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	Vector3 cameraInitialPosition;
	public float shakeMagnetude = 0.05f;
	public float shakeTime = 0.5f;
	public GameObject mainCamera;

	public void ShakeIt()
	{
		cameraInitialPosition = mainCamera.transform.position;
		InvokeRepeating ("StartCameraShaking", 0f, 0.005f);
		Invoke ("StopCameraShaking", shakeTime);
	}

	void StartCameraShaking()
	{
		float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
		float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
		Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
		cameraIntermadiatePosition.x += cameraShakingOffsetX;
		cameraIntermadiatePosition.y += cameraShakingOffsetY;
		mainCamera.transform.position = cameraIntermadiatePosition;
	}

	void StopCameraShaking()
	{
		CancelInvoke ("StartCameraShaking");
		mainCamera.transform.position = cameraInitialPosition;
	}

	public IEnumerator Shake(float duration, float magnitude)
	{
		Vector3 origionalPos = transform.localPosition;

		float elapsed = 0.0f;

		while (elapsed < duration)
		{
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;

			transform.localPosition = new Vector3(x, y, origionalPos.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = origionalPos;
	}
}