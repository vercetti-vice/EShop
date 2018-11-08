import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AlertService, BrandService, UserService} from "../../_services";
import {Brand} from "../../_models/brand";
import {first} from "rxjs/operators";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

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
              private router: Router,
              private userService: UserService,
              private alertService: AlertService) { }

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
          this.alertService.success('Создание бренда прошло успешно', true);
          this.router.navigate(['/brand-list']);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }
}
