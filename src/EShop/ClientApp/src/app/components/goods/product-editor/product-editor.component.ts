import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {first} from 'rxjs/operators';
import {ActivatedRoute, Router} from '@angular/router';
import {Product} from '../../../models/product.model';
import {ProductService} from '../../../services/product.service';

@Component({
  selector: 'app-product-editor',
  templateUrl: './product-editor.component.html',
  styleUrls: ['./product-editor.component.css']
})
export class ProductEditorComponent implements OnInit {

  product = new Product();
  products: Product[] = [];
  id: number;
  editProductForm: FormGroup;
  loading = false;
  submitted = false;


  sorts = '';
  filters = '';
  page = 1;
  pageSize = 100;

  constructor(private productService: ProductService,
              private activateRoute: ActivatedRoute,
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit() {
    this.id = this.activateRoute.snapshot.params['id'];
    this.getCategory(this.id);
    this.loadAllProducts();
    this.editProductForm = this.formBuilder.group({
      id: [this.product.id, Validators.required],
      name: [this.product.name, Validators.required],
      description: [this.product.description, Validators.required],
      price: [this.product.price, Validators.required]
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.editProductForm.controls; }

  private getCategory(id: number) {
    this.productService.getById(id).pipe(first()).subscribe(product => {
      this.product.id = product.id;
      this.product.name = product.name;
      this.product.description = product.description;
      this.product.price = product.price;
    });
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.editProductForm.invalid) {
      return;
    }

    this.loading = true;
    this.productService.update(this.editProductForm.value)
      .pipe(first(), )
      .subscribe(
        data => {
          this.router.navigate(['/category-list']);
        },
        error => {
          this.loading = false;
        });
  }

  private loadAllProducts() {
    this.productService.getAll(this.sorts, this.filters, this.page, this.pageSize).pipe(first()).subscribe(products => {
      this.products = products;
    });

  }
}
