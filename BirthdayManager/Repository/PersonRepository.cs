using BirthdayManager.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayManager.Repository
{
    public class PersonRepository
    {
        private string ConnectionString { get; set; }

        public PersonRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Save(Person person)
        {
            using(SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = "INSERT INTO PERSON(NAME, SURNAME, BIRTHDATE, AGE) VALUES (@P1, @P2, @P3, @P4)";

                DateTime today = DateTime.Today.Date;
                DateTime birthday = new DateTime(today.Year, person.Birthdate.Month, person.Birthdate.Day);

                int age = today.Year - person.Birthdate.Year;

                if (birthday < today)
                {
                    birthday = birthday.AddYears(1);
                    age = age + 1;
                }

                person.Age = age;

                connection.Execute(sql, new
                {
                    P1 = person.Firstname,
                    P2 = person.Lastname,
                    P3 = person.Birthdate,
                    P4 = person.Age
                });
            }
        }

        public void Update(Person person)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                UPDATE PERSON
                                SET FIRSTNAME = @P1,
                                LASTNAME = @P2,
                                BIRTHDATE = @P3,
                                AGE = @P4
                                WHERE ID = @P5
                ";

                DateTime today = DateTime.Today.Date;
                DateTime birthday = new DateTime(today.Year, person.Birthdate.Month, person.Birthdate.Day);

                int age = today.Year - person.Birthdate.Year;

                if (birthday < today)
                {
                    birthday = birthday.AddYears(1);
                    age = age + 1;
                }

                person.Age = age;

                connection.Execute(sql, new { 
                    P1 = person.Firstname, 
                    P2 = person.Lastname, 
                    P3 = person.Birthdate, 
                    P4 = person.Age, 
                    P5 = person.Id 
                });
            }
        }

        public void Delete(Person person)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                DELETE FROM PERSON
                                WHERE ID = @P1
                ";

                connection.Execute(sql, new { P1 = person.Id });
            }
        }

        public List<Person> GetAll()
        {
            List<Person> result = new List<Person>();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                SELECT ID, FIRSTNAME, LASTNAME, BIRTHDATE, AGE
                                FROM PERSON
                ";

                result = connection.Query<Person>(sql).ToList();
            }

            return result;
        }

        public Person GetById(int id)
        {
            Person result = null;

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                SELECT ID, FIRSTNAME, LASTNAME, BIRTHDATE, AGE
                                FROM PERSON
                                WHERE ID = @P1
                ";

                result = connection.QueryFirstOrDefault<Person>(sql, new {P1 = id});
            }

            return result;
        }

        public List<Person> Search(string query)
        {
            List<Person> result = new List<Person>();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                string sql = @"
                                SELECT ID, FIRSTNAME, LASTNAME, BIRTHDATE, AGE
                                FROM PERSON
                                WHERE (NAME LIKE '%' + @P1 +'%' OR SURNAME LIKE '%' + @P2 + '%')
                                ORDER BY BIRTHDATE DESC

                ";

                result = connection.Query<Person>(sql).ToList();
            }

            return result;
        }
    }
}
