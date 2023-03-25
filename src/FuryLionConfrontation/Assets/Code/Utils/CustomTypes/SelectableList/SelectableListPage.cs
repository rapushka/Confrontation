using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public abstract class SelectableListPage<T> : LevelEditorPage
		where T : SelectableEntryBase
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
				if (HasSelectedEntry)
				{
					_selectedEntry!.Deselect();
				}

				_selectedEntry = value;
				_selectedEntry!.Select();
			}
		}

		public bool HasSelectedEntry => SelectedEntry == true;

		private void OnDestroy() => _entries.ForEach((r) => r.EntrySelected -= OnEntrySelected);

		protected T AddEntry(T entry)
		{
			entry.transform.SetParent(_regionsListRoot);
			entry.EntrySelected += OnEntrySelected;
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

		private void OnEntrySelected(SelectableEntryBase clicked) => SelectedEntry = (T)clicked;
	}
}