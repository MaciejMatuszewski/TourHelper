using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TourHelper.Base.Model.Entity;
using TourHelper.Repository;
using System.Linq;
using UnityEngine.UI;
using System;

public class QuestionManager : MonoBehaviour
{
    private const string QuestionPrefix = "PYTANIE:\n";
    private const string Answer1Prefix = "ODPOWIEDŹ A:\n";
    private const string Answer2Prefix = "ODPOWIEDŹ B:\n";
    private const string Answer3Prefix = "ODPOWIEDŹ C:\n";
    private const string Answer4Prefix = "ODPOWIEDŹ D:\n";

    private const string NoTourSelected = "Aby quiz był dostępny\n wybierz najpierw wycieczkę";
    private const string NoQuestions = "Brak quizu\n dla wybranej wycieczki";
    private const string TourMustBeFinished = "Quiz jest dostępny\npo odwiedzeniu wszystkich\npunktów wycieczki";

    private int _userId;
    private int _tourId;
    private int _userTourId;

    private List<TourQuestion> _questions;
    private TourQuestion _currentQuestion;
    private UserTourQuestion _currentQuestionAnswer;
    private int _currentQuestionLocalIndex;

    public GameObject Question;
    public Button Answer1;
    public Button Answer2;
    public Button Answer3;
    public Button Answer4;
    public Text QuestionCount;
    public GameObject QuizPanel;
    public GameObject BlockQuizPanel;

    private void Start()
    {
        _userId = PlayerPrefs.GetInt("UserID");


        Answer1.onClick.AddListener(() => SelectAnswer(Answer1.name));
        Answer2.onClick.AddListener(() => SelectAnswer(Answer2.name));
        Answer3.onClick.AddListener(() => SelectAnswer(Answer3.name));
        Answer4.onClick.AddListener(() => SelectAnswer(Answer4.name));
    }

    private void Update()
    {
        LoadQuiz();
    }

    private void SelectAnswer(string question)
    {
        var userTourQuestionRepository = new UserTourQuestionRepository();
        var tourQuestionRepository = new TourQuestionRepository();
        _currentQuestionAnswer = userTourQuestionRepository.GetByUserTourIdAndTourQuestionId(_userTourId, _currentQuestion.Id);

        if (_currentQuestionAnswer == null)
        {
            var splittedQuestion = question.Split('_');
            var answer = Convert.ToInt16(splittedQuestion[2]);

            _currentQuestionAnswer = userTourQuestionRepository.Insert(new UserTourQuestion
            {
                Answer = answer,
                TourQuestionId = _currentQuestion.Id,
                UserTourId = _userTourId
            });

            var tourQuestion = tourQuestionRepository.Get(_currentQuestionAnswer.TourQuestionId.Value);
            if (answer == tourQuestion.CorrectAnswer)
            {
                var tempScore = PlayerPrefs.GetInt("Score");
                tempScore += 3;
                PlayerPrefs.SetInt("Score", tempScore);
            }
        }
        RefreshQuestionPanel();
    }

    private void MarkQuestion()
    {
        var userTourQuestionRepository = new UserTourQuestionRepository();
        _currentQuestionAnswer = userTourQuestionRepository.GetByUserTourIdAndTourQuestionId(_userTourId, _currentQuestion.Id);
        if (_currentQuestionAnswer != null)
        {
            Button button = QuizPanel.GetComponentsInChildren<Button>(true).FirstOrDefault(t => t.name == "QID_" + _currentQuestion.Id + "_" + _currentQuestionAnswer.Answer);

            if (_currentQuestionAnswer.Answer == _currentQuestion.CorrectAnswer)
            {
                var color = button.colors;
                color.normalColor = Color.green;
                color.highlightedColor = Color.green;
                color.pressedColor = Color.green;
                button.colors = color;
            }
            else
            {
                var color = button.colors;
                color.normalColor = Color.red;
                color.highlightedColor = Color.red;
                color.pressedColor = Color.red;
                button.colors = color;
            }
        }
    }

