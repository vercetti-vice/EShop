import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import { Brand } from '../_models/brand';
import {Observable} from "rxjs/internal/Observable";
import {map} from "rxjs/operators";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class BrandService {



  constructor(private http: HttpClient) { }

  getAll(sorts: string, filters: string, page: number, pageSize: number) {
    return this.http.get<Brand[]>(`${config.apiUrl}/brand/GetAll?sorts=` + sorts + `&filters=` + filters + `&page=` + page + `&pageSize=` + pageSize);
  }

  getById(id: number) {
    return this.http.get<Brand>(`${config.apiUrl}/brand/GetById?id=` + id);
  }

  create(brand: Brand) {
    return this.http.post(`${config.apiUrl}/brand/Create`, brand, httpOptions);
  }

  update(brand: Brand) {
    return this.http.put(`${config.apiUrl}/brand/Update`, brand);
  }

  delete(id: number) {
    return this.http.delete(`${config.apiUrl}/brand/Delete?id=` + id);
  }
}
