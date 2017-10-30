using System.Collections.Generic;

namespace GoFish {
	public abstract class Player {
		public Hand Hand { get; protected set; }

		/// <summary>
		/// Gets the guess from this player.
		/// </summary>
		/// <returns>The Card the player guessed.</returns>
		public abstract Card GetGuess();

		/// <summary>
		/// Handles the guess of the opposite player.
		/// </summary>
		/// <param name="guess">The Card the player guessed.</param>
		/// <returns>The Cards retrieved from our hand.</returns>
		public abstract List<Card> HandleGuess(Card guess);

		/// <summary>
		/// Draw a card from the given deck.
		/// </summary>
		/// <param name="deck">The deck of cards to pull from</param>
		/// <returns>The card that was drawn</returns>
		public Card DrawCard(Deck deck) {
			Card card = deck.GetCard();
			if (card != null) {
				this.Hand.Add(card);
			}
			return card;
		}

		/// <summary>
		/// Determines if the player has more cards in their hand.
		/// </summary>
		/// <returns>Whether or not the count of cards in the player's hand is > 0.</returns>
		public bool HasMoreCards() {
			return this.Hand.Count > 0;
		}

		/// <summary>
		/// Receives cards from some previous guess.
		/// </summary>
		/// <param name="cards">The cards to add to the player's hand.</param>
		public virtual void ReceiveCards(List<Card> cards) {
			this.Hand.Add(cards);
		}
	}
}