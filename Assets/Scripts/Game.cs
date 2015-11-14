using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Game : MonoBehaviour {

	public int CurrentPlayerNumber;

	public int CurrentFlagHolderID; 

	public int NumPlayers = 2;
	public UnityEngine.UI.Text NumPlayersLabel;

	public RectTransform NewGamePanel;

	public Player[] Players;
	public Player PlayerPrefab;

	public void SetPlayerCount (float value)
	{
		NumPlayers = (int)value * 2;
		NumPlayersLabel.text = NumPlayers.ToString();
	}

	public void StartGame ()
	{
		NewGamePanel.gameObject.SetActive (false);

		Players = new Player[NumPlayers];
		var teamNumbers = new List<int> ();
		for (int i = 0; i < NumPlayers / 2; ++i) {
			teamNumbers.Add(0);
			teamNumbers.Add(1);
		}

		for (int i = 0; i < NumPlayers; ++i) {
			Players[i] = Instantiate<Player>(PlayerPrefab);
			Players[i].PlayerNumber = i;
			
			int teamIndex = Random.Range(0, teamNumbers.Count);
			Players[i].TeamNumber = teamNumbers[teamIndex];
			teamNumbers.RemoveAt(teamIndex);
		}

		CurrentPlayerNumber = 0;
		BeginNewTurn();
	}

	public RectTransform GetReadyScreen;
	public UnityEngine.UI.Text GetReadyLabel;

	public void BeginNewTurn()
	{
		GetReadyScreen.gameObject.SetActive(true);
		GetReadyLabel.text = "Player " + (CurrentPlayerNumber + 1);	
	}

	public UnityEngine.UI.Text CurrentPlayerLabel;
	public UnityEngine.UI.Text CurrentTeamLabel;
	public RectTransform InGameUI;

	[System.Serializable]
	public struct TeamInfo
	{
		public string TeamName;
		public Color TeamColor;
	}
	public TeamInfo[] Teams;

	public void OnPlayerReady()
	{
		GetReadyScreen.gameObject.SetActive(false);
		InGameUI.gameObject.SetActive(true);

		CurrentPlayerLabel.text = "Player " + (CurrentPlayerNumber + 1);
		CurrentTeamLabel.text = Teams[Players[CurrentPlayerNumber].TeamNumber].TeamName;
		CurrentTeamLabel.color = Teams[Players[CurrentPlayerNumber].TeamNumber].TeamColor;
	}

	public void EndCurrentPlayerTurn()
	{
		InGameUI.gameObject.SetActive(false);
		CurrentPlayerNumber = (CurrentPlayerNumber + 1) % NumPlayers;
		BeginNewTurn();
	}

	public RectTransform SetSpawningUnitUI;
	public RectTransform SetSpawningUnitLabel;
	public RectTransform SetSpawningUnitType;

	public TrackingCollection tracker;
	public Unit spawningUnit;
	private void SetSpawningUnit(Unit u)
	{
		spawningUnit = u;
		spawningUnit.State = Unit.States.TypeNotSetYet;
		spawningUnit.Owner = Players[CurrentPlayerNumber];
		SetSpawningUnitLabel.gameObject.SetActive(false);
		SetSpawningUnitType.gameObject.SetActive(true);
	}

	public void BeginSpawnUnit()
	{
		InGameUI.gameObject.SetActive(false);
		SetSpawningUnitUI.gameObject.SetActive(true);
		SetSpawningUnitLabel.gameObject.SetActive(true);
		SetSpawningUnitType.gameObject.SetActive(false);
		tracker.FindNextUnusedTracker(SetSpawningUnit);
	}

	public void SpawnSoldier ()
	{
		SetSpawningUnitState (Unit.States.Soldier);
	}

	public void SpawnScout ()
	{
		SetSpawningUnitState (Unit.States.Scout);
	}

	public void SpawnBomb ()
	{
		SetSpawningUnitState (Unit.States.Bomb);
	}

	public void SpawnCannon()
	{
		SetSpawningUnitState (Unit.States.Cannon);
	}

	public void SetSpawningUnitState(Unit.States type)
	{
		spawningUnit.State = type;
		spawningUnit.OnSpawned();
		Players[CurrentPlayerNumber].OnSpawnedUnit(spawningUnit);

		SetSpawningUnitUI.gameObject.SetActive(false);

		EndCurrentPlayerTurn();
	}
}
