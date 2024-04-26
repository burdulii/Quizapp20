using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Models;
using QuizApp.Repositories;
using QuizApp.Services;
using System.Numerics;

namespace QuizApp.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IQuestionsRepository _questionRepository;
        private readonly IPlayersRepository _playersRepository;
        public GameController(IGameService gameService, IQuestionsRepository questionRepository)
        {
            _gameService = gameService;
            _questionRepository = questionRepository;
        }
        [HttpGet]
        public IActionResult StartQuiz()
        {
            var questions = _questionRepository.GetAllQuestions();
            return View("Quiz", questions);
        }
        public IActionResult Result(string playerName, float score)
        {
            ViewBag.PlayerName = playerName;
            ViewBag.Score = score;
            return View();
        }
        [HttpPost]
        public IActionResult SubmitQuiz(string playerName, List<int> selectedAnswerIds)
        {
            // Logic to process quiz submission, calculate score, update player info, etc.
            // For example:
            var updatedPlayer = _gameService.SubmitQuiz(new SubmitModel { PlayerName = playerName, SelectedAnswerIds = selectedAnswerIds });

            // Redirect to the Result action with the player's name and highest score
            return RedirectToAction("Result", new { playerName = updatedPlayer.PlayerName, score = updatedPlayer.HighestScore });
        }

        public ActionResult<Player> GetUpdatedPlayerInfo(string playerName)
        {
            var player = _playersRepository.GetPlayerByName(playerName);
            return player;
        }
    }
}
