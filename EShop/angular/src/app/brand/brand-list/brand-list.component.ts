import { Component, OnInit } from '@angular/core';
import {Brand} from "../../_models/brand";
import {BrandService} from "../../_services";
import {User} from "../../_models";
import {first} from "rxjs/operators";

@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.css']
})
export class BrandListComponent implements OnInit {
  currentUser: User;
  brands: Brand[] = [];

  sorts: string = '';
  filters: string = '';
  page: number = 1;
  pageSize: number = 10;

  constructor(private brandService: BrandService) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit() {
    this.loadAllBrands();
  }

  deleteBrand(id: number) {
    this.brandService.delete(id).pipe(first()).subscribe(() => {
      this.loadAllBrands()
    });
  }

  private loadAllBrands() {
    this.brandService.getAll(this.sorts, this.filters, this.page, this.pageSize).pipe(first()).subscribe(brands => {
      this.brands = brands;
    });
  }
}
