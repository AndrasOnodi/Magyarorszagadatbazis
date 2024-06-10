using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Magyarorszagadatbazis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EgyedisegElemzese("helyforr.dat");
            EgyedisegElemzese("iranyitoszamok.csv");
            EgyedisegElemzese("koordinatak.csv");
            Console.ReadKey();
        }
        static string Ekezetlenites(string ekezetes)
        {
            string ekezetlen = "";
            char[] atalakitando = { 'á', 'Á', 'é', 'É', 'ó', 'Ó', 'í', 'Í', 'ö', 'Ö', 'ő', 'Ő', 'ü', 'Ü', 'ű', 'Ű', 'ú', 'Ú' };
            char[] atalakitott = { 'a', 'A', 'e', 'E', 'o', 'O', 'i', 'I', 'o', 'O', 'o', 'O', 'u', 'U', 'u', 'U', 'u', 'U' };
            foreach(char c in ekezetes)
            {
                bool van = false;
                int i = 0;
                for(; i<atalakitott.Length; i++) 
                {
                    if(c == atalakitando[i]) 
                    {
                        van = true;
                        break;
                    }
                }
                if (van)
                {
                    ekezetlen += atalakitott[i];
                }
                else 
                {
                    ekezetlen += c;
                }
            }
            return ekezetlen;
        }
        static void EgyedisegElemzese(string file) 
        {
            int pozicio = -1;
            int kezdo = -1;
            char szeparator = '\0'; //lényegében a 0 ASCII kódú karakter Escape irásmodú változata
            switch(file)
            {
                case "helyforr.dat":
                    pozicio = 3;
                    szeparator = ' ';
                    kezdo = 0;
                    break;
                case "iranyitoszamok.csv":
                    pozicio = 1;
                    szeparator = ';';
                    kezdo = 1;
                    break;
                case "koordinatak.csv":
                    pozicio = 0;
                    szeparator = ';';
                    kezdo = 1;
                    break;
                default:
                    return;
            }
            StreamReader olvaso = new StreamReader(file);
            Dictionary<string,int> szotar = new Dictionary<string,int>();
            int db = -1;
            while(!olvaso.EndOfStream) 
            {
                string sor = olvaso.ReadLine();
                db++;
                if(db == kezdo) 
                {
                    string[] reszek = sor.Split(szeparator);
                    try
                    {
                        szotar[Ekezetlenites(reszek[pozicio])]++;
                    }
                    catch
                    {
                        szotar[Ekezetlenites(reszek[pozicio])] = 1;
                    }
                }
            }
            olvaso.Close();
            var ismetlodo = from telepules in szotar where telepules.Value >1 select telepules.Key;
        }

        static void URLEllenorzes(string url)
        {
            WebRequest keres=WebRequest.Create(url);
        }
    }
}
