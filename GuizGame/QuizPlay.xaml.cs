using GuizGame.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace GuizGame
{
    /// <summary>
    /// Interaction logic for QuizPlay.xaml
    /// </summary>
    public partial class QuizPlay : Window
    {
        public Quiz newQuiz = new Quiz();
        List<Question> selectedQuestion = new List<Question>();
        List<string> SelectedCategory = new List<string>();
        public string ChoosenGame;
        int currentQuestion;
        int Score;

        public QuizPlay(string choosenGame, List<string> selectedCategory)
        {
            InitializeComponent();
            ChoosenGame = choosenGame;
            SelectedCategory = selectedCategory;
            Choosengame(selectedCategory);
        }

        public void Choosengame(List<string> selectedCategory)
        {
            if (selectedCategory.Count != 0)
            {
                CategoryNextQuestion(selectedCategory);
            }
            else
            {
                NextQuestion();
            }
        }

        private void checkAnswer(object sender, RoutedEventArgs e)
        {
            if (currentQuestion < newQuiz.choosenQuestions.Count)
            {
                Button selectedButton = (Button)sender;
                int selectedAnwser = newQuiz.choosenQuestions[currentQuestion].CorrectAnswer;

                Button seletetedButton = (Button)sender;
                int selectedAnswers = int.Parse(selectedButton.Tag.ToString());

                if (selectedAnswers == newQuiz.choosenQuestions[currentQuestion].CorrectAnswer)
                {
                    Score++;
                    LiveResult.Text = $"{Score}/ {newQuiz.choosenQuestions.Count}";
                }
                else
                {
                    MessageBox.Show("Wrong answer");
                }
                currentQuestion++;
                double percentage = Percentage(Score, currentQuestion);
                Procent.Text = $"{percentage:F1} %";
                NextQuestion();
            }
            else
            {
                MessageBox.Show($"Your result: {Score} / {newQuiz.choosenQuestions.Count}");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
        public void NextQuestion()
        {
            newQuiz.GetRandomQuestion();
            if (currentQuestion < newQuiz.choosenQuestions.Count)
            {
                txtQuestion.Text = newQuiz.choosenQuestions[currentQuestion].Statement;
                //QuizImage.Source = newQuiz.choosenQuestions[currentQuestion].ImagePath;
                
                Chooice1.Content = newQuiz.choosenQuestions[currentQuestion].Answers[0];
                Chooice2.Content = newQuiz.choosenQuestions[currentQuestion].Answers[1];
                Chooice3.Content = newQuiz.choosenQuestions[currentQuestion].Answers[2];
                Chooice4.Content = newQuiz.choosenQuestions[currentQuestion].Answers[3];
            }
        }
        public void CategoryNextQuestion(List<string> chooice)
        {
            newQuiz.ChoosenCategories(chooice);
            if (currentQuestion < newQuiz.choosenQuestions.Count)
            {
                txtQuestion.Text = newQuiz.choosenQuestions[currentQuestion].Statement;
                //QuizImage.Source= newQuiz.choosenQuestions[currentQuestion].ImagePath;

                Chooice1.Content = newQuiz.choosenQuestions[currentQuestion].Answers[0];
                Chooice2.Content = newQuiz.choosenQuestions[currentQuestion].Answers[1];
                Chooice3.Content = newQuiz.choosenQuestions[currentQuestion].Answers[2];
                Chooice4.Content = newQuiz.choosenQuestions[currentQuestion].Answers[3];
            }
        }

      
        private double Percentage(int correctAnswers, int totalQuestions)
        {
            if(totalQuestions == 0.0)
            {
                return 0.0;
            }

            return (double)correctAnswers / totalQuestions * 100;
        }
    }
}

