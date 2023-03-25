using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public abstract class SelectableList<T> : LevelEditorPage
		where T : EntryBase
	{
		[SerializeField] private Transform _regionsListRoot;

		private readonly List<T> _entries = new();
		[CanBeNull] private T _selectedEntry;

		protected IEnumerable<T> Entries => _entries;

		public T SelectedEntry
		{
			get => _selectedEntry;
			private set
			{
				if (_selectedEntry == true)
				{
					_selectedEntry!.Deselect();
				}

				_selectedEntry = value;
				_selectedEntry!.Select();
			}
		}

		private void OnDestroy() => _entries.ForEach((r) => r.Clicked -= OnEntryClicked);

		protected T AddEntry(T entry)
		{
			entry.transform.SetParent(_regionsListRoot);
			entry.Clicked += OnEntryClicked;
			_entries.Add(entry);
			return entry;
		}

		protected void RemoveSelected() => Remove(SelectedEntry);

		private void Remove(T entry)
		{
			if (entry == true)
			{
				_entries.Remove(entry);
				Destroy(entry!.gameObject);
				Deselect();
			}
		}

		private void Deselect() => _selectedEntry = null;

		private void OnEntryClicked(EntryBase clicked) => SelectedEntry = (T)clicked;
	}
}