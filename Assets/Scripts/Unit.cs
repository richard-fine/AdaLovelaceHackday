using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Unit : MonoBehaviour {

	public Player Owner;
	public enum States
	{
		Unspawned,
		TypeNotSetYet,
		Soldier,
		Scout,
		Bomb,
		Cannon
	}

	public States State;

	public Transform SoldierPrefab;
	public Transform ScoutPrefab;
	public Transform BombPrefab;
	public Transform CannonPrefab;

	public Transform ActiveInstance;

	public void OnSpawned ()
	{
		switch (State) {
		case States.Soldier:
			ActiveInstance = Instantiate<Transform>(SoldierPrefab);
			break;
		case States.Scout:
			ActiveInstance = Instantiate<Transform>(ScoutPrefab);
			break;
		case States.Bomb:
			ActiveInstance = Instantiate<Transform>(BombPrefab);
			break;
		case States.Cannon:
			ActiveInstance = Instantiate<Transform>(CannonPrefab);
			break;
		}

		ActiveInstance.transform.parent = transform;
		ActiveInstance.transform.localPosition = Vector3.zero;
		ActiveInstance.transform.localRotation = Quaternion.identity;
	}
}
