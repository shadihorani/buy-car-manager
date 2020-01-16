using rent_a_car_manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rent_a_car_manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Application.Run(new MainForm());

            /*
            CarModel car1 = new CarModel(0,"nessan", 100);
            
            IList<CarModel> list = new List<CarModel>();
            list.Add(car1);
            list.Add(car1);
            list.Add(car1);
            list.Add(car1);
            list.Add(car1);


            Console.WriteLine(list.Count);

            for (int i = 0; i < list.Count; i++)
            {
                CarModel currentCarModel = list[i];

                Console.WriteLine(currentCarModel.id);
                Console.WriteLine(currentCarModel.name);
                Console.WriteLine(currentCarModel.price);
                Console.WriteLine();
            }
            */
           
        }
    }
}
