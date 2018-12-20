import { Component, OnInit } from '@angular/core';
import {Brand} from '../../../models/brand.model';
import {BrandService} from '../../../services/brand.service';
import {ActivatedRoute, Router} from '@angular/router';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-brand-info',
  templateUrl: './brand-info.component.html',
  styleUrls: ['./brand-info.component.css']
})
export class BrandInfoComponent implements OnInit {

  brand: Brand;
  id: number;

  constructor(private brandService: BrandService,
              private activateRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.id = this.activateRoute.snapshot.params['id'];
    this.getBrand(this.id);
  }

  private getBrand(id: number) {
    this.brandService.getById(id).pipe(first()).subscribe(brand => {
      this.brand = brand;
    });
  }

}
