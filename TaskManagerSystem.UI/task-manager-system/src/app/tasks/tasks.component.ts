import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Task } from '../models/task';
import { TaskService } from '../services/task.service';



@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit{

  displayedColumns: string[] = ['id', 'title', 'description', 'date', 'status', 'actions'];
  tasks: Task[] = [];
  
  
  constructor(private taskService: TaskService, private router:Router) {}

  ngOnInit(): void {
    this.getTasks();
  }

  detail(row:any):void{
    const link = `/detail/${row.id} `;
    this.router.navigate([link]);
  }

  getTasks(): void {
    this.taskService.getTasks().subscribe(
      tasks => {
        this.tasks = tasks.data;
      }     
    );
  }

  add(title: string, description: string): void {
    title = title.trim();
    if (!title) { return; }
    this.taskService.addTask({ title, description } as Task)
      .subscribe(task => {
        this.getTasks();
      });
  }

  delete(task: Task): void {
    this.tasks = this.tasks.filter(h => h !== task);
    this.taskService.deleteTask(task.id).subscribe();
  }
}
