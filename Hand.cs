using System.Collections.Generic;

namespace GoFish {
	public class Hand {
		/// <summary>
		/// The current set of cards in the hand
		/// </summary>
		public List<Card> Cards { get; private set; }
		/// <summary>
		/// The current set of books in the hand
		/// </summary>
		public List<Card> Books { get; private set; }

		public int Count {
			get {
				return this.Cards.Count;
			}
		}

		public Hand() {
			this.Cards = new List<Card>();
			this.Books = new List<Card>();
		}

		/// <summary>
		/// Adds a card to the hand, sorts it, and checks for 4-card
		/// "books."
		/// </summary>
		/// <param name="card">The Card to add to the hand</param>
		public void Add(Card card) {
			this.Cards.Add(card);
			this.Cards.Sort();
			
			Card book = this.CheckForBooks();
			if (book != null) {
				this.Books.Add(book);
			}
		}

		/// <summary>
		/// Adds the cards to the hand, sorts it, and checks for 4-card
		/// "books."
		/// </summary>
		/// <param name="card">The Cards to add to the hand</param>
		/// <returns>The Card of the created book, if any</returns>
		public Card Add(List<Card> cards) {
			this.Cards.AddRange(cards);
			this.Cards.Sort();
			
			Card book = this.CheckForBooks();
			if (book != null) {
				this.Books.Add(book);
			}
			return book;
		}

		/// <summary>
		/// Gets all matching cards from the current hand.
		/// </summary>
		/// <param name="guess">The card to match against</param>
		/// <returns>A list containing all matching cards</returns>
		private List<Card> GetMatches(Card card) {
			return this.Cards.FindAll((match) => {
				return card.Equals(match);
			});
		}

		/// <summary>
		/// Removes all matching cards from the current hand.
		/// </summary>
		/// <param name="card">The card to remove</param>
		private void RemoveAll(Card card) {
			this.Cards.RemoveAll((match) => {
				return card.Equals(match);
			});
		}

		/// <summary>
		/// Tests if we have the given card in the hand.
		/// </summary>
		/// <param name="card">The card to look for.</param>
		/// <returns>Whether or not this hand contains the given card</returns>
		public bool HasCard(Card card) {
			return this.GetMatches(card).Count > 0;
		}

	/// <summary>
	/// Handles a player's guess by retrieving all matching cards and
	/// removing them from the player's hand.
	/// </summary>
	/// <param name="guess">The Card representing the current guess</param>
	/// <returns>A list of Cards containing all matches</returns>
		public List<Card> HandleGuess(Card guess) {
			List<Card> matches = this.GetMatches(guess);
			
			if (matches.Count > 0) this.RemoveAll(guess);

			return matches;
		}

		/// <summary>
		/// Checks for the existence of "books" (sets of 4) of any
		/// given card value. Removes them if they exist, and returns
		/// the card whose name represents the book removed.
		/// </summary>
		/// <returns>The card which represents the book that was removed - null if none.</returns>
		public Card CheckForBooks() {
			for (int i = 1; i <= Card.CardNames.Length; i++) {
				Card bookCard = new Card(i);

				List<Card> matches = this.GetMatches(bookCard);

				if (matches.Count == 4) {
					this.RemoveAll(bookCard);
					return bookCard;
				}
			}

			return null;
		}

		public override string ToString() {
			string str = "";

			this.Cards.ForEach((card) => {
				str += card.ToString() + " ";
			});

			if (this.Books.Count > 0) {
				str += "+ book(s) of ";
				this.Books.ForEach((book) => {
					str += book.ToString() + " ";
				});
			}

			return str;
		}
	}
}