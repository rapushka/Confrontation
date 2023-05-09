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
		[Inject] private readonly IProgressionStorageService _progressionStorage;
		[Inject] private readonly Progression _progression;

		[SerializeField] private int _spellPrice;
		[SerializeField] private float _nextSpellPriceMultiplier;
		[SerializeField] private Button _unlockSpellButton;
		[SerializeField] private GameObject _thereIsAllBoughtPlug;
		[SerializeField] private NotEnoughKalymWindow _notEnoughKalymWindow;
		[Header("Spell elements")]
		[SerializeField] private TextMeshProUGUI _titleTextMesh;
		[SerializeField] private TextMeshProUGUI _descriptionTextMesh;
		[SerializeField] private Image _icon;
		[SerializeField] private IntPrefixPostfixView _priceView;
		[Header("Spells")]
		[SerializeField] private List<SpellScriptableObject> _spells;

		private PlayerProgress CurrentPlayer => _progressionStorage.LoadProgress();

		private int CurrentSpellPrice => _spellPrice + MultiplierForCurrentSpell;

		private int MultiplierForCurrentSpell => Mathf.RoundToInt(_nextSpellPriceMultiplier * LearnedSpellsCount);

		private int LearnedSpellsCount => CurrentPlayer.LearnedSpellsCount;

		private PlayerProgress ProgressedPlayer => CurrentPlayer.With((p) => p.LearnedSpellsCount++);

		private bool IsAllBought
		{
			set
			{
				_thereIsAllBoughtPlug.SetActive(value);
				_titleTextMesh.gameObject.SetActive(value == false);
				_descriptionTextMesh.gameObject.SetActive(value == false);
				_icon.gameObject.SetActive(value == false);
				_priceView.gameObject.SetActive(value == false);
				_unlockSpellButton.interactable = value == false;
			}
		}

		private void OnEnable()  => _unlockSpellButton.onClick.AddListener(UnlockSpell);
		private void OnDisable() => _unlockSpellButton.onClick.RemoveListener(UnlockSpell);

		private void Start() => UpdateShowedSpell();

		private void UnlockSpell()
		{
			if (CurrentPlayer.KalymCount >= CurrentSpellPrice)
			{
				_progression.SpentPlayerKalym(CurrentSpellPrice);
				_progressionStorage.SaveProgress(ProgressedPlayer);
			}
			else
			{
				_notEnoughKalymWindow.Open();
			}

			UpdateShowedSpell();
		}

		private void UpdateShowedSpell()
		{
			var lastNotLearnedSpell = _spells.Skip(CurrentPlayer.LearnedSpellsCount).FirstOrDefault();

			var isAllBought = lastNotLearnedSpell is null;
			IsAllBought = isAllBought;
			if (isAllBought)
			{
				return;
			}

			_titleTextMesh.text = lastNotLearnedSpell.Title;
			_descriptionTextMesh.text = lastNotLearnedSpell.Description;
			_icon.sprite = lastNotLearnedSpell.Icon;
			_priceView.Value = CurrentSpellPrice;
		}
	}
}