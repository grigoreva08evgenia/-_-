using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace GroupViewProject
{
    class Main
    {
        // Строка подключения к БД
        string strConnection = @"Data Source=403-10\LOCALDB#C24B9DA4;Initial Catalog=groupall;Integrated Security=True";

        /// <summary>
        /// Добавления данных о группе 
        /// </summary>
        /// <param name="group"> Класс Group</param>
        public void AddGroup(Group group)
        {
            // 
            string SqlCmd = $"INSERT INTO [dbo].[group] ([NameGroup],[NumberGroup],[CuratorGroup]) " +
                $"VALUES ('{group.NameGroup}','{group.NumberGroup}','{group.CuratorGroup}')";

            try
            {
                using (SqlConnection connection = new SqlConnection(strConnection))
                {

                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SqlCmd, connection);

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }



        /// <summary>
        /// Чтения данных из таблицы group
        /// </summary>
        /// <returns></returns>
        public List<Group> ReadGroup()
        {
            string SqlCmd = "SELECT * FROM [dbo].[group]";
            List<Group> groups = new List<Group>();

            try
            {
                using (SqlConnection connection = new SqlConnection(strConnection))
                {

                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SqlCmd, connection);

                    // Получаем строки из таблицы
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();


                    if (sqlDataReader.HasRows)
                    {
                        // Построчно считываем данные
                        while (sqlDataReader.Read())
                        {
                            groups.Add(new Group()
                            {
                                NameGroup = sqlDataReader.GetString(1),
                                NumberGroup = sqlDataReader.GetString(2),
                                CuratorGroup = sqlDataReader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            return groups;
        }

    }


        /// <summary>
        ///  Описание таблицы из БД
        /// </summary>
        class Group
        {
            public int idGroup { get; set; }

            public string NameGroup { get; set; }

            public string NumberGroup { get; set; }

            public string CuratorGroup { get; set; }
        }

}
