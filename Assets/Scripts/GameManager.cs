using System.Collections.Generic;
using GlobalEnums;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
	public GameState gameState;
	public LevelSettings[] levelSettings;
	private LevelSettings currentLevelSettings;

	[Header("Events")]
	public UnityEvent updateUI;

	[Header("ResourceCard Details")]
	public ResourceCardData[] available;
	public List<ResourceCardData> resourceCardDeck;
	public List<ClimateCardData> climateCardDeck;

	[Header("Prefab Management")]
	public GameObject resourceCardPrefab;
	public GameObject climateCardPrefab;
	public GameObject cardSlotPrefab;
	public GameObject playerHandArea;
	public GameObject inPlayArea;
	public GameObject climateEffectArea;

	[Header("ResourceCard Management")]
	public List<ResourceCard> inHand;
	public List<ResourceCard> inPlay;
	public List<ClimateCard> climateCards;

	[Header("Debugging")]
	public bool debugEnabled;
	public GameObject debugTools;

	public void Start() {
		currentLevelSettings = levelSettings[gameState.currentLevel-1];
		gameState.ResetResources(currentLevelSettings.startingResources);

		debugTools.SetActive(debugEnabled);

		InitPlayArea();
		InitPlayerHand();
 		updateUI.Invoke();
	}
	
	// Invoked whenever a ResourceCard is dropped on a CardSlot by the player.
	public void CardDropped(ResourceCard card) {
		if(card.IsPlayable) {
			gameState.DecreaseResources(card.cardData.cost);

			// Update Game State.
			inHand.Remove(card);
			inPlay.Add(card);
			gameState.doomMeter++;

			updateUI.Invoke();
		}
	}

	// Invoked whenever a ResourceCard is picked up by the player.
	public void CardPickup(ResourceCard card) {
		// Check if the card is allowed to be played.
		card.IsPlayable = IsCardPlayable(card);
	}

	public void DrawCard() {
		// Get a random card from the available cards.
		ResourceCardData cardToPlay = resourceCardDeck[Random.Range(0, resourceCardDeck.Count())];
		GameObject newCardObject = Instantiate(resourceCardPrefab);
		// Set the card data, and subscribe to the OnCardPickup event.
		ResourceCard newCard = newCardObject.GetComponent<ResourceCard>();
		newCard.InitCardData(cardToPlay);
		newCard.OnCardPickup += CardPickup;
		// Attach the card to the player hand.
		newCardObject.transform.SetParent(playerHandArea.transform);
		inHand.Add(newCard);
	}

	public void DrawClimateCard() {
		// Get a random card from the available cards.
		ClimateCardData cardToPlay = climateCardDeck[Random.Range(0, climateCardDeck.Count())];
		GameObject newCardObject = Instantiate(climateCardPrefab);
		// Set the card data, and subscribe to the OnCardPickup event.
		ClimateCard newCard = newCardObject.GetComponent<ClimateCard>();
		newCard.InitCardData(cardToPlay);
		// Attach the card to the player hand.
		newCardObject.transform.SetParent(climateEffectArea.transform);
		climateCards.Add(newCard);
	}

	public void EndPlayerTurn() {
		gameState.isPlayerTurn = false;
		// Handle the production of the cards that are in play.
		inPlay.ForEach((card) => gameState.AddResources(card.cardData.production));

		// Handle climate actions.
		climateCards.ForEach((card) => TriggerClimateAction(card));
		if(climateCards.Count() < currentLevelSettings.maxClimateEffects) {

		}

		DrawCard();
		updateUI.Invoke();
		gameState.isPlayerTurn = true;
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
	
	public bool IsCardPlayable(ResourceCard card) {
		foreach(Resource cost in card.cardData.cost) {
			if(gameState.resources[cost.type] - cost.value < 0) {
				return false;
			}
		}
		return true;
	}

	public void TriggerClimateAction(ClimateCard climateCard) {
		gameState.DecreaseResources(climateCard.cardData.cost);
		// Remove the card if it's no longer active.
		if(--climateCard.cardData.duration <= 0) Destroy(climateCard.gameObject);
	}

	//----------------------------------------------------
	// DEBUGGING METHODS
	//----------------------------------------------------
	public void ResetLevelData() {
		gameState.turnCounter = 1;
		gameState.doomMeter = 0;
	}

	public void ResetResources() {
		gameState.ResetResources(currentLevelSettings.startingResources);
		updateUI.Invoke();
	}

	// Destroys all cards in play and resets the cards in the players hands.
	public void ResetPlayerHand() {
		inHand.ForEach((card) => Destroy(card.gameObject));
		inHand.Clear();
		InitPlayerHand();
	}

	public void IncreaseDoomMeter() {
		gameState.doomMeter++;
		updateUI.Invoke();
	}

	public void TriggerWinState() {

	}

	public void TriggerLoseState() {
		
	}
}
