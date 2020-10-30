/* Tal Rastopchin
 * March 6, 2020
 *
 * A GameManager class that keeps track of GameTasks
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // the frequency to check if all tasks were completed in seconds
    public float updateFrequency = 1;

    // whether or not all GameTasks were completed
    private bool gameTasksAreCompleted = false;

    // the set of GameTasks the GameManager is managing
    private HashSet<GameTask> gameTaskSet;

    // Start is called before the first frame update
    void Start()
    {
        // find all GameTasks to manage
        GameTask[] gameTasks = Object.FindObjectsOfType<GameTask>();
        gameTaskSet = new HashSet<GameTask>(gameTasks);

        // start managing the GameTasks
        StartCoroutine(ManageGameTasksRoutine());
    }

    // the coroutine that manages the game tasks at the specified frequency
    private IEnumerator ManageGameTasksRoutine ()
    {
        do
        {
            gameTasksAreCompleted = ManageGameTasks();
            if (gameTasksAreCompleted)
            {
                OnGameCompleted();
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(updateFrequency);
            }
        } while (!gameTasksAreCompleted);
    }


    /* manages the set of game tasks and returns whether or not all GameTasks
     * were completed.
     */
    private bool ManageGameTasks ()
    {
        // if there are GameTasks left
        if (gameTaskSet.Count > 0)
        {
            LinkedList<GameTask> completedTasks = new LinkedList<GameTask>();

            // determine if each task is completed and act accordingly
            foreach (GameTask task in gameTaskSet)
            {
                // if completed all OnCompleted and remove it from the list ot tasks
                if (task.IsCompleted())
                {
                    task.OnCompleted();
                    completedTasks.AddLast(task);
                }
            }

            // remove completed tasks
            foreach (GameTask task in completedTasks)
            {
                gameTaskSet.Remove(task);
            }

            // signal whether or not all tasks were  completed
            return gameTaskSet.Count == 0;
        }
        else
        {
            // signal all tasks completed
            return true;
        }
    }

    // what the GameManager does when the game has completed
    private void OnGameCompleted ()
    {
        Debug.Log("You have completed the game ");
    }

}

/* abstract GameTask class inherits from MonoBehaviour so that a GameTask
 * can still act as a scriptable object but communicate with the GameManager
 */
public abstract class GameTask : MonoBehaviour {
    // this method lets the GameManager know when the task is completed
    public abstract bool IsCompleted();

    // the GameManager calls this method when the task is completed
    public abstract void OnCompleted();
}