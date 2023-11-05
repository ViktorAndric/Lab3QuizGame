using GuizGame.Model;
using System;
using System.Collections.Generic;
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

namespace GuizGame
{
    /// <summary>
    /// Interaction logic for EditQuestion.xaml
    /// </summary>
    public partial class EditQuestion : Window
    {
        public int selectedAnswer;
        public EditQuestion()
        {
            InitializeComponent();
            this.DataContext = Quizes.allQuestions;
            ComboBoxCategorie.ItemsSource = Quizes.Categories;   

            //ComboBox.ItemsSource = Quiz. questions;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void CheckAnswer_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox correctAnswer = (CheckBox)sender;
            selectedAnswer = int.Parse(correctAnswer.Tag.ToString());
            
        }
        private void btnSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInput())
            {

                string category = ComboBoxCategorie.SelectedItem.ToString();
                string statement = txtNewQuestion.Text;
                int correctAnswer = selectedAnswer;
                string[] answerOptions = new string[] 
                {
                    Answer1.Text, 
                    Answer2.Text, 
                    Answer3.Text, 
                    Answer4.Text
                };
                string dataPath = "";
                Quizes.AddQuestion(category, statement, correctAnswer, dataPath, answerOptions);
                MessageBox.Show("Question has been saved");
            }
        }

        public bool ValidateInput()
        { 
            if(ComboBoxCategorie.SelectedItem == null)
            {
                MessageBox.Show("Please choose a category");
                return false;
            }

            string statement = txtNewQuestion.Text.Trim();
            if (statement == "Question")
            {
                MessageBox.Show("Please fill in the question box");
                return false;
            }
            
            if(CheckAnswer1.IsChecked == false && CheckAnswer2.IsChecked == false && CheckAnswer3.IsChecked == false && CheckAnswer4.IsChecked == false)
            {
                MessageBox.Show("Please chech in the box for the correct answer");
                return false;
            }

            string[] answerOptions = new string[]
            {
                Answer1.Text.Trim(),
                Answer2.Text.Trim(),
                Answer3.Text.Trim(),
                Answer4.Text.Trim()
            };

            foreach (string option in answerOptions)
            {
                if (string.IsNullOrEmpty(option))
                {
                    MessageBox.Show("Please fill in all 4 choices.");
                    return false;
                }
            }
            return true;
        }

        private void btnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainMenu = new MainWindow();
            mainMenu.Show();
            Close();
        }
    }
}
