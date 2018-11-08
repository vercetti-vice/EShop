import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Category} from "../_models/category";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  getAll(sorts: string, filters: string, page: number, pageSize: number) {
    return this.http.get<Category[]>(`${config.apiUrl}/category/GetAll?sorts=` + sorts + `&filters=` + filters + `&page=` + page + `&pageSize=` + pageSize);
  }

  getById(id: number) {
    return this.http.get<Category>(`${config.apiUrl}/category/GetById?id=` + id);
  }

  create(category: Category) {
    return this.http.post(`${config.apiUrl}/category/Create`, category, httpOptions);
  }

  update(category: Category) {
    return this.http.put(`${config.apiUrl}/category/Update`, category);
  }

  delete(id: number) {
    return this.http.delete(`${config.apiUrl}/category/Delete?id=` + id);
  }
}
