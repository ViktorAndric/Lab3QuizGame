using ControlzEx.Standard;
using ControlzEx.Theming;
using GuizGame.Model;
using MahApps.Metro.Controls;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;

namespace GuizGame
{
    /// <summary>
    /// Interaction logic for StartUpQuiz.xaml
    /// </summary>
    public partial class StartUpQuiz : Window
    {
        Quiz newQuiz = new Quiz();
        QuizPlay quizPlay;
        
        List<string> selectedCategory = new List<string>();
        public StartUpQuiz()
        {
            InitializeComponent();
            this.DataContext = Quizes.allQuestions;
            listBoxCategory.ItemsSource = Quizes.Categories;
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            string choosenGame = selectedCategory.Count > 0 ? "categories" : "random";
            QuizPlay quizPlay = new QuizPlay(choosenGame, selectedCategory);
            quizPlay.Show();
            Close();

        }

        private void listBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Type type = sender .GetType();
        }

        private void btnGenerateQuiz_Click(object sender, RoutedEventArgs e)
        {
            string choosenGame = selectedCategory.Count > 0 ? "categories" : "random";
            QuizPlay Quizplay = new QuizPlay(choosenGame, selectedCategory);
            Quizplay.Show();
            Close();
        }

        private T FindFirstChildOfType<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child is T)
                    {
                        return (T)child;
                    }
                    else
                    {
                        var childOfChild = FindFirstChildOfType<T>(child);
                        if (childOfChild != null)
                        {
                            return childOfChild;
                        }
                    }
                }
            }
            return null;
        }
       
        public List<Question> ChoosenCategories(List<string> categories)
        {
            List<Question> choosenCategories = new List<Question>();

            foreach (string categoryName in categories)
            {
                var questionsInCategory = Quizes.allQuestions
                                                .Where(q => q.Category.Equals(categoryName, StringComparison.OrdinalIgnoreCase))
                                                .ToList();

                var shuffledQuestions = questionsInCategory.OrderBy(q => Guid.NewGuid()).ToList();

                choosenCategories.AddRange(shuffledQuestions.Take(10));
            }
            return choosenCategories.Take(10).ToList();
        }

        private void ChoosenCategorie_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory.Clear();

            foreach (var item in listBoxCategory.Items)
            {
                var listBoxItem = listBoxCategory.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;

                if (listBoxItem != null)
                {
                    CheckBox checkBox = FindFirstChildOfType<CheckBox>(listBoxItem);

                    if (checkBox != null)
                    {
                        string category = checkBox.Content.ToString();
                        if (checkBox.IsChecked == true)
                        {
                            selectedCategory.Add(category);
                        }
                    }
                }
            }
        }
    }
}
