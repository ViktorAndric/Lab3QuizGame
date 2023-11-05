using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuizGame.Model
{
    public class Quiz
    {
        private string _title = string.Empty;
        public string Title => _title;

        public static List<Quiz> allQuiz = new List<Quiz>();
        public List<Question> choosenQuestions = new List<Question>();

        public Quiz(string title)
        {
            _title = title;
        }
        public Quiz()
        {
        }

        public List<Question> GetRandomQuestion()
        {
            Random random = new Random();

            while (choosenQuestions.Count < 10)
            {
                int index = random.Next(Quizes.allQuestions.Count);


                if (!choosenQuestions.Contains(Quizes.allQuestions[index]))
                {
                    choosenQuestions.Add(Quizes.allQuestions[index]);
                }

            }
            return choosenQuestions;

        }

        public List<Question> ChoosenCategories(List<string> categories)
        {
            if (Quizes.allQuestions == null || !Quizes.allQuestions.Any())
            {
                return choosenQuestions;
            }

            var matchingQuestions = Quizes.allQuestions
                                          .Where(q => categories.Any(cat => string.Equals(cat, q.Category, StringComparison.OrdinalIgnoreCase)))
                                          .ToList();

            var random = new Random();
            var shuffledQuestions = matchingQuestions.OrderBy(q => random.Next()).ToList();

            choosenQuestions = shuffledQuestions.Take(10).ToList();

            return choosenQuestions;
        }

        public void RemoveQuestion(int index)
        {

        }
        public void DisplayCategories()
        {

        }
        public void SaveQuestions()
        {

        }
    }
}

