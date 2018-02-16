using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Highest_and_Lowest_Pay_Rates
{
    class MainDB : INotifyPropertyChanged
    {

        private const string CONNECTION_STRING = @"Server=LAPTOP-PM6SGM5L\SQLEXPRESS;Database=Personnel;Trusted_Connection=true;";
        // public string _text;
        private List<Employee> _employees;
        private SqlCommand _command;
        private SqlConnection _conn;
        public event PropertyChangedEventHandler PropertyChanged;

        public List<Employee> AllEmployees
        {
            get
            {
                return _employees;
            }
            set
            {
                _employees = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _messageMax;
        public decimal MessageMax
        {
            get
            {
                return _messageMax;
            }
            set
            {
                _messageMax = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _messageMin;
        public decimal MessageMin
        {
            get
            {
                return _messageMin;
            }
            set
            {
                _messageMin = value;
                NotifyPropertyChanged();
            }
        }


        public void Clear()
        {
            MessageMax = decimal.Zero;
            MessageMin = decimal.Zero;
        }
        public void OpenConnection()
        {
            _conn = new SqlConnection(CONNECTION_STRING);
            _conn.Open();
        }
        public void CloseConnection()
        {
            _conn.Close();
        }
        public void CreateTable()
        {
            OpenConnection();
            try
            {              
                using (_command = new SqlCommand(
                    "IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Employee'))"+
                    "BEGIN "+
                    "CREATE TABLE Employee " +
                    "(ID int identity(1,1) PRIMARY KEY, Name text,Position text, PayRate Real) " +
                    "END", _conn))
                {
                    _command.ExecuteNonQuery();
                }             
            }
            catch
            {

                CloseConnection();
            }
            CloseConnection();            
        }
        public void AddEmployee(string name, string position, decimal payRate)
        {
            OpenConnection();
            try
            {
                using (_command = new SqlCommand(
                  "INSERT INTO Employee(Name,Position,PayRate) VALUES(@Name, @Position, @PayRate)", _conn))
                {
                    _command.Parameters.Add(new SqlParameter("Name", name));
                    _command.Parameters.Add(new SqlParameter("Position", position));
                    _command.Parameters.Add(new SqlParameter("PayRate", payRate));
                    _command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                CloseConnection();
            }
            CloseConnection();
        }
        public void Calc()
        {
            //CreateTable();
            //AddEmployee("Milad", "Director", 13577.44m);
            //AddEmployee("Julia", "Manager", 23357.23m);
            //AddEmployee("Lida", "Worker", 3455.66m);
            //AddEmployee("Sergiy", "Boss", 10777.34m);
            //AddEmployee("Misha", "Main Manager", 8999.44m);
            CopyToList();
        }
        public void CopyToList()
        {
            _conn = new SqlConnection(CONNECTION_STRING);
            
                List<Employee> emps = new List<Employee>();
                _conn.Open();
                _command = new SqlCommand()
                {
                    Connection = _conn,
                    CommandText = "SELECT ID, Name, Position, PayRate FROM Employee"
                };
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    emps.Add(new Employee
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Name = dr["Name"].ToString(),
                        Position = dr["Position"].ToString(),
                        PayRate = Decimal.Parse(dr["PayRate"].ToString())
                    });
                }
                dr.Close();
                AllEmployees = emps;           
        }
        public string CopyToList(string query)
        {
            string pay = "";           
            OpenConnection();
            List<Employee> emps = new List<Employee>();
            _command = new SqlCommand
            {
                Connection = _conn,
                CommandText = query
            };
            SqlDataReader dr = _command.ExecuteReader();
            if (dr.Read())
            {
                pay = dr["PayRate"].ToString();
            }
            dr.Close();
            CloseConnection();
            return pay;
        }
        public void GetHighestPay()
        {
            MessageMax = decimal.Parse(CopyToList("SELECT MAX(PayRate) AS PayRate FROM Employee"));
        }             
        public void GetLowestPay()
        {
            MessageMin = decimal.Parse(CopyToList("SELECT MIN(PayRate) AS PayRate FROM Employee"));
        }

        #region Notify
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion Notify
    }
}







