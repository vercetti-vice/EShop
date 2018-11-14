import {Component, HostListener, Input, OnInit} from '@angular/core';
import {first} from "rxjs/operators";
import {User} from "../../_models";
import {Category} from "../../_models/category";
import {CategoryService} from "../../_services/category.service";
import {Subscription} from "rxjs/internal/Subscription";

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
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

  @Input('sort-direction')
  sortDirection: string = '';

  @HostListener('click')
  sort() {
    this.sortDirection = this.sortDirection === 'Name' ? '-Name' : 'Name';
    this.sorts = this.sortDirection;
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

  previousPage(){
    this.page -= 1;
    this.loadAllCategories();
  }

  nextPage(){
    this.page += 1;
    this.loadAllCategories();
  }
}
