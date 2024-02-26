using System.Collections.Generic;
using GlobalEnums;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
	public GameState gameState;
	public LevelSettings[] levelSettings;
	private LevelSettings currentLevelSettings;

	[Header("Debug Options")]
	public bool resetGameState = true;
	public bool resetResources = true;

	[Header("Events")]
	public UnityEvent updateUI;

	[Header("Card Details")]
	public CardData[] available;

	[Header("Card Management")]
	public GameObject inPlayArea;
	public GameObject cardSlotPrefab;
	public GameObject playerHandArea;
	public GameObject cardPrefab;

	public List<CardData> inDeck;
	public List<Card> inHand;
	public List<Card> inPlay;

	public void Start() {
		currentLevelSettings = levelSettings[gameState.currentLevel-1];
		if(resetResources) {
			foreach(ResourceType type in currentLevelSettings.startingResources.Keys) {
				gameState.resources[type] = currentLevelSettings.startingResources[type];
			}
		}
		if(resetGameState) gameState.Reset();
		InitPlayArea();
		InitPlayerHand();
 		updateUI.Invoke();
	}

	public void DrawCard() {
		// Get a random card from the available cards.
		CardData cardToPlay = inDeck[Random.Range(0, inDeck.Count())];
		GameObject newCardObject = Instantiate(cardPrefab);
		Card newCard = newCardObject.GetComponent<Card>();
		newCard.InitCardData(cardToPlay);
		// Attach the card to the player hand.
		newCardObject.transform.SetParent(playerHandArea.transform);
		inHand.Add(newCard);
	}

	public void InitPlayArea() {
		for (int i = 0; i < currentLevelSettings.startingCardSlots; i++) {
			// Create a Card Slot Object and subscribe to it's cardPlayed Event.
			GameObject cardSlotObject = Instantiate(cardSlotPrefab, inPlayArea.transform);
			CardSlot cardSlot = cardSlotObject.GetComponent<CardSlot>();
			cardSlot.OnCardPlayed += CardPlayed;
		}
		inPlay = inPlayArea.GetComponentsInChildren<Card>().ToList();
	}

	public void InitPlayerHand() {
		for (int i = 0; i < currentLevelSettings.startingHand; i++) {
			DrawCard();
		}
		inHand = playerHandArea.GetComponentsInChildren<Card>().ToList();
	}

	public void EndPlayerTurn() {
		gameState.isPlayerTurn = false;
		// Handle the production of the cards that are in play.
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
		inHand.Remove(card);
		inPlay.Add(card);
		gameState.doomMeter++;
		foreach(Resource cost in card.cardData.cost) {
			gameState.resources[cost.type] -= cost.value;
		}
 		updateUI.Invoke();
	}
}
