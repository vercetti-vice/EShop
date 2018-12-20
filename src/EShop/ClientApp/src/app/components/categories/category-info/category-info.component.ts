import { Component, OnInit } from '@angular/core';
import {Category} from '../../../models/category.model';
import {ActivatedRoute, Router} from '@angular/router';
import {ProductService} from '../../../services/product.service';
import {CategoryService} from '../../../services/category.service';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-category-info',
  templateUrl: './category-info.component.html',
  styleUrls: ['./category-info.component.css']
})
export class CategoryInfoComponent implements OnInit {

  category: Category;
  id: number;

  constructor(private categoryService: CategoryService,
              private activateRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.id = this.activateRoute.snapshot.params['id'];
    this.getCategory(this.id);
  }

  private getCategory(id: number) {
    this.categoryService.getById(id).pipe(first()).subscribe(category => {
      this.category = category;
    });
  }
}
