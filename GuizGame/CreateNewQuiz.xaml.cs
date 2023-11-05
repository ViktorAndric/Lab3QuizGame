using GuizGame.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Path = System.IO.Path;

namespace GuizGame
{
    /// <summary>
    /// Interaction logic for CreateNewQuiz.xaml
    /// </summary>
    public partial class CreateNewQuiz : Window
    {
        List<Question> selectedQuestions = new List<Question>();
        public List<Question> questions { get; set; }
        Quiz newQuiz = new Quiz();
        public CreateNewQuiz()
        {
            InitializeComponent();
            this.DataContext = Quizes.allQuestions;
            Statements.ItemsSource = Quizes.allQuestions;
        }
        
        private void ComboBoxStatements()
        {
            Statements.ItemsSource = Quizes.allQuestions;
        }

        private void ComboBoxSelectedQuestions()
        {
            SelectedStatements.ItemsSource = selectedQuestions;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(Statements.SelectedItem is Question selectedQuestion)
            {
               
                bool questionExists = selectedQuestions.Any(q => q.Statement == selectedQuestion.Statement);
                if(!questionExists)
                {
                    selectedQuestions.Add((Question)Statements.SelectedItem);
                    NumberOfQuestions.Text = $"{selectedQuestions.Count}/10";
                }
            }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxStatements();
            ComboBoxSelectedQuestions();
        }
        private void SaveQUiz_Click(object sender, RoutedEventArgs e)
        {
            string category = txtQuizName.Text;
            if (category == "Quiz Name")
            {
                MessageBox.Show("Choose a name for your Quiz");
            }
            else
            {
                foreach(Question question in selectedQuestions)
                {
                    question.Category = category;
                }
                if (selectedQuestions.Count == 10)
                {
                    Quizes.WriteFiles(selectedQuestions);
                    MessageBox.Show($"{category} has been saved");
                }
                else
                {
                    MessageBox.Show("Chose 10 questions to continue");
                }
            }
        }
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow backToMenu = new MainWindow();
            backToMenu.Show();
            Close();
        }
        private void btnRemoveQuestion_Click(object sender, RoutedEventArgs e)
        {
                selectedQuestions.Remove((Question)SelectedStatements.SelectedItem);
                NumberOfQuestions.Text = $"{selectedQuestions.Count}/10";
        }
    }

}
