using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IDbCommand = System.Data.IDbCommand;
using IDbConnection = System.Data.IDbConnection;
using Mono.Data.Sqlite;
using System.Data;

public class DatabaseCon : MonoBehaviour
{

    string DATABASE_NAME = "/pomoco.s3db";

    //Our Database Connection
    string conn;

    //The attribute we store our queries in
    string SQLQuery;

    // Start is called before the first frame update
    void Start()
    {
        string filepath = Application.dataPath + DATABASE_NAME;
        Debug.Log($"filepath={filepath}");
        conn = "URI=file:" + filepath;

        //create the Database
        CreateDB();

        CreateGoal("Test", 0);
        CreateTask(2, "test", 200, 0);
        CreateUser("avatar1", "audio", "testuser");

        ReadAllTasks();
    }

    //Creates the Database and all the Tables
    public void CreateDB()
    {

        //creates a new connection to the database
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {

                //Goals Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS goals (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name TEXT NOT NULL, status NUMBER(1));";
                command.ExecuteNonQuery();

                //Task Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS tasks (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name TEXT NOT NULL, time INTEGER, is_completed NUMBER(1), goal_id INTEGER, FOREIGN KEY (goal_id) REFERENCES goals (id));";
                command.ExecuteNonQuery();

                //User Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name TEXT, learningType TEXT, avatar TEXT);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //Creates a new Goal. Goals need a name! 
    public bool CreateGoal(string name, int status)
    {

        //checks if a name was entered
        if (name != "")
        {
            using (var connection = new SqliteConnection(conn))
            {
                connection.Open();

                //access the database using a command
                using (var command = connection.CreateCommand())
                {

                    //creates a new goal with the according name
                    command.CommandText = "INSERT INTO goals (name, status) VALUES ('" + name + "','" + status + "');";
                    command.ExecuteNonQuery();

                    //Debug.Log(name);
                }
                connection.Close();
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    //Creates a new Task. A task is related to a goal and needs the goal_id. Additionally it 
    //needs a name, time in minutes and a status. The status needs to be a number between 1 and 0
    public bool CreateTask(int goalid, string name, int time, int completed)
    {
        //checks if a name was entered, if a valid time was entered and if the task is incomplete
        if (name != "" && time != 0 && completed == 0)
        {
            using (var connection = new SqliteConnection(conn))
            {
                connection.Open();

                //access the database using a command
                using (var command = connection.CreateCommand())
                {

                    //creates a new goal with the according name
                    command.CommandText = "INSERT INTO tasks (name, time, is_completed, goal_id) VALUES ('" + name + "','" + time + "','" + completed + "','" + goalid + "');";
                    //Debug.Log(goalid);
                    command.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    //Creates a new User. This should be done at the start of the application after the 
    //learning types and avatars have been chosen
    //This method also checks if there is more than one user in the database and prevents
    //more than 1 user being added. 
    public bool CreateUser(string avatar, string learningType, string name)
    {
        int onlyOneUser = 0;
        //This code selects the user from the table and checks if its more than 0
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM users;";

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                //for each line read the counter is incremented by 1
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        onlyOneUser++;
                    }
                    //Debug.Log("Counter: " + onlyOneUser);
                }
            }
            connection.Close();
        }
        //if the inputs are correct and no user is in the table 1 user is added. There can not be more than one user
        if (name != "" && avatar != "" && learningType != "" && onlyOneUser == 0)
        {
            using (var connection = new SqliteConnection(conn))
            {
                connection.Open();

                //access the database using a command
                using (var command = connection.CreateCommand())
                {

                    //creates a new user with the according values
                    command.CommandText = "INSERT INTO users (name, learningType, avatar) VALUES ('" + name + "','" + learningType + "','" + avatar + "');";
                    //Debug.Log(name);
                    command.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    //Reads all the tasks! returns an array list with task objects in them Tasks objects contain
    //the data of the task but have no functionality.
    public ArrayList ReadAllTasks()
    {
        ArrayList arrlist = new ArrayList();

        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM tasks;";

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Task currentTask = new Task(reader.GetInt32(4), (int)reader.GetInt32(3), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(0));
                        arrlist.Add(currentTask);
                    }
                }
            }
            connection.Close();
        }

        //Debug code that checks if the right tasks were selected

        for (int i = 0; i < arrlist.Count; i++)
        {
            Debug.Log("Im Here2!");
            Task currTask = (Task)arrlist[i];
            Debug.Log("name: " + currTask.taskname + " id: " + currTask.id);
        }
        return arrlist;
    }

    //Reads all the tasks that belong to a certain goal! returns an array list with task objects in them Tasks objects contain
    //the data of the task but have no functionality.
    public ArrayList ReadTasksForGoalX(int goalX)
    {
        ArrayList arrlist = new ArrayList();

        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM tasks WHERE goal_id = " + goalX;

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Task currentTask = new Task(reader.GetInt32(4), (int)reader.GetInt32(3), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(0));
                        arrlist.Add(currentTask);
                    }
                }
            }
            connection.Close();
        }
        //Debug code that checks if the right tasks were selected
        /**
        for (int i = 0; i < arrlist.Count; i++)
        {
            Debug.Log("Im Here2!");
            Task currTask = (Task)arrlist[i];
            Debug.Log("name: " + currTask.taskname + " id: " + currTask.id);
        }
        */
        return arrlist;
    }

    //Reads all the tasks that are completed! returns an array list with task objects in them Tasks objects contain
    //the data of the task but have no functionality.
    public ArrayList ReadCompletedTasks()
    {
        ArrayList arrlist = new ArrayList();

        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM tasks WHERE is_completed = 0";

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Task currentTask = new Task(reader.GetInt32(4), (int)reader.GetInt32(3), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(0));
                        arrlist.Add(currentTask);
                    }
                }
            }
            connection.Close();
        }

        //Debug code that checks if the right tasks were selected
        /**
        for (int i = 0; i < arrlist.Count; i++)
        {
            Debug.Log("Im Here2!");
            Task currTask = (Task)arrlist[i];
            Debug.Log("name: " + currTask.taskname + " id: " + currTask.id);
        }
        */
        return arrlist;
    }

    //Reads a specific task idetified by its ID! returns an array list with task objects in them Tasks objects contain
    //the data of the task but have no functionality.
    public ArrayList ReadSpecificTask(int taskID)
    {
        ArrayList arrlist = new ArrayList();

        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM tasks WHERE id = " + taskID + ";";

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Task currentTask = new Task(reader.GetInt32(4), (int)reader.GetInt32(3), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(0));
                        arrlist.Add(currentTask);
                    }
                }
            }
            connection.Close();
        }

        //Debug code that checks if the right tasks were selected
        /**
        for (int i = 0; i < arrlist.Count; i++)
        {
            Debug.Log("Im Here2!");
            Task currTask = (Task)arrlist[i];
            Debug.Log("name: " + currTask.taskname + " id: " + currTask.id);
        }
        */
        return arrlist;
    }

    //Reads a specific goal idetified by its ID! returns an array list with goal objects in them goal objects contain
    //the data of the goal but have no functionality.
    public ArrayList ReadSpecificGoal(int goalID)
    {
        ArrayList arrlist = new ArrayList();

        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM goals WHERE id = " + goalID + ";";

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Goal currentGoal = new Goal(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1));
                        arrlist.Add(currentGoal);
                    }
                }
            }
            connection.Close();
        }

        //Debug code that checks if the right tasks were selected
        /**
        for (int i = 0; i < arrlist.Count; i++)
        {
            Debug.Log("Im Here2!");
            Task currTask = (Task)arrlist[i];
            Debug.Log("name: " + currTask.taskname + " id: " + currTask.id);
        }
        */
        return arrlist;
    }

    //Reads all goals! returns an array list with goal objects in them goal objects contain
    //the data of the goal but have no functionality.
    public ArrayList ReadAllGoals()
    {
        ArrayList arrlist = new ArrayList();

        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM goals;";

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Goal currentGoal = new Goal(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1));
                        arrlist.Add(currentGoal);
                    }
                }
            }
            connection.Close();
        }

        //Debug code that checks if the right tasks were selected
        /**
        for (int i = 0; i < arrlist.Count; i++)
        {
            Debug.Log("Im Here2!");
            Task currTask = (Task)arrlist[i];
            Debug.Log("name: " + currTask.taskname + " id: " + currTask.id);
        }
        */
        return arrlist;
    }

    //Reads all goals that are set to completed! returns an array list with goal objects in them goal objects contain
    //the data of the goal but have no functionality.
    public ArrayList ReadCompletedGoals()
    {
        ArrayList arrlist = new ArrayList();

        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM goals WHERE status = 1;";

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Goal currentGoal = new Goal(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1));
                        arrlist.Add(currentGoal);
                    }
                }
            }
            connection.Close();
        }

        //Debug code that checks if the right tasks were selected
        /**
        for (int i = 0; i < arrlist.Count; i++)
        {
            Debug.Log("Im Here2!");
            Task currTask = (Task)arrlist[i];
            Debug.Log("name: " + currTask.taskname + " id: " + currTask.id);
        }
        */
        return arrlist;
    }

    //Reads a specific goal idetified by its ID! returns an array list with goal objects in them goal objects contain
    //the data of the goal but have no functionality.
    public ArrayList ReadUser()
    {
        ArrayList arrlist = new ArrayList();

        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM users;";

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User currentUser = new User(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(1));
                        arrlist.Add(currentUser);
                    }
                }
            }
            connection.Close();
        }

        //Debug code that checks if the right tasks were selected
        /**
        for (int i = 0; i < arrlist.Count; i++)
        {
            Debug.Log("Im Here2!");
            Task currTask = (Task)arrlist[i];
            Debug.Log("name: " + currTask.taskname + " id: " + currTask.id);
        }
        */
        return arrlist;
    }
}
