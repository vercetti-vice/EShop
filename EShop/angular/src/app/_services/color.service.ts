import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Color} from "../_models/color";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ColorService {

  constructor(private http: HttpClient) { }

  getAll(sorts: string, filters: string, page: number, pageSize: number) {
    return this.http.get<Color[]>(`${config.apiUrl}/color/GetAll?sorts=` + sorts + `&filters=` + filters + `&page=` + page + `&pageSize=` + pageSize);
  }

  getById(id: number) {
    return this.http.get<Color>(`${config.apiUrl}/color/GetById?id=` + id);
  }

  create(color: Color) {
    return this.http.post(`${config.apiUrl}/color/Create`, color, httpOptions);
  }

  update(color: Color) {
    return this.http.put(`${config.apiUrl}/color/Update`, color);
  }

  delete(id: number) {
    return this.http.delete(`${config.apiUrl}/color/Delete?id=` + id);
  }
}
