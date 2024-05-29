using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exsam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserDictionary us = new UserDictionary();
            //us.UsingDictionary();

            var quizService = new QuisServices("Quizes.json");

            Quiz quis1 = new Quiz("Topic1", new List<Question>
            {
                new Question ("Question 1 Topic 1:", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 2 Topic 1:", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 3 Topic 1:", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 4 Topic 1:", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 5 Topic 1:", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 6 Topic 1:", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 7 Topic 1:", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 8 Topic 1:", "answerOptions 1,answerOptions 2", "1")
            });

            Quiz quis2 = new Quiz("Topic2", new List<Question>
            {
                new Question ("Question 1 Topic2 :", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 2 Topic2 :", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 3 Topic2 :", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 4 Topic2 :", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 5 Topic2 :", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 6 Topic2 :", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 7 Topic2 :", "answerOptions 1,answerOptions 2", "1"),
                new Question ("Question 8 Topic2 :", "answerOptions 1,answerOptions 2", "1")
            });

            quizService.AddQuiz(quis1);
            quizService.AddQuiz(quis2);
            quizService.WriteToJson(quizService.GetQwizes());
            Console.WriteLine(quizService.PassQuiz(quis1.Name));
            Console.WriteLine(quizService.PassRandomedQuiz(quis1.Name, quis2.Name));


        Console.ReadLine();
        }
    }
}
