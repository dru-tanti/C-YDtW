using System.Collections.Generic;
using GlobalEnums;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
	public GameState gameState;
	public LevelSettings[] levelSettings;

	[Header("Debug Options")]
	public bool resetGameState = true;
	public bool resetResources = true;

	[Header("Events")]
	public UnityEvent updateUI;

	[Header("Card Details")]
	public GameObject cardPrefab;
	public CardData[] available;

	[Header("Card Management")]
	public GameObject playerHandArea;
	public GameObject inPlayArea;
	public List<CardData> inDeck;
	public List<Card> inHand;
	public List<Card> inPlay;

	public void Start() {
		if(resetResources) {
			foreach(ResourceType type in levelSettings[gameState.currentLevel-1].startingResources.Keys) {
				gameState.resources[type] = levelSettings[gameState.currentLevel-1].startingResources[type];
			}
		}
		if(resetGameState) gameState.Reset();

 		updateUI.Invoke();

		inHand = playerHandArea.GetComponentsInChildren<Card>().ToList();
		inPlay = inPlayArea.GetComponentsInChildren<Card>().ToList();
	}

	public void DrawCard() {
		CardData cardToPlay = inDeck[Random.Range(0, inDeck.Count())];
		GameObject newCardObject = Instantiate(cardPrefab);
		Card newCard = newCardObject.GetComponent<Card>();
		newCardObject.transform.SetParent(playerHandArea.transform);
		newCard.InitCardData(cardToPlay);
		inHand.Add(newCard);
	}

	public void EndPlayerTurn() {
		gameState.isPlayerTurn = false;
		foreach(Card card in inPlay) {
			foreach(Resource production in card.cardData.production) {
				gameState.resources[production.type] += production.value;
			}
		}
		DrawCard();
		updateUI.Invoke();
		gameState.isPlayerTurn = true;
	}

	public void CardPlayed(Card card) {
		Debug.Log("Card Played");
		inHand.Remove(card);
		inPlay.Add(card);
		gameState.doomMeter++;
		foreach(Resource cost in card.cardData.cost) {
			gameState.resources[cost.type] -= cost.value;
		}
 		updateUI.Invoke();
	}
}
