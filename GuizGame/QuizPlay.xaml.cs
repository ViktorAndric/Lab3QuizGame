using GuizGame.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
            this.DataContext = Quizes.allQuestions;
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
                var question = newQuiz.choosenQuestions[currentQuestion];

                txtQuestion.Text = question.Statement;
                Chooice1.Content = question.Answers[0];
                Chooice2.Content = question.Answers[1];
                Chooice3.Content = question.Answers[2];
                Chooice4.Content = question.Answers[3];
                string imagePath = question.ImagePath;

                if (!string.IsNullOrEmpty(imagePath))
                {
                    string fullImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
                    fullImagePath = fullImagePath.Replace('/', '\\');

                    var uri = new Uri(fullImagePath, UriKind.Absolute);
                    if (File.Exists(fullImagePath)) 
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.UriSource = uri;
                        bitmap.EndInit();
                        QuizImage.Source = bitmap;
                    }
                    else
                    {
                        QuizImage.Source = null;
                    }
                }
            }
            else
            {
                QuizImage.Source = null;
            }
        }
        public void CategoryNextQuestion(List<string> chooice)
        {
            newQuiz.ChoosenCategories(chooice);
            if (currentQuestion < newQuiz.choosenQuestions.Count)
            {
                var question = newQuiz.choosenQuestions[currentQuestion];

                txtQuestion.Text = question.Statement;
                Chooice1.Content = question.Answers[0];
                Chooice2.Content = question.Answers[1];
                Chooice3.Content = question.Answers[2];
                Chooice4.Content = question.Answers[3];

                string imagePath = question.ImagePath;
                if (!string.IsNullOrEmpty(imagePath))
                {
                    string fullImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);

                    fullImagePath = fullImagePath.Replace('/', '\\');

                    var uri = new Uri(fullImagePath, UriKind.Absolute);
                    if (File.Exists(fullImagePath))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.UriSource = uri;
                        bitmap.EndInit();
                        QuizImage.Source = bitmap;
                    }
                    else
                    {
                        QuizImage.Source = null;
                    }
                }
            }
            else
            {
                QuizImage.Source = null;
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

