using System;
using System.Linq;
using System.Collections.Generic;

namespace GoFish {
	public class Deck {
		/// <summary>
		/// The list of cards currently in the deck
		/// </summary>
		List<Card> cards;
		
		/// <summary>
		/// A private RNG for shuffling the deck
		/// </summary>
		Random rand;

		/// <summary>
		/// The count of cards remaining in the deck
		/// </summary>
		public int Count {
			get {
				return this.cards.Count;
			}
		}

		public Deck(Random rand) {
			this.rand = rand;

			this.cards = new List<Card>();

			for (int i = 0; i < Card.SuitCount; i++) {
				for (int j = 1; j <= Card.CardNames.Length; j++) {
					cards.Add(new Card(j));
				}
			}
		}

		/// <summary>
		/// Swap two cards in the deck.
		/// </summary>
		/// <param name="index1">The first position to swap</param>
		/// <param name="index2">The second position to swap</param>
		private void SwapCards(int index1, int index2) {
			Card tmp = this.cards[index1];
			this.cards[index1] = this.cards[index2];
			this.cards[index2] = tmp;
		}

		/// <summary>
		/// Shuffles the deck using the Fisher-Yates shuffle algorithm.
		/// </summary>
		public void Shuffle() {
			for (int i = 0; i < this.Count - 2; i++) {
				int swapIndex = rand.Next(i, this.Count);
				SwapCards(i, swapIndex);
			}
		}

		/// <summary>
		/// Gets a card from the top of the deck.
		/// </summary>
		/// <returns>The card on top of the deck, or null if none left.</returns>
		public Card GetCard() {
			if (this.Count < 1) return null;
			Card ret = cards[0];
			cards.RemoveAt(0);
			return ret;
		}

		public override string ToString() {
			string str = "";

			this.cards.ForEach((card) => {
				str += card.ToString() + " ";
			});

			return str;
		}
	}
}