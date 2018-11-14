import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AlertService, UserService} from "../../_services";
import {first} from "rxjs/operators";
import {ActivatedRoute, Router} from "@angular/router";
import {Category} from "../../_models/category";
import {CategoryService} from "../../_services/category.service";

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.css']
})
export class CategoryEditComponent implements OnInit {

  category = new Category();
  categories: Category[] = [];
  id: number;
  editCategoryForm: FormGroup;
  loading = false;
  submitted = false;

  sorts: string = '';
  filters: string = '';
  page: number = 1;
  pageSize: number = 10000;

  constructor(private categoryService: CategoryService,
              private activateRoute: ActivatedRoute,
              private formBuilder: FormBuilder,
              private router: Router,
              private userService: UserService,
              private alertService: AlertService) { }

  ngOnInit() {
    this.id = this.activateRoute.snapshot.params['id'];
    this.getCategory(this.id);
    this.loadAllCategories();
    this.editCategoryForm = this.formBuilder.group({
      id: [this.category.id, Validators.required],
      name: [this.category.name, Validators.required],
      parentCategoryId: []
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.editCategoryForm.controls; }

  private getCategory(id: number) {
    this.categoryService.getById(id).pipe(first()).subscribe(category => {
      this.category.id = category.id;
      this.category.name = category.name;
      this.category.parentCategoryId = category.parentCategoryId;
      this.category.parentCategory = category.parentCategory;
    });
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.editCategoryForm.invalid) {
      return;
    }

    this.loading = true;
    console.log(this.editCategoryForm);
    this.categoryService.update(this.editCategoryForm.value)
      .pipe(first(), )
      .subscribe(
        data => {
          this.alertService.success('Изменение категории прошло успешно', true);
          this.router.navigate(['/category-list']);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

  private loadAllCategories() {
    this.categoryService.getAll(this.sorts, this.filters, this.page, this.pageSize).pipe(first()).subscribe(categories => {
      this.categories = categories;
    });
  }
}
