using System;
using System.Collections.Generic;
using System.Linq;

namespace GoFish {
	public class Game {
		static void Main(string[] args) {
			Random rand = new Random();

			Player human     = new HumanPlayer();
			Player ai        = new AIPlayer(rand);
			Player[] players = {human, ai};

			Deck deck = new Deck(rand);

			bool cont = true;
			bool playersSwapped = true;

			while (cont) {
				deck.Shuffle();

				int curPlayer = rand.Next(0, 1);
				int lastPlayer = curPlayer == 0 ? 1 : 0;

				for (int i = 0; i < 14; i++) {
					players[i % 2].DrawCard(deck);
				}

				while (players.All(player => player.HasMoreCards())) {
					Player pl    = players[curPlayer];
					Player other = players[curPlayer == 0 ? 1 : 0];

					string youOrI      = curPlayer == 0 ? "You" : "I";
					string otherYouOrI = curPlayer == 0 ? "I" : "You";

					if (!playersSwapped) {
						Console.WriteLine($"{youOrI} get another guess!");
					}

					List<Card> previousBooks = pl.Hand.Books.ToList();

					Console.WriteLine($"Your hand: {human.Hand}");

					Card guess = pl.GetGuess();

					if (curPlayer == 1) {
						Console.WriteLine($"My guess: {guess.Name}");
					}

					List<Card> cards = other.HandleGuess(guess);
					if (cards.Count == 0) {
						Console.WriteLine($"{otherYouOrI} say GO FISH!!!");
						Card drawn = pl.DrawCard(deck);
						if (curPlayer == 0) {
							Console.WriteLine($"You drew: {drawn.Name}");
						}

						if (!drawn.Equals(guess)) {
							curPlayer = curPlayer == 0 ? 1 : 0;
							playersSwapped = true;
						} else {
							Console.WriteLine($"{youOrI} drew the guess!");
							playersSwapped = false;
						}
					} else {
						Console.WriteLine($"{otherYouOrI} have {cards.Count} {guess.Name}(s).");
						pl.ReceiveCards(cards);
						playersSwapped = false;
					}

					List<Card> newBooks = pl.Hand.Books.Except(previousBooks).ToList();
					
					if (newBooks.Count > 0) {
						Console.WriteLine($"{youOrI} made a book of {newBooks[0]}s!");
					}
				}

				Console.WriteLine($"Your books: {human.Hand.Books.Count}");
				Console.WriteLine($"My books: {ai.Hand.Books.Count}");

				if (human.Hand.Books.Count > ai.Hand.Books.Count) {
					Console.WriteLine("You win!");
				} else if (ai.Hand.Books.Count > human.Hand.Books.Count) {
					Console.WriteLine("I win!");
				} else {
					Console.WriteLine("Tie game!");
				}

				Console.Write("Would you like to play again? [Y/n]: ");
				string input = Console.ReadLine();
				cont = input != null && input.Equals("y", StringComparison.InvariantCultureIgnoreCase);
			}
		}
	}
}
