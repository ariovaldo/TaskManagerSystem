import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { delay } from 'rxjs/operators';

import { TaskService } from '../services/task.service';
import { TaskStatus } from '../models/task-status';


@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent implements OnInit {
  
  formTask: FormGroup;
  id = 0;
  taskStatus: TaskStatus[] = [];
  isEditar =false;

  constructor(
    private route: ActivatedRoute,
    private taskService: TaskService,
    private location: Location,
    private fb:FormBuilder
  ) 
  {
    this.formTask = this.fb.group({
      title:['', Validators.required],
      description:[''],
      date:['', Validators.required],
      status:[null]
    });
  }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if(this.id !== 0){
      this.getTask();
    }
    this.getStatusTask();
  }
  
  getTask(): void {
      this.taskService.getTask(this.id)
      .subscribe(
        task => {
          if(task?.data?.id !== 0){
            this.isEditar = true;
            this.formTask.patchValue(task.data);
          }
      });
  }

  getStatusTask(): void {
    this.taskService.getStatusTasks()
    .subscribe( task => this.taskStatus = task );
}
  
  onSubmit(): void {
    if (this.formTask.invalid) { return; }
    if (this.isEditar) {
      this.formTask.value.id = this.id;
      this.taskService.updateTask(this.formTask.value)
      .pipe(delay(1000))
      .subscribe(() => this.goBack());
    } else {
      this.taskService.addTask(this.formTask.value)
      .pipe(delay(1000))
      .subscribe(() => this.goBack());
    }
  }

  get f(): any { return this.formTask.controls; }

  goBack(): void {
    this.location.back();
  }
}
