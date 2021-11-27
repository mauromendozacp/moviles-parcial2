using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

[System.Serializable]
public struct Achievement
{
    public int id;
    public string description;
    public string achievementId;
}

public class PlayGames : MonoBehaviourSingleton<PlayGames>
{
    [SerializeField] private Achievement[] achievements = null;

    public static PlayGamesPlatform platform;
    private double progress = 100f;

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
            InitAchievement();
            Debug.Log(success ? "Logged in successfully" : "Login Failed");
        });
    }

    public void UnlockAchievement(int id)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportProgress(GetAchievement(id).achievementId, progress, success =>
            {
                Debug.Log("Unlock achievement: " + GetAchievement(id).description);
            });
        }
    }

    public void UpdateLeadboard(int stars)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportScore(stars, GPGSIds.leaderboard_leadboard_estrellas, success =>
            {
                Debug.Log("Post score: " + stars);
            });
        }
    }

    private void InitAchievement()
    {
        achievements[0].achievementId = GPGSIds.achievement_jaja_noob;
        achievements[1].achievementId = GPGSIds.achievement_50_stars;
        achievements[2].achievementId = GPGSIds.achievement_250_estrellas;
        achievements[3].achievementId = GPGSIds.achievement_500_estrellas;
        achievements[4].achievementId = GPGSIds.achievement_nasheeee;
    }

    private Achievement GetAchievement(int id)
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            if (achievements[i].id == id)
            {
                return achievements[i];
            }
        }

        return new Achievement();
    }
}
