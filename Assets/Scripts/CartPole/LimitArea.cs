using System;
using UnityEngine;

public class LimitArea : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("LimitArea OnTriggerEnter");
		CartPoleController.Instance.Reset();
	}
}