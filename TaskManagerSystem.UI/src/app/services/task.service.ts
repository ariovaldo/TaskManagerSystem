import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

import { TASK_STATUS } from '../models/mock-tasks';
import { Task } from '../models/task';
import { MessageService } from './message.service';
import { TaskStatus } from '../models/task-status';


@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private tasksUrl = 'https://localhost:7235/Task';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) { }

  //v1
  // getTasks(): Observable<Task[]> {
  //   const tasks = of(TASKS);
  //   this.messageService.add('TaskService: fetched tasks')
  //   return tasks;
  // }

  getStatusTasks(): Observable<TaskStatus[]> {
    const tasksStatus = of(TASK_STATUS);
    this.messageService.add('TaskStatus: fetched tasks')
    return tasksStatus;
  }

  /** GET tasks from the server */
  getTasks(): Observable<any> {
    return this.http.get<any>(this.tasksUrl)
      .pipe(
        //tap(_ => this.log('fetched Tasks')),
        catchError(this.handleError<any>('getTasks', []))
      );
  }

  /** GET task from the server */
  getTask(id: number): Observable<any> {
    const url = `${this.tasksUrl}/${id}`;
    return this.http.get<any>(url)
    .pipe(
      tap(_ => this.log(`fetched task id=${id}`)),
      catchError(this.handleError<any>(`getTask id=${id}`))
    );
  }

  /** POST: add a new task to the server */
  addTask(task: Task): Observable<any> {
    return this.http.post<any>(this.tasksUrl+"/message", task, this.httpOptions)
    .pipe(
      tap((newTask: Task) => this.log(`added task id=${newTask.id}`)),
      catchError(this.handleError<any>('addTask'))
    );
  }

  /** PUT: update the task on the server */
  updateTask(task: Task): Observable<any> {
    const url =  `${this.tasksUrl}/${task.id} `;
    return this.http.put(url, task, this.httpOptions)
    .pipe(
      tap(_ => this.log(`updated task id=${task.id}`)),
      catchError(this.handleError<any>('updateTask'))
    );
  }

  /** DELETE: delete the task from the server */
  deleteTask(id: number): Observable<Task> {
    const url = `${this.tasksUrl}/${id}`;
  
    return this.http.delete<Task>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted task id=${id}`)),
      catchError(this.handleError<any>('deleteTask'))
    );
  }

  /** Log a taskService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`TaskService: ${message}`);
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error("Error!!!", error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

}
