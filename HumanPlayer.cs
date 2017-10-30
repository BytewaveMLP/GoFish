using System;
using System.Collections.Generic;
using System.Linq;

namespace GoFish {
	public class HumanPlayer : Player {
		public HumanPlayer() {
			this.Hand = new Hand();
		}
		
		public override List<Card> HandleGuess(Card guess) {
			return this.Hand.HandleGuess(guess);
		}

		public override Card GetGuess() {
			Console.Write("Your guess: ");
			string input = Console.ReadLine();
			
			if (!Card.CardNames.Contains(input)) {
				Console.Error.WriteLine("Invalid card name.");
				return this.GetGuess();
			}

			int index = this.Hand.Cards.FindIndex((card) => card.Name == input);

			if (index == -1) {
				Console.Error.WriteLine("You don't have this card!");
				return this.GetGuess();
			}

			return this.Hand.Cards[index];
		}
	}
}