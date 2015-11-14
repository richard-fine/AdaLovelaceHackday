using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour {

	[SyncVar]
	public int OwnerID;
	
	[SyncVar]
	public string ARCode;
}
