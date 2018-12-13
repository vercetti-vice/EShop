import { Component, OnInit } from '@angular/core';
import {Brand} from '../../../models/brand.model';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {BrandService} from '../../../services/brand.service';
import {Router} from '@angular/router';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-brand-create',
  templateUrl: './brand-create.component.html',
  styleUrls: ['./brand-create.component.css']
})
export class BrandCreateComponent implements OnInit {

  brand: Brand;
  createBrandForm: FormGroup;
  loading = false;
  submitted = false;

  constructor(private brandService: BrandService, private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit() {
    this.createBrandForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      imageUrl: ['', Validators.required],
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.createBrandForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.createBrandForm.invalid) {
      return;
    }

    this.loading = true;
    this.brandService.create(this.createBrandForm.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/brand-list']);
        },
        error => {
          this.loading = false;
        });
  }

}
