import {Injectable, Injector} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {ConfigurationService} from './configuration.service';
import {Category} from '../models/category.model';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient, private configurations: ConfigurationService, injector: Injector) { }

  getAll(sorts: string, filters: string, page: number, pageSize: number) {
    return this.http.get<Category[]>(`${this.configurations.baseUrl}/api/category/GetAll?sorts=` + sorts + `&filters=` + filters + `&page=` + page + `&pageSize=` + pageSize);
  }

  getById(id: number) {
    return this.http.get<Category>(`${this.configurations.baseUrl}/api/category/GetById?id=` + id);
  }

  create(category: Category) {
    return this.http.post(`${this.configurations.baseUrl}/api/category/Create`, category, httpOptions);
  }

  update(category: Category) {
    return this.http.put(`${this.configurations.baseUrl}/api/category/Update`, category);
  }

  delete(id: number) {
    return this.http.delete(`${this.configurations.baseUrl}/api/category/Delete?id=` + id);
  }
}
