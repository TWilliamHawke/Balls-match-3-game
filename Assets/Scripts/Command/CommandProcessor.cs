using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CommandProcessor : Singleton<CommandProcessor>
{
    List<Icommand> _commandsHistory = new List<Icommand>();

    protected override void Awake()
    {
        base.Awake();
        InputReader.OnCancelKeyPress += Undo;
    }

    void OnDestroy()
    {
        InputReader.OnCancelKeyPress -= Undo;
    }

    public void ExecuteCommand(Icommand command)
    {
        command.Execute();
        CheckListSize();
        _commandsHistory.Add(command);
    }

    bool HistoryContainsMoveCommand()
    {
        foreach(var command in _commandsHistory)
        {
            if(command is MoveCommand) {
                return true;
            }
        }
        return false;
    }

    void CheckListSize()
    {
        if(_commandsHistory.Count > 999)
        {
            Debug.Log("Clear list");
            _commandsHistory.RemoveRange(0, 99);
        }
    }

    public void Undo()
    {
        if(!HistoryContainsMoveCommand()) {
            return;
        }

        while(true)
        {
            var command = _commandsHistory.Last();
            command.Undo();
            _commandsHistory.RemoveAt(_commandsHistory.Count - 1);

            if(command is MoveCommand) {
                break;
            }
        }
    }
}
