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
	public bool resetLevel = true;
	public bool resetResources = true;

	[Header("Events")]
	public UnityEvent updateUI;

	[Header("Card Details")]
	public ResourceCardData[] available;
	public List<ResourceCardData> inDeck;

	[Header("Prefab Management")]
	public GameObject cardPrefab;
	public GameObject cardSlotPrefab;
	public GameObject playerHandArea;
	public GameObject inPlayArea;
	public GameObject climateEffectArea;

	[Header("Card Management")]
	public List<Card> inHand;
	public List<Card> inPlay;
	public List<ClimateCard> climateCards;

	public void Start() {
		currentLevelSettings = levelSettings[gameState.currentLevel-1];

		// Reset for testing purposes.
		if(resetLevel) gameState.ResetLevel();
		if(resetResources) gameState.ResetResources(currentLevelSettings.startingResources);

		InitPlayArea();
		InitPlayerHand();
 		updateUI.Invoke();
	}

	public void DrawCard() {
		// Get a random card from the available cards.
		ResourceCardData cardToPlay = inDeck[Random.Range(0, inDeck.Count())];
		GameObject newCardObject = Instantiate(cardPrefab);
		Card newCard = newCardObject.GetComponent<Card>();
		// Set the card data, and subscribe to the OnCardPickup event.
		newCard.InitCardData(cardToPlay);
		newCard.OnCardPickup += CardPickup;
		// Attach the card to the player hand.
		newCardObject.transform.SetParent(playerHandArea.transform);
		inHand.Add(newCard);
	}

	public void InitPlayArea() {
		for (int i = 0; i < currentLevelSettings.maxCardSlots; i++) {
			// Create a Card Slot Object and subscribe to it's cardPlayed Event.
			GameObject cardSlotObject = Instantiate(cardSlotPrefab, inPlayArea.transform);
			CardSlot cardSlot = cardSlotObject.GetComponent<CardSlot>();
			cardSlot.OnCardDropped += CardDropped;
		}

		for (int i = 0; i < currentLevelSettings.maxClimateEffects; i++) {
			// Create a Card Slot Object and subscribe to it's cardPlayed Event.
			GameObject cardSlotObject = Instantiate(cardSlotPrefab, climateEffectArea.transform);
			CardSlot cardSlot = cardSlotObject.GetComponent<CardSlot>();
			cardSlot.ClimateSlot = true;
		}
	}

	public void InitPlayerHand() {
		for (int i = 0; i < currentLevelSettings.startingHand; i++) {
			DrawCard();
		}
	}

	public void EndPlayerTurn() {
		gameState.isPlayerTurn = false;
		// Handle the production of the cards that are in play.
		foreach(Card card in inPlay) {
			foreach(Resource production in card.cardData.production) {
				gameState.resources[production.type] += production.value;
			}
		}

		foreach(ClimateCard card in climateCards) {
			card.TriggerClimateEffect();
		}

		DrawCard();
		updateUI.Invoke();
		gameState.isPlayerTurn = true;
	}

	// Invoked whenever a Card is picked up by the player.
	public void CardPickup(Card card) {
		// Check if the card is allowed to be played.
		card.IsPlayable = IsCardPlayable(card);
	}

	// Invoked whenever a Card is dropped on a CardSlot by the player.
	public void CardDropped(Card card) {
		if(card.IsPlayable) {
			// Reduces the card cost from the players resources.
			foreach(Resource cost in card.cardData.cost) {
				gameState.resources[cost.type] -= cost.value;
			}

			// Update Game State.
			inHand.Remove(card);
			inPlay.Add(card);
			gameState.doomMeter++;

			updateUI.Invoke();
		}
	}

	public void TriggerClimateAction(ClimateCard climateCard) {
		// If the climate card is still active.
		if(climateCard.cardData.duration-- > 0) {

		} else {
			Destroy(climateCard.transform);
		}
		foreach(Resource cost in climateCard.cardData.cost) { 
			gameState.resources[cost.type] -= cost.value;
		}
	}

	public bool IsCardPlayable(Card card) {
		foreach(Resource cost in card.cardData.cost) {
			if(gameState.resources[cost.type] - cost.value < 0) {
				return false;
			}
		}
		return true;
	}
}
