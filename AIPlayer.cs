using System;
using System.Collections.Generic;
using System.Linq;
using GoFish.Util;

namespace GoFish {
	class AIPlayer : Player {
		/// <summary>
		/// The last few guesses from the human player
		/// </summary>
		public List<Card> LastUserGuesses { get; private set; }

		/// <summary>
		/// A private RNG for getting random guesses
		/// </summary>
		Random rand;

		FixedSizeQueue<Card> lastGuesses;

		public AIPlayer(Random rand) {
			this.rand = rand;
			this.Hand = new Hand();
			this.LastUserGuesses = new List<Card>();
			this.lastGuesses = new FixedSizeQueue<Card>(3);
		}

		/// <summary>
		/// Gets the AI player's guess, based on the user's previous guesses and its current hand.
		/// </summary>
		/// <returns>The AI player's guess, stored as a Card</returns>
		public override Card GetGuess() {
			if (this.Hand.Count == 0) return null;

			Card possible;

			List<Card> possibleGuesses = this.LastUserGuesses.Intersect(this.Hand.Cards).ToList();

			if (possibleGuesses.Count == 0) {
				List<Card> distinctHeldCards = this.Hand.Cards.Distinct().ToList();
				possible = distinctHeldCards[(this.rand.Next(0, distinctHeldCards.Count))];
			} else {
				possible = possibleGuesses[this.rand.Next(possibleGuesses.Count)];
			}

			/// Don't want to guess the same thing twice.
			if (this.lastGuesses.Contains(possible)) {
				return this.GetGuess();
			}

			this.lastGuesses.Enqueue(possible);

			return possible;
		}

		public override List<Card> HandleGuess(Card guess) {
			this.LastUserGuesses.Add(guess);
			return this.Hand.HandleGuess(guess);
		}

		public override void ReceiveCards(List<Card> cards) {
			this.Hand.Add(cards);
			this.LastUserGuesses.Remove(cards[0]);
		}
	}
}