    public void NextQuestion()
    {
        if (_questions.Any())
        {
            if (_currentQuestionLocalIndex >= _questions.Count() - 1)
                _currentQuestionLocalIndex = 0;
            else
                _currentQuestionLocalIndex++;

            _currentQuestion = _questions[_currentQuestionLocalIndex];
            RefreshQuestionPanel();
        }
    }

    public void PreviousQuestion()
    {
        if (_questions.Any())
        {
            if (_currentQuestionLocalIndex == 0)
                _currentQuestionLocalIndex = _questions.Count() - 1;
            else
                _currentQuestionLocalIndex--;

            _currentQuestion = _questions[_currentQuestionLocalIndex];
            RefreshQuestionPanel();
        }
    }

    private void RefreshQuestionPanel()
    {
        var userTourPointRepository = new UserTourPointRepository();
        var userTourPoins = userTourPointRepository.GetByUserTourId(_userTourId);
        var tourPointRepository = new TourPointRepository();
        var tourPoints = tourPointRepository.GetByTourID(_tourId);

        if (_userTourId == 0)
        {
            QuizPanel.SetActive(false);
            BlockQuizPanel.SetActive(true);
            BlockQuizPanel.GetComponentInChildren<Text>().text = NoTourSelected;
        }
        else if (!_questions.Any())
        {
            QuizPanel.SetActive(false);
            BlockQuizPanel.SetActive(true);
            BlockQuizPanel.GetComponentInChildren<Text>().text = NoQuestions;
        }
        else if (userTourPoins.Count() != tourPoints.Count())
        {
            QuizPanel.SetActive(false);
            BlockQuizPanel.SetActive(true);
            BlockQuizPanel.GetComponentInChildren<Text>().text = TourMustBeFinished;
        }
        else
        {
            QuizPanel.SetActive(true);
            BlockQuizPanel.SetActive(false);

            var questionButtonText = Question.GetComponentInChildren<Text>();
            questionButtonText.text = QuestionPrefix + _currentQuestion.Question;
            Question.name = "QID_" + _currentQuestion.Id;

            var answerButton1Text = Answer1.GetComponentInChildren<Text>();
            answerButton1Text.text = Answer1Prefix + _currentQuestion.Answer1;
            Answer1.name = "QID_" + _currentQuestion.Id + "_1";

            var answerButton2Text = Answer2.GetComponentInChildren<Text>();
            answerButton2Text.text = Answer2Prefix + _currentQuestion.Answer2;
            Answer2.name = "QID_" + _currentQuestion.Id + "_2";

            var answerButton3Text = Answer3.GetComponentInChildren<Text>();
            answerButton3Text.text = Answer3Prefix + _currentQuestion.Answer3;
            Answer3.name = "QID_" + _currentQuestion.Id + "_3";

            var answerButton4Text = Answer4.GetComponentInChildren<Text>();
            answerButton4Text.text = Answer4Prefix + _currentQuestion.Answer4;
            Answer4.name = "QID_" + _currentQuestion.Id + "_4";

            var color = Answer1.colors;
            color.highlightedColor = new Color(1, 1, 1, 1);
            color.pressedColor = new Color(1, 1, 1, 1);
            color.normalColor = new Color(1, 1, 1, 1);
            Answer1.colors = color;
            Answer2.colors = color;
            Answer3.colors = color;
            Answer4.colors = color;

            MarkQuestion();
            SetQuestionCounter();
        }
    }
    private void LoadQuiz()
    {
        if (_userTourId != PlayerPrefs.GetInt("UserTourID"))
        {
            _userTourId = PlayerPrefs.GetInt("UserTourID");
            _tourId = PlayerPrefs.GetInt("TourID");

            var tourQuestionRepository = new TourQuestionRepository();
            _questions = tourQuestionRepository.GetByTourId(_tourId).ToList();
            if (_questions.Any())
            {
                _currentQuestionLocalIndex = 0;
                _currentQuestion = _questions[_currentQuestionLocalIndex];
            }
            RefreshQuestionPanel();
        }
    }

    private void SetQuestionCounter()
    {
        QuestionCount.text = String.Format("{0}/{1}", _currentQuestionLocalIndex + 1, _questions.Count);
    }
}
