import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {first} from 'rxjs/operators';
import {ActivatedRoute, Router} from '@angular/router';
import {Product} from '../../../models/product.model';
import {ProductService} from '../../../services/product.service';
import {CategoryService} from '../../../services/category.service';
import {BrandService} from '../../../services/brand.service';
import {Category} from '../../../models/category.model';
import {Brand} from '../../../models/brand.model';

@Component({
  selector: 'app-product-editor',
  templateUrl: './product-editor.component.html',
  styleUrls: ['./product-editor.component.css']
})
export class ProductEditorComponent implements OnInit {

  product = new Product();
  products: Product[] = [];
  categories: Category[] = [];
  brands: Brand[] = [];
  id: number;
  editProductForm: FormGroup;
  loading = false;
  submitted = false;

  defaultBrand = {
    id : 1,
    name : 'Hello'
  };

  sorts = '';
  filters = '';
  page = 1;
  pageSize = 100;

  constructor(private categoryService: CategoryService,
              private brandService: BrandService,
              private productService: ProductService,
              private activateRoute: ActivatedRoute,
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit() {
    this.id = this.activateRoute.snapshot.params['id'];
    this.getProduct(this.id);
    this.loadAllProducts();
    this.loadAllBrands();
    this.loadAllCategories();
    // this.editProductForm = this.formBuilder.group({
    //   id: [this.product.id, Validators.required],
    //   name: [this.product.name, Validators.required],
    //   brandId: [this.product.brandId, Validators.required],
    //   categoryId: [this.product.categoryId, Validators.required],
    //   description: [this.product.description, Validators.required],
    //   price: [this.product.price, Validators.required],
    //   imageUrl: [this.product.imageUrl, Validators.required]
    // });
  }

  // convenience getter for easy access to form fields
  get f() { return this.editProductForm.controls; }

  private getProduct(id: number) {
    this.productService.getById(id).pipe(first()).subscribe(product => {
      this.product.id = product.id;
      this.product.name = product.name;
      this.product.description = product.description;
      this.product.price = product.price;
      this.product.imageUrl = product.imageUrl;
      this.product.brandId = product.brandId;
      this.product.categoryId = product.categoryId;
      this.product.brand = product.brand;
      this.product.category = product.category;

      this.editProductForm = this.formBuilder.group({
        id: [product.id, Validators.required],
        name: [product.name, Validators.required],
        brandId: [product.brandId, Validators.required],
        categoryId: [product.categoryId, Validators.required],
        description: [product.description, Validators.required],
        price: [product.price, Validators.required],
        imageUrl: [product.imageUrl, Validators.required]
      });
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
          this.router.navigate(['/product-list']);
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

  private loadAllCategories() {
    this.categoryService.getAll(this.sorts, this.filters, this.page, this.pageSize).pipe(first()).subscribe(categories => {
      this.categories = categories;
    });
  }

  private loadAllBrands() {
    this.brandService.getAll(this.sorts, this.filters, this.page, this.pageSize).pipe(first()).subscribe(brands => {
      this.brands = brands;
    });
  }
}
