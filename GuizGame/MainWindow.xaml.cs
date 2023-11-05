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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuizGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Quizes.ReadFiles();
            Quizes.GetFolder();
        }

        private void btnSetupQuiz_Click(object sender, RoutedEventArgs e)
        {
            StartUpQuiz startUp = new StartUpQuiz();
            startUp.Show();
        }

        private void btnCreateQuiz_Click(object sender, RoutedEventArgs e)
        {
            CreateNewQuiz createNewQuiz = new CreateNewQuiz();
            createNewQuiz.Show();
        }

        private void btnEditQuiz_Click(object sender, RoutedEventArgs e)
        {
            EditQuestion editQuestion = new EditQuestion();
            editQuestion.Show();
        }

        private void btnCloseGame_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
