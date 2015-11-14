using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine.Events;

public class TrackingCollection : MonoBehaviour {

	ImageTargetBehaviour[] _targets;
	public void Awake ()
	{
		_targets = new ImageTargetBehaviour[transform.childCount];
		for (int i = 0; i < transform.childCount; ++i) {
			_targets[i] = transform.GetChild(i).GetComponent<ImageTargetBehaviour>();
		}
	}

	public Coroutine FindNextUnusedTracker (System.Action<Unit> action)
	{
		return StartCoroutine(FindNextUnusedTrackerCoroutine(action));
	}

	private IEnumerator FindNextUnusedTrackerCoroutine (System.Action<Unit> action)
	{
		while (true) {
			var candidates = new List<Unit> ();
			foreach (var tracker in _targets) {
				if (tracker.CurrentStatus == TrackableBehaviour.Status.NOT_FOUND)
					continue;
				if (tracker.CurrentStatus == TrackableBehaviour.Status.UNDEFINED)
					continue;
				if (tracker.CurrentStatus == TrackableBehaviour.Status.UNKNOWN)
					continue;

				var unit = tracker.GetComponent<Unit> ();
				if (unit.Owner != null)
					continue;

				action(unit);
				yield break;
			}

			yield return null;
		}		
	}
}
