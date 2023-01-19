using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	// https://gist.github.com/der-hugo/4f676ede0d4a43bc187690ec3269cd9b
	public abstract class ObservedList { }

	[Serializable]
	public class ObservedList<T> : ObservedList, IList<T>
	{
		[SerializeField] private List<T> _list = new();

		public delegate void ChangedDelegate(int index, T oldValue, T newValue);

		public event ChangedDelegate Changed;

		public event Action Updated;

		public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public void Add(T item)
		{
			_list.Add(item);
			Updated?.Invoke();
		}

		public void Clear()
		{
			_list.Clear();
			Updated?.Invoke();
		}

		public bool Contains(T item) => _list.Contains(item);

		public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

		public bool Remove(T item)
		{
			var output = _list.Remove(item);
			Updated?.Invoke();

			return output;
		}

		public int Count => _list.Count;

		public bool IsReadOnly => false;

		public int IndexOf(T item) => _list.IndexOf(item);

		public void InsertRange(int index, IEnumerable<T> collection)
		{
			_list.InsertRange(index, collection);
			Updated?.Invoke();
		}

		public void AddRange(IEnumerable<T> collection)
		{
			_list.AddRange(collection);
			Updated?.Invoke();
		}

		public void RemoveAll(Predicate<T> predicate)
		{
			_list.RemoveAll(predicate);
			Updated?.Invoke();
		}

		public void RemoveRange(int index, int count)
		{
			_list.RemoveRange(index, count);
			Updated?.Invoke();
		}

		public void Insert(int index, T item)
		{
			_list.Insert(index, item);
			Updated?.Invoke();
		}

		public void RemoveAt(int index)
		{
			_list.RemoveAt(index);
			Updated?.Invoke();
		}

		public T this[int index]
		{
			get => _list[index];
			set
			{
				var oldValue = _list[index];
				_list[index] = value;
				Changed?.Invoke(index, oldValue, value);

				Updated?.Invoke();
			}
		}
	}
}