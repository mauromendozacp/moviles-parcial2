using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGames : MonoBehaviour
{
    public int playerScore;
    string leaderboardID = "";

    public static PlayGamesPlatform platform;

    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Logged in successfully");
            }
            else
            {
                Debug.Log("Login Failed");
            }
        });

        UnlockAchievement(0);
    }

    public void AddScoreToLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportScore(playerScore, leaderboardID, success => { });
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowAchievementsUI();
        }
    }

    public static void UnlockAchievement(int ID)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_jaja_noob, 100f, success => { });
        }
    }
}
