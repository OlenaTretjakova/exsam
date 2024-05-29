using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace exsam
{
    public class QuisServices : IQuisService
    {
        private List<Quiz> Quizes;
        private readonly string quizesFilePath;

        public QuisServices(string quizesFilePath)
        {
            this.quizesFilePath = quizesFilePath;
            Quizes = ReadFromJson() ?? new List<Quiz>();
        }

        public void AddQuiz(Quiz quiz) => Quizes.Add(quiz);

        public List<Quiz> GetQwizes() => Quizes;

        public string PassQuiz(string name)
        {
            var quizToPass = Quizes.FirstOrDefault(item => item.Name == name);
            return quizToPass != null 
                ? IneractivePassQuiz(quizToPass)
                : "Quiz not found"; 
        }


        public string PassRandomedQuiz(params string [] names)
        {
            List<Question> selectedQuestions = new List<Question>();
            int countTopics = 0;
            foreach (string name in names)
            {
                var currentQuiz = Quizes.FirstOrDefault(item => item.Name == name);
                if(currentQuiz != null)
                {
                    selectedQuestions.AddRange(currentQuiz.Questions);
                    countTopics++;
                }
                else
                {
                    Console.WriteLine($"Quiz {name} not found");
                }
            }

            Random random = new Random();

            while (selectedQuestions.Count() > 8)
            {
                int randomNumber = random.Next(0, selectedQuestions.Count);
                selectedQuestions.RemoveAt(randomNumber);

            }

            var quizToPass = new Quiz("QuizToPass", selectedQuestions);
            //AddQuiz(quizToPass);
            WriteToJson(Quizes);

            return quizToPass != null
               ? IneractivePassQuiz(quizToPass)
               : "Quiz not found";
        }

        public List<Quiz> ReadFromJson()
        {
            if(!File.Exists(quizesFilePath))
            {
                return null;
            }

            string json = File.ReadAllText(quizesFilePath);
            return JsonConvert.DeserializeObject<List<Quiz>>(json);
        }

        public void WriteToJson(List<Quiz> quizzes)
        {
            string json = JsonConvert.SerializeObject(quizzes, Formatting.Indented);
            File.WriteAllText(quizesFilePath, json);
        }

        private string IneractivePassQuiz(Quiz quiz)
        {
            string correctAnswers = "";
            string incorrectAnswers = "";
            int correctAnswersCount = 0;
            int count = 0;
            Console.WriteLine("Enter the correct answers through ", " .");

            foreach (var question in quiz.Questions)
            {
                count ++;
                Console.WriteLine(question.Content);
                Console.WriteLine(question.AnswerOptions);
                string answer = Console.ReadLine();

                if (answer == question.RightAnswers)
                {
                    correctAnswersCount++;
                    correctAnswers +=  $"{ count},";
                }
                else
                {
                    incorrectAnswers += $"{ count},";
                }
            }

            correctAnswers = correctAnswers.TrimEnd(',') + ".";

            incorrectAnswers = incorrectAnswers.TrimEnd(',') + ".";

            return $"{correctAnswersCount} / {quiz.Questions.Count()} ." +
                $"You answered question: {correctAnswers} correctly, and question: {incorrectAnswers} incorrectly.";
        }
    }
}
