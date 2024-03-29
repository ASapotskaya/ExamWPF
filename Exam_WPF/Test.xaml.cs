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
using System.IO;

namespace Exam_WPF
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
       
        int question_count;
        int correct_answers;
        int wrong_answers;

        string[] array;

        int correct_answers_number;
        int selected_response;

        StreamReader sr;

        public string Text { get;  set; }

        public Test()
        {
            InitializeComponent();
        }
        void start()
        {
            var Encoding = System.Text.Encoding.GetEncoding(65001);
            try
            {

                 sr = new StreamReader(Directory.GetCurrentDirectory() +
                                               @"\test.txt", Encoding);
                

                question_count = 0;
                correct_answers = 0;
                wrong_answers = 0;

                array = new String[10];
            }
            catch (Exception)
            {
                MessageBox.Show("ошибка 1");
            }
            question();

        }
        void question()
        {
            
                label1.Content = sr.ReadLine();

                radioButton1.Content = sr.ReadLine();
                radioButton2.Content = sr.ReadLine();
                radioButton3.Content = sr.ReadLine();

                correct_answers_number = Convert.ToInt32(sr.ReadLine());

                radioButton1.IsChecked = false;
                radioButton2.IsChecked = false;
                radioButton3.IsChecked = false;

                button1.IsEnabled = false;
                question_count = question_count + 1;

            if (sr.EndOfStream == true)
            {
                button1.Content = "Завершить";
                
            }


        }
        void MyPerekluch(object sender, EventArgs e)
        {
            button1.IsEnabled = true;
            button1.Focus();
           
            RadioButton P = (RadioButton)sender;
            var t = P.Name;

            selected_response = int.Parse(t.Substring(11));
        }
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (selected_response == correct_answers_number) correct_answers =
                                               correct_answers + 1;
            if (selected_response != correct_answers_number)
            {

                wrong_answers = wrong_answers + 1;

                array[wrong_answers] = (string)label1.Content;
            }
            if (button1.Content == "Начать сначала")
            {
                button1.Content = "Следующий вопрос";

                radioButton1.IsEnabled = true;
                radioButton2.IsEnabled = true;
                radioButton3.IsEnabled = true;

                start(); 
                return;
            }
            if (button1.Content == "Завершить")
            {

                sr.Close();

                radioButton1.Visibility = Visibility.Hidden;
                radioButton2.Visibility = Visibility.Hidden;
                radioButton3.Visibility = Visibility.Hidden;
               
                StreamWriter srw = new StreamWriter("answers.txt");
                
                label1.Content = "";
                label2.Content = $"Тестирование завершено.\n Правильных ответов: {correct_answers} из {10}.\n";

                button1.Content = "Начать сначала";
                srw.Write($"Тестирование завершено.\n Правильных ответов: {correct_answers} из {10}.\n");
               
                var Str = "Список ошибок " +
                          ":\n\n";
                for (int i = 1; i <= wrong_answers; i++)
                    Str = Str + array[i] + "\n";

                srw.Write(Str);
                

                srw.Close();
                if (wrong_answers != 0) MessageBox.Show(
                                          Str, "Тестирование завершено");
            }
            if (button1.Content == "Следующий вопрос") question();

        }
    

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            button1.Content = "Следующий вопрос";
            button2.Content = "Выйти";

            radioButton1.Checked += MyPerekluch;
            radioButton2.Checked += MyPerekluch;
            radioButton3.Checked += MyPerekluch;

                start();
        }
    }
}
