using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GearChamp
{
    public class Champ
    {
        protected String workingDir;
        protected FileStream myFile;
        protected StreamWriter myWriter;
        private static int quantidadeTimes;
        private static string modoCampeonato;
        private static string[] times;
        private static string meuTexto;

        public Champ()
        {
            try
            {
                workingDir = Environment.CurrentDirectory;
                myFile = new FileStream(workingDir + "/database.txt", FileMode.Open);
                myWriter = new StreamWriter(myFile);
                meuTexto = "";
                ReadData();
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        private void ReadData()
        {
            try
            {
                Console.WriteLine("Informe a quantidade de times que participarão:");
                quantidadeTimes = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Informe qual será o modo do campeonato:\n1 -> Eliminação Simples\n2 -> Dupla Eliminação\n3 -> Fase de Grupos seguido de Eliminação Simples\n4 -> Fase de Grupos seguido de Dupla Eliminação\n5 -> Fase de Grupos estilo todos contra um");
                modoCampeonato = Console.ReadLine();
                times = new string[quantidadeTimes];
                Console.WriteLine("Informe o nome dos times.");
                for (int i = 0; i < quantidadeTimes; i++)
                {
                    Console.WriteLine("Time {0}:", Convert.ToString(i + 1));
                    times[i] = Console.ReadLine();
                    meuTexto += "Time " + Convert.ToString(i + 1) + ": " + times[i] + "\n";
                }
                WriteFile(meuTexto, false);
                InitChamp(modoCampeonato);
            }
            catch (System.OverflowException e)
            {
                Console.WriteLine("Dados inseridos inválidos!");
                throw e;
            }
        }
        
        private static void InitChamp(string modo)
        {
            try
            {
                if (modo == "1")
                {
                    EliSimples typeChamp = new EliSimples(times);
                }
                else if (modo == "2")
                {
                    Console.WriteLine("Classe não implementada.");
                }
                else if (modo == "3")
                {
                    Console.WriteLine("Classe não implementada.");
                }
                else if (modo == "4")
                {
                    Console.WriteLine("Classe não implementada.");
                }
                else if (modo == "5")
                {
                    Console.WriteLine("Classe não implementada.");
                }
                else
                {
                    Console.WriteLine("Modo de campeonato inválido.");
                    Console.WriteLine("Informe qual será o modo do campeonato:\n1 -> Eliminação Simples\n2 -> Dupla Eliminação\n3 -> Fase de Grupos seguido de Eliminação Simples\n4 -> Fase de Grupos seguido de Dupla Eliminação\n5 -> Fase de Grupos estilo todos contra um");
                    modoCampeonato = Console.ReadLine();
                    InitChamp(modoCampeonato);
                }
            }
            catch (System.OverflowException e)
            {
                Console.WriteLine("Dados inseridos inválidos!");
                throw e;
            }
        }
        protected void WriteFile(string textWrite, bool closeFile)
        {
            try
            {
                myWriter.WriteLine(textWrite);
                if (closeFile == true)
                {
                    myWriter.Close();
                    myFile.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Problema ao realizar a escrita dos times no arquivo database.txt");
                throw e;
            }

        }

    }

    class EliSimples : Champ
    {
        private static string meuTexto;
        private static Random rnd;
        private static double totalTimesDisputa;
        private static string[] times;
        private static double contador;
        public EliSimples(string[] tim)
        {
            try
            {
                times = tim;
                meuTexto = "-------------------------------------------\nModo do Campeonato -> Eliminação Simples!\n\nDisputas:\n";
                rnd = new Random();
                totalTimesDisputa = 0;
                contador = 0;
                Console.WriteLine(times.Length);
                SetTotalTimes();
                SetDisputa();
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        protected void SetTotalTimes()
        {
            while (times.Length > totalTimesDisputa)
            {
                totalTimesDisputa = Math.Pow(2, contador);
                if (totalTimesDisputa == times.Length)
                {
                    break;
                }
                else
                {
                    contador += 1;
                }
            }
            if (totalTimesDisputa != times.Length)
            {
                for (int l = times.Length; l < totalTimesDisputa; l++)
                {
                    times.Append("Bye");
                }
            }
        }
        protected void SetDisputa()
        {
            times.Reverse();
            Console.WriteLine(times);
        }
    }
}
