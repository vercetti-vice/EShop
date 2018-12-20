import { Component, OnInit } from '@angular/core';
import {Product} from '../../../models/product.model';
import {ActivatedRoute, Router} from '@angular/router';
import {BrandService} from '../../../services/brand.service';
import {ProductService} from '../../../services/product.service';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.css']
})
export class ProductInfoComponent implements OnInit {

  product: Product;
  id: number;

  constructor(private productService: ProductService,
              private activateRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.id = this.activateRoute.snapshot.params['id'];
    this.getProduct(this.id);
  }

  private getProduct(id: number) {
    this.productService.getById(id).pipe(first()).subscribe(product => {
      this.product = product;
    });
  }
}
