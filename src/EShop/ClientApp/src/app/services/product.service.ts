import {Injectable, Injector} from '@angular/core';
import {ConfigurationService} from './configuration.service';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Product} from '../models/product.model';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient, private configurations: ConfigurationService, injector: Injector) { }

  getAll(sorts: string, filters: string, page: number, pageSize: number) {
    return this.http.get<Product[]>(`${this.configurations.baseUrl}/api/product/GetAll?sorts=` + sorts + `&filters=` + filters + `&page=` + page + `&pageSize=` + pageSize);
  }

  getById(id: number) {
    return this.http.get<Product>(`${this.configurations.baseUrl}/api/product/GetById?id=` + id);
  }

  create(product: Product) {
    return this.http.post(`${this.configurations.baseUrl}/api/product/Create`, product, httpOptions);
  }

  update(product: Product) {
    return this.http.put(`${this.configurations.baseUrl}/api/product/Update`, product);
  }

  delete(id: number) {
    return this.http.delete(`${this.configurations.baseUrl}/api/product/Delete?id=` + id);
  }
}
