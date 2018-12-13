import {Component, Input, OnInit} from '@angular/core';
import {Category} from '../../../models/category.model';
import {CategoryService} from '../../../services/category.service';
import {first} from 'rxjs/operators';
import {Product} from '../../../models/product.model';
import {ProductService} from '../../../services/product.service';

@Component({
  selector: 'app-product-managment',
  templateUrl: './product-managment.component.html',
  styleUrls: ['./product-managment.component.css']
})
export class ProductManagmentComponent implements OnInit {

  products: Product[] = [];

  nextIsActive = false;

  sorts = '';
  filters = '';
  page = 1;
  pageSize = 10;

  constructor(private productService: ProductService) {
  }

  ngOnInit() {
    this.loadAllProducts();
  }

  @Input('sort-direction')
  sortDirection = '';

  sort() {
    this.sortDirection = this.sortDirection === 'Name' ? '-Name' : 'Name';
    this.sortDirection = this.sortDirection === 'Price' ? '-Price' : 'Price';
    this.sorts = this.sortDirection;
    this.loadAllProducts();
  }

  deleteProduct(id: number) {
    this.productService.delete(id).pipe(first()).subscribe(() => {
      this.loadAllProducts();
    });
  }

  private loadAllProducts() {
    this.productService.getAll(this.sorts, this.filters, this.page, this.pageSize).pipe(first()).subscribe(products => {
      this.products = products;
      if (this.products.length < this.pageSize) {
        this.nextIsActive = false;
      } else {
        this.nextIsActive = true;
      }
    });
  }

  previousPage() {
    this.page -= 1;
    this.loadAllProducts();
    this.nextIsActive = true;
  }

  nextPage() {
    this.page += 1;
    this.loadAllProducts();
  }
}
