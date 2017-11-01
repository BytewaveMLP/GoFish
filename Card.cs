using System;

namespace GoFish {
	public class Card : IComparable<Card>, IEquatable<Card> {
		/// <summary>
		/// The card's "name," or textual representation
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// The card's numerical value, useful for sorting or comparison
		/// </summary>
		public int Value { get; private set; }

		/// <summary>
		/// A list of all possible card names
		/// </summary>
		public static readonly string[] CardNames = {
			"1", "2", "3", "4", "5",
			"6", "7", "8", "9", "10",
			"J", "Q", "K",
		};

		/// <summary>
		/// The number of valid suits of cards
		/// </summary>
		public const int SuitCount = 4;

		/// <summary>
		/// Creates a new Card instace
		/// </summary>
		/// <param name="value">The value of the card, from 1 - 13 (11, 12, 13 are J, Q, K respectively)</param>
		public Card(int value) {
			if (value < 1 || value > 13) {
				throw new ArgumentOutOfRangeException($"{value} is outside of the possible range 1 - 13");
			}

			this.Value = value;
			this.Name = CardNames[value - 1];
		}

		/// <summary>
		/// Compares this Card to another one by value
		/// </summary>
		/// <param name="that">The Card to compare against</param>
		/// <returns>&lt; 0 if this &lt; that, &gt; 0 if this &gt; that, 0 if this == that</returns>
		public int CompareTo(Card that) {
			return this.Value - that.Value;
		}

		/// <summary>
		/// Tests for equivalency between two Cards by value
		/// </summary>
		/// <param name="that">The Card object to compare against</param>
		/// <returns>Whether or not the two Card objects have the same value</returns>
		public bool Equals(Card that) {
			return that != null && that.Value == this.Value;
		}

		public override string ToString() {
			return this.Name;
		}

		public override int GetHashCode() {
			int hashName = this.Name == null ? 0 : this.Name.GetHashCode();
			int hashValue = this.Value == 0 ? 0 : this.Value.GetHashCode();

			return hashName ^ hashValue;
		}
	}
}