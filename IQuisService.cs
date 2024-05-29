using System.Collections.Generic;

namespace exsam
{
    interface IQuisService
    {
        List<Quiz> ReadFromJson();
        void WriteToJson(List<Quiz> quizzes);
        void AddQuiz(Quiz quiz);
        string PassQuiz(string name);
        string PassRandomedQuiz(params string[] names);
    }
}
