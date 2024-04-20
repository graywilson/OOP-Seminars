using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.Data;

namespace OOP_Seminars.Models
{
    public class DataBase
    {
        private Database db;

        public Database Db
        {
            get
            {
                return db;
            }
            private set
            {
                db = value;
            }
        }

        public DataBase(Database db)
        {
            Db = db;
        }

        public void Create() { }
        public void Read() { }
        public void Update() { }
        public void Delete() { }
    }
}