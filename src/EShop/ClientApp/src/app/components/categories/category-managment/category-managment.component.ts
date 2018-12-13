import {Component, Input, OnInit} from '@angular/core';
import {Category} from '../../../models/category.model';
import {CategoryService} from '../../../services/category.service';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-category-managment',
  templateUrl: './category-managment.component.html',
  styleUrls: ['./category-managment.component.css']
})
export class CategoryManagmentComponent implements OnInit {

  categories: Category[] = [];

  nextIsActive = false;

  sorts = '';
  filters = '';
  page = 1;
  pageSize = 10;

  constructor(private categoryService: CategoryService) {
  }

  ngOnInit() {
    this.loadAllCategories();
  }

  @Input('sort-direction')
  sortDirection = '';

  sort() {
    this.sortDirection = this.sortDirection === 'Name' ? '-Name' : 'Name';
    this.sorts = this.sortDirection;
    this.loadAllCategories();
  }

  deleteCategory(id: number) {
    this.categoryService.delete(id).pipe(first()).subscribe(() => {
      this.loadAllCategories();
    });
  }

  private loadAllCategories() {
    this.categoryService.getAll(this.sorts, this.filters, this.page, this.pageSize).pipe(first()).subscribe(categories => {
      this.categories = categories;
      if (this.categories.length < this.pageSize) {
        this.nextIsActive = false;
      } else {
        this.nextIsActive = true;
      }
    });
  }

  previousPage() {
    this.page -= 1;
    this.loadAllCategories();
    this.nextIsActive = true;
  }

  nextPage() {
    this.page += 1;
    this.loadAllCategories();
  }

}
