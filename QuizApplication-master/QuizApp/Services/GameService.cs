﻿using QuizApp.Models;
using QuizApp.Repositories;

namespace QuizApp.Services
{
    public class GameService : IGameService
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IQuestionsRepository _questionRepository;
        public GameService(IPlayersRepository playersRepository, IQuestionsRepository questionRepository)
        {
            _playersRepository = playersRepository;
            _questionRepository = questionRepository;
        }

        public Player SubmitQuiz(SubmitModel submitModel)
        {
            var player = _playersRepository.GetPlayerByName(submitModel.PlayerName);
            var round = new Round();
            var selectedanswer = _questionRepository.GetAnswersByIds(submitModel.SelectedAnswerIds);
            var correctanswer = selectedanswer.Where(z => z.IsCorrect).Count();
            var score = (correctanswer * 100) / selectedanswer.Count;
            round.IsWon = score >= 70;
            var attempts = selectedanswer.Select(x =>
                new Attempt
                {
                    AnswerId = x.Id,
                    IsCorrect = x.IsCorrect
                });

            round.Attempts.AddRange(attempts);
            player.Rounds.Add(round);

            if (round.IsWon) player.Wons++;
            player.HighestScore = player.HighestScore > score ? player.HighestScore : score;

            player = _playersRepository.UpdatePlayer(player);
            return player;

        }
    }
}