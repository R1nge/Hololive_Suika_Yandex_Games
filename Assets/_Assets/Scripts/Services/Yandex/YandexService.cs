using System;
using Cysharp.Threading.Tasks;
using YG;
using YG.Utils.LB;

namespace _Assets.Scripts.Services.Yandex
{
    public class YandexService
    {
        public event Action OnFullScreenAdShown;
        public event Action OnFullScreenAdClosed;
        public event Action<int> OnRewardVideo;
        public event Action<LBData>  OnGetLeaderboard;

        public async UniTask Init()
        {
            YandexGame.GameReadyAPI();
            YandexGame.GetLeaderboard("leaderboard", 10, 9, 9, "100");
            YandexGame.OpenFullAdEvent += OnFullScreenAdShown;
            YandexGame.CloseFullAdEvent += OnFullScreenAdClosed;
            YandexGame.RewardVideoEvent += OnRewardVideo;
            YandexGame.onGetLeaderboard += OnGetLeaderboard;
            await UniTask.WaitUntil(() => YandexGame.SDKEnabled);
        }

        public void ShowStickyAd()
        {
            YandexGame.StickyAdActivity(true);
        }

        public void HideStickyAd()
        {
            YandexGame.StickyAdActivity(false);
        }

        public void ShowVideoAd()
        {
            YandexGame.FullscreenShow();
        }

        public void ShowReviewPrompt(bool authDialog)
        {
            YandexGame.ReviewShow(authDialog);
        }

        public void ShowReward(int id)
        {
            YandexGame.RewVideoShow(id);
        }

        public void UpdateLeaderBoardScore(int score)
        {
            YandexGame.NewLeaderboardScores("leaderboard", score);
        }

        public void GetLeaderBoard()
        {
            YandexGame.GetLeaderboard("leaderboard", 10, 3, 3, "100");
        }
    }
}