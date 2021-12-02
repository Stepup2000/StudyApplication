using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IDbCommand = System.Data.IDbCommand;
using IDbConnection = System.Data.IDbConnection;
using Mono.Data.Sqlite;
using System.Data;

public class DatabaseCon
{

    string DATABASE_NAME = "/pomoco.s3db";

    //Our Database Connection
    string conn;

    //The attribute we store our queries in
    string SQLQuery;

    // Start is called before the first frame update
    public void StartDB()
    {
        string filepath = Application.dataPath + DATABASE_NAME;
        Debug.Log($"filepath={filepath}");
        conn = "URI=file:" + filepath;

        //create the Database
        CreateDB();

        //CreateGoal("Test", 0, 1);
        //CreateTask(1, "test", 200, 0, 1);
        //CreateUser("avatar1", "audio", "testuser");

        //CreateGoal("Test2", 0, 1);
        //CreateTask(2, "test2", 200, 0, 1);
        //CreateUser("avatar2", "visual", "testuser2");

        //ReadAllTasks();
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
                command.CommandText = "CREATE TABLE IF NOT EXISTS goals (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name TEXT NOT NULL, status NUMBER(1), gpriority INTEGER);";
                command.ExecuteNonQuery();

                //Task Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS tasks (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name TEXT NOT NULL, time INTEGER, tpriority INTEGER, is_completed NUMBER(1), treward TEXT, goal_id INTEGER, FOREIGN KEY (goal_id) REFERENCES goals (id));";
                command.ExecuteNonQuery();

                //User Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name TEXT, learningType TEXT, avatar TEXT);";
                command.ExecuteNonQuery();

                //Notes Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS notes (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, ntext TEXT, category TEXT, task_id INTEGER, FOREIGN KEY (task_id) REFERENCES tasks (id));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //Creates a new Goal. Goals need a name! 
    public bool CreateGoal(string name, int status, int priority)
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
                    command.CommandText = "INSERT INTO goals (name, status, gpriority) VALUES ('" + name + "','" + status + "','" + priority + "');";
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
    public bool CreateTask(int goalid, string name, int time, int completed, int priority, string reward)
    {
        Debug.Log("e.wak,lrnf wek.rf wke.j fr");
        //checks if a name was entered, if a valid time was entered and if the task is incomplete
        if (name != "" && time != 0)
        {
            using (var connection = new SqliteConnection(conn))
            {
                connection.Open();

                //access the database using a command
                using (var command = connection.CreateCommand())
                {
                    //creates a new goal with the according name
                    command.CommandText = "INSERT INTO tasks (name, time, is_completed, goal_id, tpriority, treward) VALUES ('" + name + "','" + time + "','" + completed + "','" + goalid + "','" + priority + "','" + reward + "');";
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


    //Creates a new Note. A Note is related to a task and needs the task_id. Additionally it 
    //needs a name, text and a category.
    public bool CreateNote(string text, string category, int task_id)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {

                //Debug.Log("laeriwskgnbweklrfg");
                //creates a new goal with the according name
                command.CommandText = "INSERT INTO notes (ntext, category, task_id) VALUES ('" + text + "','" + category + "','" + task_id + "');";
                //Debug.Log(goalid);
                command.ExecuteNonQuery();

            }
            connection.Close();
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
                        DataTask currentTask = new DataTask(reader.GetInt32(6), (int)reader.GetInt32(4), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(0), reader.GetInt32(3), reader.GetString(5));
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
            DataTask currTask = (DataTask)arrlist[i];
            Debug.Log("name: " + currTask.taskname + " id: " + currTask.id);
        }
        */
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
                        DataTask currentTask = new DataTask(reader.GetInt32(6), (int)reader.GetInt32(4), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(0), reader.GetInt32(3), reader.GetString(5));
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

    public ArrayList ReadNotesForTaskX(int taskID)
    {
        ArrayList arrlist = new ArrayList();

        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM notes WHERE task_id = " + taskID;

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataNote currentNote = new DataNote(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt16(3));
                        arrlist.Add(currentNote);
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
                command.CommandText = "SELECT * FROM tasks WHERE is_completed = 1";

                //This code reads the select statement by executing it and the reading it line by line until there is no line left
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataTask currentTask = new DataTask(reader.GetInt32(6), (int)reader.GetInt32(4), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(0), reader.GetInt32(3), reader.GetString(5));
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
                        DataTask currentTask = new DataTask(reader.GetInt32(6), (int)reader.GetInt32(4), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(0), reader.GetInt32(3), reader.GetString(5));
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
                        DataGoal currentGoal = new DataGoal(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1), reader.GetInt32(3));
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
                        DataGoal currentGoal = new DataGoal(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1), reader.GetInt32(3));
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
                        DataGoal currentGoal = new DataGoal(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1), reader.GetInt32(3));
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


    //Deleting a certain task by its ID. If the task exists it gets deleted from the database
    //When deleting a task there is nothing to worry about so no further logic is required. 
    public void DeleteTask(int taskID)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command deletes a Task
                command.CommandText = "DELETE FROM tasks WHERE id = " + taskID + ";";
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }

    //Deleting a certain note by its ID. If the note exists it gets deleted from the database
    //When deleting a note there is nothing to worry about so no further logic is required. 
    public void DeleteNote(int noteID)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command deletes a Task
                command.CommandText = "DELETE FROM notes WHERE id = " + noteID + ";";
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }

