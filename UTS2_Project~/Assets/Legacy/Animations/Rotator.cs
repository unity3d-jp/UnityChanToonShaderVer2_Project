using UnityEngine;
using System.Collections;
namespace UnityEngine.Rendering.Toon.Samples
{
	public class Rotator : MonoBehaviour
	{

		private Vector3 pivot = Vector3.zero;
		public float speed = 20.0f;

		// Use this for initialization
		void Awake()
		{
			pivot = transform.position;
		}

		// Update is called once per frame
		void Update()
		{
			transform.RotateAround(pivot, Vector3.up, speed * Time.deltaTime);
		}
	}
}