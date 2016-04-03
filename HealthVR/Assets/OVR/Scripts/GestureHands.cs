using UnityEngine;
using System.Collections;
using Leap;

public class GestureHands : MonoBehaviour {

	Controller controller;
	public GameObject pointOne;
	public GameObject pointTwo;
	public GameObject cameraVR;
	public GameObject hands;
	public GameObject plaque;
	int whereAt;
	int isReadyToChange;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
		controller.EnableGesture (Gesture.GestureType.TYPE_SWIPE);
		controller.EnableGesture (Gesture.GestureType.TYPE_KEY_TAP);
		whereAt = 0;
		isReadyToChange = 0;
		// controller.Config.SetFloat ("Gesture.Swipe.MinLength", 200.0f);
		// controller.Config.SetFloat("Gesture.Swipe.MinVelocity", 750f);
		// controller.Config.Save ();
	}

	void DeactivateChildren(GameObject g, bool a) {
		g.activeSelf = a;
		foreach (Transform child in g.transform) {
			DeactivateChildren (child.gameObject, a);
		}
	}

	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame ();
		GestureList gestures = frame.Gestures ();
		if (isReadyToChange > 40) {
			for (int i = 0; i < gestures.Count; i++) {
				Gesture g = gestures [i];
				if (g.Type == Gesture.GestureType.TYPE_KEY_TAP) {
					Debug.Log ("Key Tap");
					if (whereAt == 0) {
						// -0.72, -0.97, 0.81
						// 0, 0, 0
						//Quaternion.
						//hands.gameObject.transform.position = new Vector3(-0.72f, -0.97f, 0.81f);
						cameraVR.gameObject.transform.position = new Vector3 (pointOne.transform.position.x,
							pointOne.transform.position.y, pointOne.transform.position.z);
						whereAt++;
					} else if (whereAt == 1) {
						// 0.72, 0.97, -0.81
						// 0, 90, 0
						//hands.gameObject.transform.position = new Vector3(0.72f, 0.97f, -0.81f);
						cameraVR.transform.position = new Vector3 (pointTwo.transform.position.x,
							pointTwo.transform.position.y, pointTwo.transform.position.z);
						whereAt++;
					}
				} else if (g.Type == SwipeGesture.ClassType ()) {
					// transition to other scene
					Debug.Log ("Swipe");
					DeactivateChildren (plaque, !plaque.activeSelf);
				}
				isReadyToChange = 0;
			}
		} else {
			isReadyToChange++;
		}

	}
}
