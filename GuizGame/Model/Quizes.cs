using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GuizGame.Model
{
    public static class Quizes
    {
        public static List<Question> allQuestions = new List<Question>();
        public static List<string> Categories = new List<string>();
        public static List<string> allStatements = new List<string>();

        public static void AddQuestion(string category, string statement, int correctAnswer, string dataPath, string[] answers)
        {
            int id = allQuestions.Count + 1;
            List<Question> newQuestions = new List<Question>();
            Question newQuestion = new Question(id, category, statement, correctAnswer, dataPath, answers);
            newQuestions.Add(newQuestion);
            allQuestions.Add(newQuestion);
            WriteFiles(newQuestions);
            if (!Categories.Contains(category))
            {
                Categories.Add(category);
            }

            if (!allStatements.Contains(statement))
            {
                allStatements.Add(statement);
            }
        }
        public static (List<Question> allQuestions, List<string> allStatements, List<string> Categories)ReadFiles()
        {
            string localAppData = GetFolder();
            string textfilePath = Path.Combine(localAppData, "Questions.json");

            if (File.Exists(textfilePath))
            {
                try
                {
                    string jsonData = File.ReadAllText(textfilePath);
                    List<Question> existingQuestions = JsonConvert.DeserializeObject<List<Question>>(jsonData);

                    foreach (var question in existingQuestions)
                    {
                        if (!allQuestions.Any(q => q.Id == question.Id))
                        {
                            allQuestions.Add(question);
                        }
                        if (!Categories.Contains(question.Category))
                        {
                            Categories.Add(question.Category);
                        }
                        if (!allStatements.Contains(question.Statement))
                        {
                            allStatements.Add(question.Statement);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            return (allQuestions, allStatements, Categories);
        }
        public static async Task WriteFiles(List<Question> questions)
        {

            string localAppData = GetFolder();
            string textfilePath = Path.Combine(localAppData, "Questions.json");
            List<Question> existingQuestions = new List<Question>();

            if(File.Exists(textfilePath))
            {
                string jsonData = await File.ReadAllTextAsync(textfilePath);
                existingQuestions = JsonConvert.DeserializeObject<List<Question>>(jsonData);
            }

            foreach(Question question in questions)
            {
                existingQuestions.Add(question);

            }

            var jsonFile = JsonConvert.SerializeObject(existingQuestions, Formatting.Indented);
            await File.WriteAllTextAsync(textfilePath, jsonFile);
        }
        public static string GetFolder()
        {
            string folderName = "AAQuestion";
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string jsonFolder = Path.Combine(appDataFolder, folderName);

            if (!Directory.Exists(jsonFolder))
            {
                Directory.CreateDirectory(jsonFolder);
                ExistingQuestion();
            }
            return jsonFolder;
        }
        public static void ExistingQuestion()
        {
            AddQuestion("Sport", "Vem vann Fotbolls-VM 2018?", 0, "ImagesSport.jpg" , new string[] { "Frankrike", "Brasilien", "Tyskland", "Spanien" });

            AddQuestion("Sport", "Vilket land är känt för sin ishockeytradition och kallas 'Hockeylandet'?", 1, "Sport.jpg", new string[] { "Sverige", "Kanada", "USA", "Ryssland" });

            AddQuestion("Sport", "Vilken sport använder en shinty?", 3, "Sport.jpg", new string[] { "Golf", "Fotboll", "Ishockey", "Fält-hockey" });

            AddQuestion("Sport", "Vilket år infördes damtennis som en olympisk gren vid sommarspelen?", 1, "Sport.jpg", new string[] { "1900", "1924", "1988", "2004" });

            AddQuestion("Sport", "Vilken är den första etappen av Tour de France?", 3, "Sport.jpg", new string[] { "Paris", "Lyon", "Marseille", "Nice" });

            AddQuestion("Sport", "Vilket lag vann Super Bowl LIV (54) 2020?", 2, "Sport.jpg", new string[] { "New England Patriots", "San Francisco 49ers", "Kansas City Chiefs", "Green Bay Packers" });

            AddQuestion("Sport", "Vad är en runda i golf?", 3, "Sport.jpg", new string[] { "Ett hål", "Nio hål", "Åtta hål", "18 hål" });

            AddQuestion("Sport", "Vilket land var värd för de olympiska vinterspelen 2018?", 1, "Sport.jpg", new string[] { "Kanada", "Sydkorea", "Ryssland", "Sverige" });

            AddQuestion("Sport", "Vilken idrottsman kallas 'The Greatest' inom boxning?", 2, "Sport.jpg", new string[] { "Floyd Mayweather", "Mike Tyson", "Muhammad Ali", "Manny Pacquiao" });

            AddQuestion("Geografi", "Vilket land är känt som Land of the Rising Sun?", 2, "Geography.jpg", new string[] { "Kina", "Indien", "Japan", "Sydkorea" });

            AddQuestion("Geografi", "Vilken flod rinner genom Kairo?", 1, "Geography.jpg", new string[] { "Amazonfloden", "Nilen", "Yangtze", "Mississippifloden" });

            AddQuestion("Geografi", "Vilket hav ligger öst om Afrika?", 1, "Geography.jpg", new string[] { "Atlanten", "Indiska oceanen", "Stilla havet", "Medelhavet" });

            AddQuestion("Geografi", "Vilken är världens största ö?", 0, "Geography.jpg", new string[] { "Grönland", "Madagaskar", "Borneo", "Australien" });

            AddQuestion("Geografi", "Vilket land har flest invånare?", 0, "Geography.jpg", new string[] { "Kina", "Indien", "USA", "Indonesien" });

            AddQuestion("Geografi", "Vad är huvudstaden i Sverige?", 3, "Geography.jpg", new string[] { "Köpenhamn", "Oslo", "Helsingfors", "Stockholm" });

            AddQuestion("Geografi", "Vilken kontinent är känd som 'Den gamla världen'?", 1, "Geography.jpg", new string[] { "Nordamerika", "Asien", "Australien", "Sydamerika" });

            AddQuestion("Geografi", "Vad är världens högsta berg?", 2, "Geography.jpg", new string[] { "Mount Kilimanjaro", "Mount Fuji", "Mount Everest", "Mount St. Helens" });

            AddQuestion("Geografi", "Vilket land har högst antal öar?", 0, "Geography.jpg", new string[] { "Indonesien", "Filippinerna", "Japan", "Maldiverna" });

            AddQuestion("Geografi", "Vad är huvudstaden i Sydafrika?", 1, "Geography.jpg", new string[] { "Nairobi", "Pretoria", "Johannesburg", "Kapstaden" });

            AddQuestion("Musik", "Vem sjöng låten 'Bohemian Rhapsody'?", 0, "Music.jpg", new string[] { "Queen", "The Beatles", "Led Zeppelin", "Pink Floyd" });

            AddQuestion("Musik", "Vilket instrument spelar en trumslagare?", 1, "Music.jpg", new string[] { "Gitarr", "Trummor", "Piano", "Saxofon" });

            AddQuestion("Musik", "Vem är känd som 'The King of Pop'?", 1, "Music.jpg", new string[] { "Prince", "Michael Jackson", "Elvis Presley", "David Bowie" });

            AddQuestion("Musik", "Vilken musikgenre är kännetecknad av elektroniska trummor och synthesizers?", 3, "Music.jpg", new string[] { "Rock", "Jazz", "Country", "Elektronisk dansmusik (EDM)" });

            AddQuestion("Musik", "Vilket band släppte albumet 'The Dark Side of the Moon'?", 2, "Music.jpg", new string[] { "The Rolling Stones", "The Who", "Pink Floyd", "Led Zeppelin" });

            AddQuestion("Musik", "Vem är känd för låten 'Purple Haze'?", 0, "Music.jpg", new string[] { "Jimi Hendrix", "Eric Clapton", "Bob Dylan", "Prince" });

            AddQuestion("Musik", "Vilken artist sjöng låten 'Hello'?", 0, "Music.jpg", new string[] { "Adele", "Ed Sheeran", "Taylor Swift", "Bruno Mars" });

            AddQuestion("Musik", "Vad är Pink Floyds mest kända album?", 3, "Music.jpg", new string[] { "Wish You Were Here", "Animals", "The Wall", "Dark Side of the Moon" });

            AddQuestion("Musik", "Vem skapade albumet 'Thriller'?", 1, "Music.jpg", new string[] { "Prince", "Michael Jackson", "Elvis Presley", "David Bowie" });

            AddQuestion("Musik", "Vad är huvudinstrumentet i en orkester?", 3, "Music.jpg", new string[] { "Gitarr", "Trummor", "Piano", "Stråkinstrument" });

            AddQuestion("Blandat", "Vilken planet är känd som den Röda Planeten?", 2, "MixedQUIZ.jpg", new string[] { "Jorden", "Venus", "Mars", "Jupiter" });

            AddQuestion("Blandat", "Vad är huvudingrediensen i sushi?", 3, "MixedQUIZ.jpg", new string[] { "Kött", "Fisk", "Kyckling", "Ris" });

            AddQuestion("Blandat", "Vilket land är känt som 'Smörgåsriket'?", 0, "MixedQUIZ.jpg", new string[] { "Sverige", "Frankrike", "Italien", "Grekland" });

            AddQuestion("Blandat", "Vilket element har kemiskt symbol Hg?", 3, "MixedQUIZ.jpg", new string[] { "Väte", "Kväve", "Kol", "Kvicksilver" });

            AddQuestion("Blandat", "Vad är världens längsta flod?", 0, "MixedQUIZ.jpg", new string[] { "Nilen", "Amazonfloden", "Mississippifloden", "Yangtze" });

            AddQuestion("Blandat", "Vad är världens mest sålda bilmärke?", 2, "MixedQUIZ.jpg", new string[] { "Ford", "Toyota", "Volkswagen", "Chevrolet" });

            AddQuestion("Blandat", "Vad är huvudingrediensen i guacamole?", 0, "MixedQUIZ.jpg", new string[] { "Avocado", "Tomat", "Lök", "Koriander" });

            AddQuestion("Blandat", "Vad är den mest talade språket i världen?", 1, "MixedQUIZ.jpg", new string[] { "Engelska", "Kinesiska", "Spanska", "Arabiska" });

            AddQuestion("Blandat", "Vilket år uppfanns World Wide Web?", 1, "MixedQUIZ.jpg", new string[] { "1989", "1991", "1995", "2000" });

            AddQuestion("Blandat", "Vad är huvudstaden i Kanada?", 1, "MixedQUIZ.jpg", new string[] { "Toronto", "Ottawa", "Montreal", "Vancouver" });

            
        }

       
    }
}
