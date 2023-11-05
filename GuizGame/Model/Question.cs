using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GuizGame.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Statement { get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswer { get; set; }
        public string ImagePath { get; set; }

        public Question(int id, string category, string statement, int correctAnswers, string imagePath, string[] answers)
        {
            Id = id;
            Category = category;
            Statement = statement;
            CorrectAnswer = correctAnswers;
            ImagePath = imagePath;
            Answers = answers;
        }
    }
}
