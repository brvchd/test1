using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Test.DTOs.Request;
using Test.DTOs.Response;
using Test.Model;

namespace Test.Services
{
    public class TeamMemberService : IDbTask
    {
        private const string connectionString = "Data Source=db-mssql;Initial Catalog=s18963;Integrated Security=True";
        public TeamMemberResponce getTeamMember(int id)
        {
            var teamMember = new TeamMemberResponce();
            var tasks = new List<TaskModel>();
            using (var con = new SqlConnection(connectionString))
            {
               
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "select firstname, lastname, email from TeamMember where idteammember = @id";
                    com.Parameters.AddWithValue("@id", id);
                    con.Open();

                    var dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        teamMember.FirstName = dr["firstname"].ToString();
                        teamMember.LastName = dr["lastname"].ToString();
                        teamMember.Email = dr["email"].ToString();
                        dr.Close();
                    }
                    else
                    {
                        throw new ArgumentException("Cannot find such id");
                    }

                    com.CommandText = "select Name, Description, DeadLine, IdProject, IdTaskType from TeamMember, Task where task.IdAssignedTo = TeamMember.IdTeamMember and IdTeamMember = @id order by deadline";
                    com.Parameters.AddWithValue("@id", id);
                    dr = com.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        var task = new TaskModel();
                        task.Name = dr["Name"].ToString();
                        task.Description = dr["Description"].ToString();
                        task.DeadLine = dr["Deadline"].ToString();
                        task.ProjectId = (int)dr["IdProject"];
                        task.TypeId = (int)dr["IdTaskType"];
                        tasks.Add(task);
                    }
                    teamMember.Tasks = tasks;
                }
            }
            return teamMember;
        }


        public void deleteProject(ProjectRequest project)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                SqlTransaction transaction = con.BeginTransaction();
                com.Transaction = transaction;

                com.CommandText = "select IdProject from project where name = @name";
                com.Parameters.AddWithValue("projectname", project.Name);
                var dr = com.ExecuteReader();
                var projModel = new ProjectModel();
                if (dr.Read()) 
                {
                    projModel.IdProject = (int)dr["IdProject"];
                    dr.Close();
                }
                else
                {
                    throw new ArgumentException("Incorrect project name");
                }

                com.CommandText = "select * from project, task where project.IdProject= task.IdProject and project.Name = @name";
                com.Parameters.AddWithValue("projectname", project.Name);
                
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    com.CommandText = "delete from task where idproject = @id";
                    com.Parameters.AddWithValue("@id", projModel.IdProject);
                    com.CommandText = "delete from project where name = @name";
                    com.Parameters.AddWithValue("@name", project.Name);
                }
                else
                {
                    dr.Close();
                    com.CommandText = "delete from project where name = @name";
                    com.Parameters.AddWithValue("@name", project.Name);
                }

                com.ExecuteNonQuery();
                transaction.Commit();
            }
        }

    }
}

