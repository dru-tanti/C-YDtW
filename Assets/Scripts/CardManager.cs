using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CardManager : MonoBehaviour {
	public CardData[] available;
	public UnityEvent events;
	public GameObject cardPrefab;
	public GameObject playerHandArea;
	public GameObject inPlayArea;
	public List<CardData> inDeck;
	public List<Card> inHand;
	public List<Card> inPlay;

	void Start() {
		inHand = playerHandArea.GetComponentsInChildren<Card>().ToList<Card>();
		inPlay = inPlayArea.GetComponentsInChildren<Card>().ToList<Card>();
		StartingDeck();
	}

	void StartingDeck() {
		InvokeRepeating("DrawCard", 0, 1);
	}

	void DrawCard() {
		if(inHand.Count() > 5) CancelInvoke();
		CardData cardToPlay = inDeck[Random.Range(0, inDeck.Count())];
		GameObject newCardObject = Instantiate(cardPrefab);
		Card newCard = newCardObject.GetComponent<Card>();
		newCardObject.transform.SetParent(playerHandArea.transform);
		newCard.InitCardData(cardToPlay);
		inHand.Add(newCard);
		events.AddListener(newCard.OnCardPlay);
		// events.AddListener(newCard.GetComponent<Card>().OnCardPlay);
	}

	public void CardPlayed(Card card) {
		inHand.Remove(card);
		inPlay.Add(card);
	}
}
