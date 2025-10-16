using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiArduinoBluetoothClassicAndroid
{
    public class QuestionReponse
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Reponse { get; set; }

        public QuestionReponse(int id, string question, string reponse)
        {
            Id = id;
            Question = question;
            Reponse = reponse;
        }

        public static QuestionReponse[] QuestionReponses = new QuestionReponse[]
        {
            new QuestionReponse(1, "Monsieur et Madame Neymar ont un fils, comment s'appelle-t-il ?", "Jean"),
            new QuestionReponse(2, "Quel est le comble du facteur ?", "D'être timbré"),
            new QuestionReponse(3, "Monsieur et Madame Olle ont 5 filles, comment s'apellent-t-elles ?", "Jenny, Lydia, Beth, Nicole, Esther"),
            new QuestionReponse(4, "Monsieur et Madamme Touille ont un fils, comment s'appelle-t-il ?", "Sacha"),
            new QuestionReponse(5, "Monsieur et Madamme Courci ont une fille, comment s'appelle-t-elle ?", "Sarah"),
            new QuestionReponse(6, "Monsieur et Madamme Mensoif ont un fils, comment s'appelle-t-il ?", "Gérard"),
            new QuestionReponse(7, "Monsieur et Madamme Coh ont un fils, comment s'appelle-t-il ?", "Harry"),
            new QuestionReponse(8, "Quel est le comble pour un électricien ?", "D'être au courant de rien"),
            new QuestionReponse(9, "Quel est le comble pour un jardinier ?", "De raconter des salades"),
            new QuestionReponse(10, "Quel est le comble pour un boulanger ?", "D'avoir du pain sur la planche"),
            new QuestionReponse(11, "Monsieur et Madame Feeling ont une fille, comment s'appelle-t-elle ?", "Agatha"),
            new QuestionReponse(12, "Monsieur et Madame Loge ont une fille, comment s'appelle-t-elle ?", "Laure"),
            new QuestionReponse(13, "Monsieur et Madame Sérien ont un fils, comment s’appelle-t-il ?", "Jean"),
            new QuestionReponse(14, "Monsieur et Madame Duciel ont cinq filles, comment s'appellent-elles ?", "Betty, Baba, Noëlle, Candice et Sandra"),
            new QuestionReponse(15, "Monsieur et Madame Nana ont un fils, comment s’appelle-t-il ?", "Judas"),
            new QuestionReponse(16, "Monsieur et Madame Brico ont un fils, comment s’appelle-t-il ?", "Judas"),
            new QuestionReponse(17, "Monsieur et Madame Tare ont un fils, comment s’appelle-t-il?", "Guy"),
            new QuestionReponse(18, "Monsieur et Madame Deuf ont un fils, comment s’appelle-t-il ?", "John"),
            new QuestionReponse(19, "Monsieur et Madame Par ont un fils, comment s’appelle-t-il ?", "Léo"),
            new QuestionReponse(20, "Monsieur et Madame Ultou ont une fille, comment s’appelle-t-elle ?", "Jeanne"),
            new QuestionReponse(21, "Monsieur et Madame Zarella ont une fille, comment s’appelle-t-elle ?", "Maude"),
            new QuestionReponse(22, "Monsieur et Madame Scott ont une fille, comment s’appelle-t-elle ?", "Debby"),
            new QuestionReponse(23, "Monsieur et Madame Lairbon ont un fils, comment s'appelle-t-il ?", "Oussama"),
            new QuestionReponse(24, "Monsieur et Madame Cussonnet ont un fils, comment s'appelle-t-il ?", "Simon"),
            new QuestionReponse(25, "Monsieur et Madame Honnête ont une fille, quel est son prénom ?", "Camille"),
            new QuestionReponse(26, "Monsieur et Madame Coptaire ont un fils, comment s'appelle-t-il ?", "Elie"),
            new QuestionReponse(27, "Monsieur et Madame Bistrot ont un fils, comment s’appelle-t-il ?", "Alonso"),
            new QuestionReponse(28, "Monsieur et Madame Dalors ont un fils, comment s’appelle-t-il ?", "Omer"),
            new QuestionReponse(29, "Monsieur et Madame Golé ont une fille, comment s'appelle-t-elle ?", "Hillary"),
            new QuestionReponse(30, "Monsieur et Madame Caman ont un fils, comment s’appelle-t-il ?", "Mehdi"),
            new QuestionReponse(31, "Monsieur et Madame Zion ont une fille, comment s'appelle-t-elle ?", "Eva"),
            new QuestionReponse(32, "Monsieur et madame Zobabander ont un fils. Comment s’appelle-t-il ? ", "Edmond"),
            new QuestionReponse(33, "Monsieur et madame Ma-ia ont quatre enfants. Comment s'appellent t-ils?", "Ma-ia Hii, Ma-ia Huu, Ma-ia Hoo, Ma-ia Haa Haa"),
            new QuestionReponse(34, "", ""),
            new QuestionReponse(35, "", ""),
            new QuestionReponse(36, "", ""),
            new QuestionReponse(37, "", ""),
            new QuestionReponse(38, "", ""),
            new QuestionReponse(39, "", ""),
            new QuestionReponse(40, "", "")
        };
    }
}
