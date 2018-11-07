import { Component, OnInit } from '@angular/core';
import {first} from "rxjs/operators";
import {User} from "../../_models";
import {Color} from "../../_models/color";
import {ColorService} from "../../_services/color.service";

@Component({
  selector: 'app-color-list',
  templateUrl: './color-list.component.html',
  styleUrls: ['./color-list.component.css']
})
export class ColorListComponent implements OnInit {

  currentUser: User;
  colors: Color[] = [];

  sorts: string = '';
  filters: string = '';
  page: number = 1;
  pageSize: number = 10;

  constructor(private colorService: ColorService) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit() {
    this.loadAllColors();
  }

  deleteColor(id: number) {
    this.colorService.delete(id).pipe(first()).subscribe(() => {
      this.loadAllColors()
    });
  }

  private loadAllColors() {
    this.colorService.getAll(this.sorts, this.filters, this.page, this.pageSize).pipe(first()).subscribe(colors => {
      this.colors = colors;
    });
  }

}
