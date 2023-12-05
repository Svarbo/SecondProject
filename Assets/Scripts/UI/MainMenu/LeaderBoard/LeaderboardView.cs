using Agava.YandexGames;
using ConstantValues;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.MainMenu.Leaderboard
{
	public class LeaderboardView : MonoBehaviour
	{
		[SerializeField] private List<LeaderPlace> _easyLeaderPlaces = new List<LeaderPlace>();
		[SerializeField] private List<LeaderPlace> _mediumLeaderPlaces = new List<LeaderPlace>();
		[SerializeField] private List<LeaderPlace> _hardLeaderPlaces = new List<LeaderPlace>();
		[SerializeField] private TMP_Text _easyPlayerTopPlaceText;
		[SerializeField] private TMP_Text _mediumPlayerTopPlaceText;
		[SerializeField] private TMP_Text _hardPlayerTopPlaceText;
		[SerializeField] private TMP_Text _easyPlayerAttemptionsCountText;
		[SerializeField] private TMP_Text _mediumPlayerAttemptionsCountText;
		[SerializeField] private TMP_Text _hardPlayerAttemptionsCountText;

		private void OnEnable()
		{
			ShowFirstLeaders(LeaderboardsNames.EasyLeaderboardName, _easyLeaderPlaces);
			ShowFirstLeaders(LeaderboardsNames.MediumLeaderboardName, _mediumLeaderPlaces);
			ShowFirstLeaders(LeaderboardsNames.HardLeaderboardName, _hardLeaderPlaces);

			ShowPlayerPlace(LeaderboardsNames.EasyLeaderboardName, _easyPlayerTopPlaceText, _easyPlayerAttemptionsCountText);
			ShowPlayerPlace(LeaderboardsNames.MediumLeaderboardName, _mediumPlayerTopPlaceText, _mediumPlayerAttemptionsCountText);
			ShowPlayerPlace(LeaderboardsNames.HardLeaderboardName, _hardPlayerTopPlaceText, _hardPlayerAttemptionsCountText);
		}

		private void ShowPlayerPlace(string leaderboardName, TMP_Text playerTopPlaceText, TMP_Text playerAttemptionsCountText)
		{
            Agava.YandexGames.Leaderboard.GetPlayerEntry(leaderboardName,
				(result) =>
				{
					if (result == null)
					{
						playerTopPlaceText.text = "-";
						playerAttemptionsCountText.text = "-";
					}
					else
					{
						playerTopPlaceText.text = result.rank.ToString();
						playerAttemptionsCountText.text = result.score.ToString();
					}
				});
		}

		private void ShowFirstLeaders(string leaderboardName, List<LeaderPlace> leaderPlaces)
		{
            Agava.YandexGames.Leaderboard.GetEntries(leaderboardName,
				(result) =>
				{
					string leaderName;
					int leaderScore;
					int leadersCount = leaderPlaces.Count;

					for (int i = 0; i < leadersCount; i++)
					{
                        LeaderboardEntryResponse entry = result.entries[i];

						if (entry != null)
						{
							leaderName = GetLeaderName(entry);
							leaderScore = GetLeaderScore(entry);

							leaderPlaces[i].SetLeaderData(leaderName, leaderScore);
							leaderPlaces[i].gameObject.SetActive(true);
						}
					}
				});
		}

		private int GetLeaderScore(LeaderboardEntryResponse entry) =>
			entry.score;

		private string GetLeaderName(LeaderboardEntryResponse entry)
		{
			string leaderName;

			leaderName = entry.player.publicName;

			if (string.IsNullOrEmpty(leaderName))
				leaderName = SetAnonymousName();

			return leaderName;
		}

		private string SetAnonymousName()
		{
			string leaderName = " ";
			int playerLanguageIndex = PlayerPrefs.GetInt("LanguageIndex", 0);

			if (playerLanguageIndex == 0)
				leaderName = "Аноним";
			if (playerLanguageIndex == 1)
				leaderName = "Anonymous";
			if (playerLanguageIndex == 2)
				leaderName = "Anonim";

			return leaderName;
		}
	}
}