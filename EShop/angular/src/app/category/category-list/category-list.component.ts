import { Component, OnInit } from '@angular/core';
import {first} from "rxjs/operators";
import {User} from "../../_models";
import {Color} from "../../_models/color";
import {ColorService} from "../../_services/color.service";
import {Category} from "../../_models/category";
import {CategoryService} from "../../_services/category.service";

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {

  currentUser: User;
  categories: Category[] = [];

  sorts: string = '';
  filters: string = '';
  page: number = 1;
  pageSize: number = 10;

  constructor(private categoryService: CategoryService) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit() {
    this.loadAllCategories();
  }

  deleteCategory(id: number) {
    this.categoryService.delete(id).pipe(first()).subscribe(() => {
      this.loadAllCategories()
    });
  }

  private loadAllCategories() {
    this.categoryService.getAll(this.sorts, this.filters, this.page, this.pageSize).pipe(first()).subscribe(categories => {
      this.categories = categories;
    });
  }

}
