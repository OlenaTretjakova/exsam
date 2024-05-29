using System.Collections.Generic;

namespace exsam
{
    public class Question
    {
        public string Content { get; set; }
        public string AnswerOptions { get; set; }
        public string RightAnswers { get; set; }

        public Question(string content, string answerOptions, string rightAnswers)
        {
            Content = content;
            RightAnswers = rightAnswers;
            AnswerOptions = answerOptions;
        }
    }
}
