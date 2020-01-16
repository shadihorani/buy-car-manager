using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent_a_car_manager.Models
{
    class CarModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        public CarModel() { 

        }

        public CarModel(int id, string name, int price) 
        {
            this.id    = id;
            this.name  = name;
            this.price = price;
        }

        
        public override string ToString()
        {
            return name + " - " +price+ " Euro";
        }

    }
}