    //Deleting a certain tasks by its goal ID. If the tasks exist they get deleted from the database
    //When deleting a task there is nothing to worry about so no further logic is required. 
    public void DeleteTaskByGoal(int goalID)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command deletes all Tasks for the goal
                command.CommandText = "DELETE FROM tasks WHERE goal_id = " + goalID + ";";
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }

    //Deletes a specific goal. All the tasks connected to that goal get deleted as well!
    public void DeleteGoal(int goalID)
    {
        //first the task deletion is called
        DeleteTaskByGoal(goalID);
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command deletes the goal
                command.CommandText = "DELETE FROM goals WHERE id= " + goalID + ";";
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }

    //Updates the Avatar of the user if they want to choose a new one. Requires the Avatar String. This always selects the first user!
    public void UpdateUserAvatar(string avatar)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command updates the user
                command.CommandText = "UPDATE users SET avatar = '" + avatar + "' WHERE id = 1;";
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }

    //Updates the Name of the user if they want to choose a new one. Requires the Name String. This always selects the first user!
    public void UpdateUserName(string name)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command updates the user
                command.CommandText = "UPDATE users SET name = '" + name + "' WHERE id = 1;";
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }

    //Updates the Name of a specific goal identified by its ID
    public void UpdateGoalName(string name, int id)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //Debug.Log("here + " + id);
                //This Command updates the goal
                command.CommandText = "UPDATE goals SET name = '" + name + "' WHERE id = " + id + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //Updates the priority of a specific goal identified by its ID
    public void UpdateGoalPriority(int prio, int id)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command updates the goal
                command.CommandText = "UPDATE goals SET gpriority = '" + prio + "' WHERE id = " + id + ";";
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }

    //Updates the Status of a specific goal identified by its ID
    //Status can only be a 0 or a 1!
    public void UpdateGoalStatus(int status, int id)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command updates the task
                command.CommandText = "UPDATE goals SET status = '" + status + "' WHERE id = " + id + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //Updates the name of a specific task identified by its ID
    public void UpdateTaskName(string name, int id)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command updates the task
                command.CommandText = "UPDATE tasks SET name = '" + name + "' WHERE id = " + id + ";";
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }

    //Updates the priority of a specific task identified by its ID
    public void UpdateTaskPriority(int priority, int id)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command updates the task
                command.CommandText = "UPDATE tasks SET tpriority = '" + priority + "' WHERE id = " + id + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //Updates the time of a specific task identified by its ID
    public void UpdateTaskTime(int time, int id)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command updates the task
                command.CommandText = "UPDATE tasks SET time = '" + time + "' WHERE id = " + id + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //Updates the Reward of a specific task identified by its ID
    public void UpdateTaskReward(string reward, int id)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command updates the task
                command.CommandText = "UPDATE tasks SET treward = '" + reward + "' WHERE id = " + id + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //Updates the Status of a specific task identified by its ID
    //Status can only be a 0 or a 1!
    public void UpdateTaskStatus(int status, int id)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                Debug.Log("changing time id: " + id + " status: " + status);
                //This Command updates the task
                command.CommandText = "UPDATE tasks SET is_completed = '" + status + "' WHERE id = " + id + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //Updates the Status of a specific task identified by its ID
    //Status can only be a 0 or a 1!
    public void UpdateNoteText(string text, int id)
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            //access the database using a command
            using (var command = connection.CreateCommand())
            {
                //This Command updates the task
                command.CommandText = "UPDATE notes SET ntext = '" + text + "' WHERE id = " + id + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
