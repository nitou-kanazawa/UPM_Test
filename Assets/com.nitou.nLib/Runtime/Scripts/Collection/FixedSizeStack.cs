using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// [REF]
//  _: Stack<T> クラス https://learn.microsoft.com/ja-jp/dotnet/api/system.collections.generic.stack-1?view=net-8.0

namespace Nitou {

	/// <summary>
	/// A stack with a fixed maximum size.
	/// </summary>
    public class FixedSizeStack<T> : IEnumerable<T>, IReadOnlyCollection<T> {

        private readonly int _maxSize;
        private readonly LinkedList<T> _list;

		/// <summary>
		/// Number of items in the stack.
		/// </summary>
		public int Count => _list.Count;

		/// <summary>
		/// Checks if the stack is empty.
		/// </summary>
        public bool IsEmpty => _list.Count == 0;


        /// ----------------------------------------------------------------------------
        // Public Method

		/// <summary>
		/// Constructor. Sets the maximum size.
		/// </summary>
        public FixedSizeStack(int maxSize) {
            if (maxSize <= 0) {
                throw new ArgumentException("maxSize must be greater than zero.", nameof(maxSize));
            }
            _maxSize = maxSize;
            _list = new LinkedList<T>();
        }

		/// <summary>
		/// Checks if the stack contains the specified item.
		/// </summary>
		public bool Contains(T item) {
			return _list.Contains(item);
		}

		/// <summary>
		/// Convert to array.
		/// </summary>
		public T[] ToArray() {
			return _list.ToArray();
		}

		public IEnumerator<T> GetEnumerator() {
			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}


		/// ----------------------------------------------------------------------------
		// Public Method (Add/Remove)

		/// <summary>
		/// Adds item to the top of the stack.
		/// </summary>
		public void Push(T item) {
            if (_list.Count == _maxSize) {
                // Remove the oldest item (bottom of the stack)
                _list.RemoveLast();
            }
            // Add the new item to the top of the stack
            _list.AddFirst(item);
        }

		/// <summary>
		/// Removes and returns the top item.
		/// </summary>
		public T Pop() {
            if (_list.Count == 0) {
                throw new InvalidOperationException("The stack is empty.");
            }
            // Remove the top item
            var value = _list.First.Value;
            _list.RemoveFirst();
            return value;
        }

		/// <summary>
		/// Returns the top item without removing it.
		/// </summary>
		public T Peek() {
            if (_list.Count == 0) {
                throw new InvalidOperationException("The stack is empty.");
            }
            // Return the top item without removing it
            return _list.First.Value;
        }

		/// <summary>
		/// Tries to get the top item without removing it.
		/// </summary>
		public bool TryPeek(out T result) {
			if (_list.Count == 0) {
				result = default;
				return false;
			}
			result = _list.First.Value;
			return true;
		}

		/// <summary>
		/// Tries to remove and return the top item.
		/// </summary>
		public bool TryPop(out T result) {
			if (_list.Count == 0) {
				result = default;
				return false;
			}
			result = _list.First.Value;
			_list.RemoveFirst();
			return true;
		}

		/// <summary>
		/// Removes all items from the stack.
		/// </summary>
		public void Clear() {
			_list.Clear();
		}
    }
}

