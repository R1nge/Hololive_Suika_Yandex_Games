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
        public event Action<LBData> OnGetLeaderboard;

        public async UniTask Init()
        {
            YandexGame.GameReadyAPI();
            //GetLeaderBoard("leaderboard", 4, 4, 4);
            YandexGame.OpenFullAdEvent += OnFullScreenAdShown;
            YandexGame.CloseFullAdEvent += OnFullScreenAdClosed;
            YandexGame.RewardVideoEvent += OnRewardVideo;
            YandexGame.onGetLeaderboard += OnGetLeaderboard;

            YandexGame.onAdNotification += ResetADTimer;
            YandexGame.CloseFullAdEvent += ResetADTimer;
            YandexGame.CloseVideoEvent += ResetADTimer;
            await UniTask.WaitUntil(() => YandexGame.SDKEnabled);
        }

        public void ShowStickyAd()
        {
            YandexGame.StickyAdActivity(true);
        }

        private void ResetADTimer()
        {
            YandexGame.Instance.ResetTimerFullAd();
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

        public void UpdateLeaderBoardScore(string leaderboardName, int score)
        {
            YandexGame.NewLeaderboardScores(leaderboardName, score);
        }

        public void GetLeaderBoard(string leaderboardName, int maxResults, int quantityTop, int quantityAround,
            string photoSize = "100")
        {
            YandexGame.GetLeaderboard(leaderboardName, maxResults, quantityTop, quantityAround, photoSize);
        }
    }
}