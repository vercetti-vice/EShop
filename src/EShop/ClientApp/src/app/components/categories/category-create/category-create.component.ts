import { Component, OnInit } from '@angular/core';
import {Category} from '../../../models/category.model';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {CategoryService} from '../../../services/category.service';
import {Router} from '@angular/router';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.css']
})

export class CategoryCreateComponent implements OnInit {

  category: Category;
  categories: Category[] = [];
  createCategoryForm: FormGroup;
  loading = false;
  submitted = false;

  sorts = '';
  filters = '';
  page = 1;
  pageSize = 10000;

  constructor(private categoryService: CategoryService,
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit() {
    this.createCategoryForm = this.formBuilder.group({
      name: ['', Validators.required],
      parentCategoryId: ['']
    });
    this.loadAllCategories();
  }

  // convenience getter for easy access to form fields
  get f() { return this.createCategoryForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.createCategoryForm.invalid) {
      return;
    }

    this.loading = true;
    this.categoryService.create(this.createCategoryForm.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/category-list']);
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

}
