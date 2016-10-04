using UnityEngine;
using System.Collections;

// TODO Add variable to know should we switch enemy direction

public class TeleportationEnemy : MonoBehaviour
{

	public bool isLeft;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag != "Enemy")
			return;
			
		Camera camera = Camera.main;
		float height = 2f * camera.orthographicSize;
		float width = height * camera.aspect;
		Debug.Log (width);

		Vector3 initialPosition = other.transform.position;
		float newX = 0;
		if (isLeft) {
			newX = width - other.bounds.extents.x * 1.5f;
		} else {
			newX = other.bounds.extents.x * 1.5f;
		}

		other.transform.position = new Vector3 (
			newX,
			height,
			initialPosition.z
		);

		Vector2 initialVelocity = other.attachedRigidbody.velocity;
		other.attachedRigidbody.velocity = new Vector3 (
			-initialVelocity.x,
			initialVelocity.y
		);
	}
}