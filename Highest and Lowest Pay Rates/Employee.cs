using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Highest_and_Lowest_Pay_Rates
{
    class Employee
    {
        private int _id;
        private string _name;
        private string _positiion;
        private decimal _payRate;

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }


        public string Position
        {
            get
            {
                return _positiion;
            }
            set
            {
                _positiion = value;
            }
        }

        public decimal PayRate
        {
            get
            {
                return _payRate;
            }
            set
            {
                _payRate = value;
            }
        }





    }
}
