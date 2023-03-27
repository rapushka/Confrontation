using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Confrontation
{
	public abstract class SelectableListPage<T> : LevelEditorPage
		where T : SelectableEntryBase
	{
		[SerializeField] private Transform _listRoot;

		private readonly List<T> _entries = new();
		[CanBeNull] private T _selectedEntry;

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

		protected void AddEntry(T entry)
		{
			entry.transform.SetParent(_listRoot);
			entry.EntrySelected += OnEntrySelected;
			_entries.Add(entry);
		}

		protected void RemoveSelected() => Remove(SelectedEntry);

		protected void ClearList() => _entries.Clear();

		protected void ForEachEntry(Action<T> @do)
		{
			foreach (var entry in _entries)
			{
				@do.Invoke(entry);
			}
		}

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