﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NTestData.Samples
{
    namespace DomainDrivenDesign
    {
        internal interface IAggregateRoot
        {
            // just for demo purpose
        }

        internal class TodoList : IAggregateRoot
        {
            private readonly List<Task> _tasks = new List<Task>();

            public string Name { get; set; }

            public IEnumerable<Task> Tasks
            {
                get { return _tasks; }
            }

            public IEnumerable<Task> CompleteTasks
            {
                get { return _tasks.Where(t => t.IsComplete); }
            }

            public IEnumerable<Task> IncompleteTasks
            {
                get { return _tasks.Where(t => !t.IsComplete); }
            }

            public void AddTask(Task newTask)
            {
                if (String.IsNullOrWhiteSpace(newTask.Title))
                {
                    throw new ArgumentException("Task has no meaningful title");
                }
                if (TaskWithSameTitleAlreadyExists(newTask.Title))
                {
                    throw new InvalidOperationException("List already contains task with title '" + newTask.Title + "'");
                }

                _tasks.Add(newTask);
            }

            private bool TaskWithSameTitleAlreadyExists(string taskTitle)
            {
                return _tasks.Any(existingTask => String.Equals(existingTask.Title,
                                                                taskTitle,
                                                                StringComparison.InvariantCultureIgnoreCase));
            }

            public void MarkCompleteTaskAtPosition(int position)
            {
                int index=ConvertPositionToZeroBasedIndex(position);
                var task = _tasks[index];
                task.IsComplete = true;
                _tasks[index] = task;
            }

            private int ConvertPositionToZeroBasedIndex(int position)
            {
                if (position < 1)
                {
                    throw new ArgumentOutOfRangeException(
                        "position", "Position must be positive number starting from 1.");
                }
                if (_tasks.Count < position)
                {
                    if (_tasks.Count == 0)
                    {
                        throw new ArgumentOutOfRangeException("position", "There is no tasks in list.");
                    }
                    throw new ArgumentOutOfRangeException("position",
                                                          "Unable to mark task at position " + position
                                                          + " complete as there are only "
                                                          + _tasks.Count + " tasks in list.");
                }
                return position - 1;
            }
        }

        internal interface IValueObject
        {
            // just for demo purpose
        }

        internal struct Task : IValueObject
        {
            public string Title { get; set; }
            public string Details { get; set; }
            public bool IsComplete { get; set; }
        }

        public class Tests
        {
            [Fact]
            public void DemoTraditionalWay()
            {
                // arrange
                var todoList = new TodoList { Name = "Learn NTestData" };
                todoList.AddTask(new Task
                                     {
                                         Title = "read documentation",
                                         Details = "at http://ntestdata.net"
                                     });
                todoList.AddTask(new Task
                                     {
                                         Title = "check out source code",
                                         Details = "at https://github.com/NTestData/NTestData"
                                     });
                todoList.AddTask(new Task
                                     {
                                         Title = "download package",
                                         Details = "from http://nuget.org/packages/NTestData"
                                     });
                todoList.AddTask(new Task
                                     {
                                         Title = "try it in your current project(s)"
                                     });

                // act
                todoList.MarkCompleteTaskAtPosition(1);

                // assert
                todoList.Tasks.Should().HaveCount(4);
                todoList.CompleteTasks.Should().HaveCount(1);
                todoList.IncompleteTasks.Should().HaveCount(3);
            }

            [Fact]
            public void DemoMoreNaturalWay()
            {
                // arrange
                var todoList = TestData.Create(
                    ToDo("Learn NTestData",
                         Item("read documentation", "at http://ntestdata.net"),
                         Item("check out source code", "at https://github.com/NTestData/NTestData"),
                         Item("download package", "from http://nuget.org/packages/NTestData"),
                         Item("try it in your current project(s)")));

                // act
                todoList.MarkCompleteTaskAtPosition(1);

                // assert
                todoList.Tasks.Should().HaveCount(4);
                todoList.CompleteTasks.Should().HaveCount(1);
                todoList.IncompleteTasks.Should().HaveCount(3);
            }

            private Action<TodoList> ToDo(string name, params Action<TodoList>[] withTasks)
            {
                return list =>
                           {
                               list.Name = name;
                               list.Customize(withTasks);
                           };
            }

            private Action<TodoList> Item(string title, string details = null)
            {
                return list => list.AddTask(new Task { Title = title, Details = details });
            }
        }
    }
}
