using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[Header("Target to Follow")]
	public Transform player;

	[Header("Follow Settings")]
	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	private void FixedUpdate()
	{
		if (player != null)
		{
			// Desired position of the camera
			Vector3 desiredPosition = player.position + offset;

			// Smoothly interpolate between current position and desired position
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

			// Update the camera position
			transform.position = smoothedPosition;
		}
	}
}
