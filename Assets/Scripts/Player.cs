using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public int PlayerNumber;
	public int TeamNumber;

	public int NumSoldiersRemaining = 3;

	public int NumCannonsRemaining = 1;
	
	public int NumBombsRemaining = 1;

	public int NumScoutsRemaining = 1;

	public void OnSpawnedUnit (Unit u)
	{
		switch (u.State) {
		case Unit.States.Soldier:
			--NumSoldiersRemaining;
			break;
		case Unit.States.Cannon:
			--NumCannonsRemaining;
			break;
		case Unit.States.Scout:
			--NumScoutsRemaining;
			break;
		case Unit.States.Bomb:
			--NumBombsRemaining;
			break;
		}
	}
}
