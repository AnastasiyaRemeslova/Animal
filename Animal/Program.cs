using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Animal
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());           // Enviroment.Process();

            Random rand = new Random();
            Regex pat = new Regex("^(([0]{1})|([0][,][0-9]+)|([1-9]{1}[0-9]*)|([1-9]{1}[0-9]*[,]{1}[0-9]+))+$");
            int n, m, numberOfBirds;
            try
            {
                

                Console.ReadLine();
            }
            catch (AnimalException ex)
            {
                Console.WriteLine("Ошибка: "+ ex.Message);
                Console.ReadLine();
            }
        }

       


        

    }
}
