using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class AcademyWindow : WindowBase
	{
		[Inject] private readonly IProgressionStorageService _progression;

		[SerializeField] private int _spellPrice;
		[SerializeField] private float _nextSpellPriceMultiplier;
		[SerializeField] private Button _unlockSpellButton;
		[Header("Spell elements")]
		[SerializeField] private TextMeshProUGUI _titleTextMesh;
		[SerializeField] private TextMeshProUGUI _descriptionTextMesh;
		[SerializeField] private Image _icon;
		[Header("Spells")]
		[SerializeField] private List<SpellScriptableObject> _spells;

		private PlayerProgress CurrentPlayer => _progression.LoadProgress();

		private int CurrentSpellPrice => _spellPrice + MultiplierForCurrentSpell;

		private int MultiplierForCurrentSpell => Mathf.RoundToInt(_nextSpellPriceMultiplier * LearnedSpellsCount);

		private int LearnedSpellsCount => CurrentPlayer.LearnedSpellsCount;

		private void OnEnable()  => _unlockSpellButton.onClick.AddListener(UnlockSpell);
		private void OnDisable() => _unlockSpellButton.onClick.RemoveListener(UnlockSpell);

		private void Start() => UpdateShowedSpell();

		private void UnlockSpell()
		{
			if (CurrentPlayer.KalymCount >= CurrentSpellPrice)
			{
				UpdateProgress(with: (p) => p.LearnedSpellsCount++);
			}
			else
			{
				Debug.Log("TODO: not enough kalym");
			}

			UpdateShowedSpell();
		}

		private void UpdateShowedSpell()
		{
			var lastSpell = _spells.Skip(CurrentPlayer.LearnedSpellsCount).FirstOrDefault();

			if (lastSpell is null)
			{
				Debug.Log("TODO: there is all bought");
				return;
			}

			_titleTextMesh.text = lastSpell.Title;
			_descriptionTextMesh.text = lastSpell.Description;
			_icon.sprite = lastSpell.Icon;
		}

		private void UpdateProgress(Action<PlayerProgress> with) => _progression.SaveProgress(CurrentPlayer.With(with));
	}
}