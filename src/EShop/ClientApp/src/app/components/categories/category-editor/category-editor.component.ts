import { Component, OnInit } from '@angular/core';
import {first} from 'rxjs/operators';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {CategoryService} from '../../../services/category.service';
import {Category} from '../../../models/category.model';

@Component({
  selector: 'app-category-editor',
  templateUrl: './category-editor.component.html',
  styleUrls: ['./category-editor.component.css']
})
export class CategoryEditorComponent implements OnInit {

  category = new Category();
  categories: Category[] = [];
  id: number;
  editCategoryForm: FormGroup;
  loading = false;
  submitted = false;
  selectedOption = true;

  sorts = '';
  filters = '';
  page = 1;
  pageSize = 100;

  constructor(private categoryService: CategoryService,
              private activateRoute: ActivatedRoute,
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit() {
    this.id = this.activateRoute.snapshot.params['id'];
    this.getCategory(this.id);
    this.loadAllCategories();
    // this.editCategoryForm = this.formBuilder.group({
    //   id: [this.category.id, Validators.required],
    //   name: [this.category.name, Validators.required],
    //   parentCategoryId: [this.category.parentCategoryId]
    // });
  }

  // convenience getter for easy access to form fields
  get f() { return this.editCategoryForm.controls; }

  private getCategory(id: number) {
    this.categoryService.getById(id).pipe(first()).subscribe(category => {
      this.category.id = category.id;
      this.category.name = category.name;
      this.category.parentCategoryId = category.parentCategoryId;
      this.category.parentCategory = category.parentCategory;

      this.editCategoryForm = this.formBuilder.group({
        id: [category.id, Validators.required],
        name: [category.name, Validators.required],
        parentCategoryId: [category.parentCategoryId]
      });
    });
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.editCategoryForm.invalid) {
      return;
    }

    this.loading = true;
    this.categoryService.update(this.editCategoryForm.value)
      .pipe(first(), )
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
