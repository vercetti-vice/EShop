import {Injectable, Injector} from '@angular/core';
import {HttpHeaders, HttpClient} from '@angular/common/http';
import {Brand} from '../models/brand.model';
import {ConfigurationService} from './configuration.service';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  constructor(private http: HttpClient, private configurations: ConfigurationService, injector: Injector) { }

  getAll(sorts: string, filters: string, page: number, pageSize: number) {
    return this.http.get<Brand[]>(`${this.configurations.baseUrl}/api/brand/GetAll?sorts=` + sorts + `&filters=` + filters + `&page=` + page + `&pageSize=` + pageSize);
  }

  getById(id: number) {
    return this.http.get<Brand>(`${this.configurations.baseUrl}/api/brand/GetById?id=` + id);
  }

  create(brand: Brand) {
    return this.http.post(`${this.configurations.baseUrl}/api/brand/Create`, brand, httpOptions);
  }

  update(brand: Brand) {
    return this.http.put(`${this.configurations.baseUrl}/api/brand/Update`, brand);
  }

  delete(id: number) {
    return this.http.delete(`${this.configurations.baseUrl}/api/brand/Delete?id=` + id);
  }
}
