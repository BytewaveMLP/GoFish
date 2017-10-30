using System.Collections.Generic;

namespace GoFish.Util {
	public class FixedSizeQueue<T> : Queue<T> {
		public int Size { get; private set; }

		public FixedSizeQueue(int size) {
			this.Size = size;
		}

		public new void Enqueue(T obj) {
			base.Enqueue(obj);
			while (base.Count > this.Size) {
				base.TryDequeue(out _);
			}
		}
	}
}