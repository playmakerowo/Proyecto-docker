import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Users } from '../models/Users.model';

@Injectable({
  providedIn: 'root'
})
export class ApiControllerService {
  private apiGetList = 'http://localhost:5214/api/Users';
  private apiGetUser = 'http://localhost:5214/api/Users/'; //es la misma ruta para eliminar, modificar y buscar

  constructor(private http: HttpClient) { }

  getUsers(): Observable<Users[]>{
    return this.http.get<Users[]>(this.apiGetList);
  }

  getUser(id: number): Observable<Users>{
    return this.http.get<Users>(this.apiGetUser + id); //IMPORTANTE NO usar [] con corchetes por que el controlador espera un arreglo y no objeto individual
  }

  removeUser(id: number): Observable<Users>{
    return this.http.delete<Users>(this.apiGetUser + id);
  }

  createUser(listaUsuarios: any): Observable<Users>{
    return this.http.post<Users>(this.apiGetList, listaUsuarios);
  }

  updateUser(id: number, listaUsuarios: any): Observable<Users>{
    return this.http.put<Users>(this.apiGetUser + id, listaUsuarios)
  }
}
