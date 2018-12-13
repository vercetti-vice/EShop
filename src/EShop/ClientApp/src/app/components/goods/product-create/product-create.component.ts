import { Component, OnInit } from '@angular/core';
import {Category} from '../../../models/category.model';
import {CategoryService} from '../../../services/category.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {first} from 'rxjs/operators';
import {Router} from '@angular/router';
import {Brand} from '../../../models/brand.model';
import {BrandService} from '../../../services/brand.service';
import {ProductService} from '../../../services/product.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css']
})
export class ProductCreateComponent implements OnInit {

  categories: Category[] = [];
  brands: Brand[] = [];
  createProductForm: FormGroup;
  loading = false;
  submitted = false;

  sorts = '';
  filters = '';
  page = 1;
  pageSize = 10000;

  constructor(private categoryService: CategoryService,
              private brandService: BrandService,
              private productService: ProductService,
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit() {
    this.createProductForm = this.formBuilder.group({
      name: ['', Validators.required],
      brandId: ['', Validators.required],
      categoryId: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', Validators.required],
      imageUrl: ['', Validators.required],
    });
    this.loadAllCategories();
    this.loadAllBrands();
  }

  // convenience getter for easy access to form fields
  get f() { return this.createProductForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.createProductForm.invalid) {
      return;
    }

    this.loading = true;
    this.productService.create(this.createProductForm.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/product-list']);
        },
        error => {
          this.loading = false;
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
