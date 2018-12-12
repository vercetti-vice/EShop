import {Component, Input, OnInit} from '@angular/core';
import {Brand} from '../../../models/brand.model';
import {BrandEditorComponent} from '../brand-editor/brand-editor.component';
import {AlertService, MessageSeverity} from '../../../services/alert.service';
import {BrandService} from '../../../services/brand.service';
import {Utilities} from '../../../services/utilities';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-brand-managment',
  templateUrl: './brand-managment.component.html',
  styleUrls: ['./brand-managment.component.css']
})
export class BrandManagmentComponent implements OnInit{

  brands: Brand[] = [];

  sorts = '';
  filters = '';
  page = 1;
  pageSize = 10;

  nextIsActive = false;

  constructor(private brandService: BrandService) {

  }

  ngOnInit() {
    this.loadAllBrands();
  }

  deleteBrand(id: number) {
    this.brandService.delete(id).pipe(first()).subscribe(() => {
      this.loadAllBrands();
    });
  }

  private loadAllBrands() {
    this.brandService.getAll(this.sorts, this.filters, this.page, this.pageSize).pipe(first()).subscribe(brands => {
      this.brands = brands;
      if(this.brands.length < this.pageSize) {
        this.nextIsActive = false;
      } else {
        this.nextIsActive = true;
      }
    });


  }

  @Input('sort-direction')
  sortDirection = '';

  sort() {
    this.sortDirection = this.sortDirection === 'Name' ? '-Name' : 'Name';
    this.sorts = this.sortDirection;
    this.loadAllBrands();
  }

  previousPage() {
    this.page -= 1;
    this.loadAllBrands();
    this.nextIsActive = true;
  }

  nextPage(){
    this.page += 1;
    this.loadAllBrands();
  }

}
