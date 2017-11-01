using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UsefulThings {
	public class CameraMovement : MonoBehaviour {

		private Vector3 _originalPosition;

		void Start() {
			_originalPosition = transform.localPosition;
		}

		void Update() {
			transform.localPosition = _originalPosition + CameraShake.CameraShakeOffset;
		}

		public void OnTriggerCameraShake() {
			CameraShake.ShakeCamera(1, 0.2f);
		}
	}
}